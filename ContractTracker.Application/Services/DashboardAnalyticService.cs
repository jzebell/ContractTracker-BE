using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContractTracker.Domain.Entities;
using ContractTracker.Infrastructure.Data;

namespace ContractTracker.Application.Services
{
    public class DashboardAnalyticsService : IDashboardAnalyticsService
    {
        private readonly AppDbContext _context;

        public DashboardAnalyticsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardMetrics> GetDashboardMetricsAsync()
        {
            var contracts = await _context.Contracts
                .Include(c => c.ContractResources)
                    .ThenInclude(cr => cr.Resource)
                        .ThenInclude(r => r.LCAT)
                .ToListAsync();

            var resources = await _context.Resources
                .Include(r => r.LCAT)
                .Include(r => r.ContractResources)
                .ToListAsync();

            var metrics = new DashboardMetrics
            {
                TotalContractValue = contracts.Sum(c => c.TotalValue),
                TotalFundedValue = contracts.Sum(c => c.FundedValue),
                TotalBurnedAmount = contracts.Sum(c => c.GetTotalBurned()),
                ActiveContracts = contracts.Count(c => c.Status == ContractStatus.Active),
                DraftContracts = contracts.Count(c => c.Status == ContractStatus.Draft),
                ClosedContracts = contracts.Count(c => c.Status == ContractStatus.Closed),
                CalculatedAt = DateTime.UtcNow
            };

            // Calculate burn rates across all active contracts
            var activeContracts = contracts.Where(c => c.Status == ContractStatus.Active).ToList();
            metrics.MonthlyBurnRate = activeContracts.Sum(c => c.CalculateMonthlyBurnRate());
            metrics.QuarterlyBurnRate = activeContracts.Sum(c => c.CalculateQuarterlyBurnRate());

            // Count critical and warning contracts
            metrics.CriticalContracts = contracts.Count(c => 
                c.CalculateFundingWarningLevel() == FundingWarningLevel.Critical);
            metrics.WarningContracts = contracts.Count(c => 
                c.CalculateFundingWarningLevel() == FundingWarningLevel.High);

            // Calculate projected revenue and profit
            metrics.ProjectedMonthlyRevenue = CalculateMonthlyRevenue(activeContracts);
            metrics.ProjectedMonthlyProfit = CalculateMonthlyProfit(activeContracts, resources);

            // Determine overall portfolio health
            metrics.OverallHealth = DeterminePortfolioHealth(metrics);

            return metrics;
        }

        public async Task<List<ContractHealthCard>> GetContractHealthCardsAsync()
        {
            var contracts = await _context.Contracts
                .Include(c => c.ContractResources)
                    .ThenInclude(cr => cr.Resource)
                        .ThenInclude(r => r.LCAT)
                .ToListAsync();

            var healthCards = new List<ContractHealthCard>();

            foreach (var contract in contracts)
            {
                var burnAnalysis = contract.AnalyzeBurnRate();
                var resources = contract.ContractResources.Where(cr => cr.IsActive).ToList();
                
                var card = new ContractHealthCard
                {
                    ContractId = contract.Id,
                    ContractNumber = contract.ContractNumber,
                    CustomerName = contract.CustomerName,
                    PrimeContractorName = contract.PrimeContractorName,
                    IsPrime = contract.IsPrime,
                    Status = contract.Status,
                    TotalValue = contract.TotalValue,
                    FundedValue = contract.FundedValue,
                    BurnedAmount = contract.GetTotalBurned(),
                    MonthlyBurnRate = burnAnalysis.CurrentMonthlyBurn,
                    MonthsUntilDepletion = burnAnalysis.MonthsUntilFundingDepleted,
                    ProjectedDepletionDate = burnAnalysis.ProjectedDepletionDate,
                    WarningLevel = burnAnalysis.WarningLevel,
                    ResourceCount = resources.Count,
                    ResourceUtilization = resources.Any() 
                        ? resources.Average(r => r.AllocationPercentage) 
                        : 0,
                    Alerts = new List<string>(),
                    Trend = DetermineContractTrend(contract)
                };

                // Calculate profit margin
                if (card.MonthlyBurnRate > 0)
                {
                    var monthlyRevenue = CalculateContractMonthlyRevenue(contract);
                    var monthlyCost = CalculateContractMonthlyCost(contract);
                    card.ProfitMargin = monthlyRevenue > 0 
                        ? ((monthlyRevenue - monthlyCost) / monthlyRevenue) * 100 
                        : 0;
                }

                // Generate alerts
                GenerateContractAlerts(card, contract, burnAnalysis);

                healthCards.Add(card);
            }

            return healthCards.OrderBy(c => c.WarningLevel).ThenBy(c => c.MonthsUntilDepletion).ToList();
        }

        public async Task<ResourceUtilizationMetrics> GetResourceUtilizationAsync()
        {
            var resources = await _context.Resources
                .Include(r => r.LCAT)
                .Include(r => r.ContractResources)
                    .ThenInclude(cr => cr.Contract)
                .Where(r => r.IsActive)
                .ToListAsync();

            var metrics = new ResourceUtilizationMetrics
            {
                TotalResources = resources.Count(),
                ActiveResources = resources.Count(r => r.ContractResources.Any(cr => cr.IsActive)),
                BenchResources = resources.Count(r => !r.ContractResources.Any(cr => cr.IsActive)),
                MetricsByType = new Dictionary<string, ResourceTypeMetrics>(),
                TopUtilizedResources = new List<ResourceAllocation>(),
                UnderutilizedResources = new List<ResourceAllocation>()
            };

            // Calculate average utilization
            var utilizationRates = resources.Select(r => 
            {
                var activeAssignments = r.ContractResources.Where(cr => cr.IsActive);
                return activeAssignments.Sum(cr => cr.AllocationPercentage);
            }).ToList();

            metrics.AverageUtilization = utilizationRates.Any() ? utilizationRates.Average() : 0;

            // Calculate costs and revenue
            foreach (var resource in resources)
            {
                var monthlyCost = CalculateResourceMonthlyCost(resource);
                var monthlyRevenue = CalculateResourceMonthlyRevenue(resource);
                
                metrics.TotalMonthlyCost += monthlyCost;
                metrics.TotalMonthlyRevenue += monthlyRevenue;

                // Check if underwater
                if (monthlyRevenue < monthlyCost)
                    metrics.UnderwaterResources++;

                // Build allocation record
                var allocation = new ResourceAllocation
                {
                    ResourceId = resource.Id,
                    ResourceName = $"{resource.FirstName} {resource.LastName}",
                    LCATTitle = resource.LCAT?.Name ?? "Unassigned",
                    TotalAllocation = resource.ContractResources
                        .Where(cr => cr.IsActive)
                        .Sum(cr => cr.AllocationPercentage),
                    ContractCount = resource.ContractResources.Count(cr => cr.IsActive),
                    MonthlyCost = monthlyCost,
                    MonthlyRevenue = monthlyRevenue,
                    IsUnderwater = monthlyRevenue < monthlyCost
                };

                // Categorize by utilization
                if (allocation.TotalAllocation >= 90)
                    metrics.TopUtilizedResources.Add(allocation);
                else if (allocation.TotalAllocation < 50)
                    metrics.UnderutilizedResources.Add(allocation);
            }

            // Group metrics by resource type
            var typeGroups = resources.GroupBy(r => r.Type);
            foreach (var group in typeGroups)
            {
                var typeMetrics = new ResourceTypeMetrics
                {
                    ResourceType = group.Key,
                    Count = group.Count(),
                    AverageCost = group.Average(r => CalculateResourceMonthlyCost(r)),
                    AverageRevenue = group.Average(r => CalculateResourceMonthlyRevenue(r)),
                    UtilizationPercentage = group.Average(r => 
                        r.ContractResources.Where(cr => cr.IsActive).Sum(cr => cr.AllocationPercentage))
                };
                
                typeMetrics.AverageMargin = typeMetrics.AverageRevenue > 0
                    ? ((typeMetrics.AverageRevenue - typeMetrics.AverageCost) / typeMetrics.AverageRevenue) * 100
                    : 0;

                metrics.MetricsByType[group.Key] = typeMetrics;
            }

            // Sort lists
            metrics.TopUtilizedResources = metrics.TopUtilizedResources
                .OrderByDescending(r => r.TotalAllocation)
                .Take(10)
                .ToList();
            
            metrics.UnderutilizedResources = metrics.UnderutilizedResources
                .OrderBy(r => r.TotalAllocation)
                .Take(10)
                .ToList();

            return metrics;
        }

        public async Task<FinancialProjections> GetFinancialProjectionsAsync(int monthsAhead = 12)
        {
            var contracts = await _context.Contracts
                .Include(c => c.ContractResources)
                    .ThenInclude(cr => cr.Resource)
                        .ThenInclude(r => r.LCAT)
                .Where(c => c.Status == ContractStatus.Active)
                .ToListAsync();

            var projections = new FinancialProjections
            {
                Projections = new List<MonthlyProjection>(),
                DepletionSchedule = new List<ContractDepletion>(),
                ProjectionDate = DateTime.UtcNow,
                MonthsProjected = monthsAhead
            };

            var cumulativeRevenue = 0m;
            var cumulativeCost = 0m;

            // Generate monthly projections
            for (int month = 0; month < monthsAhead; month++)
            {
                var projectionDate = DateTime.UtcNow.AddMonths(month);
                var monthProjection = new MonthlyProjection
                {
                    Month = new DateTime(projectionDate.Year, projectionDate.Month, 1),
                    ExpiringContracts = new List<string>(),
                    DepletingContracts = new List<string>()
                };

                // Calculate projections for active contracts in this month
                foreach (var contract in contracts)
                {
                    // Check if contract will be active in this month
                    if (contract.EndDate >= projectionDate && contract.StartDate <= projectionDate)
                    {
                        var monthlyRevenue = CalculateContractMonthlyRevenue(contract);
                        var monthlyCost = CalculateContractMonthlyCost(contract);
                        
                        monthProjection.ProjectedRevenue += monthlyRevenue;
                        monthProjection.ProjectedCost += monthlyCost;
                        monthProjection.ActiveContractCount++;

                        // Check for expiring contracts
                        if (contract.EndDate.Year == projectionDate.Year && 
                            contract.EndDate.Month == projectionDate.Month)
                        {
                            monthProjection.ExpiringContracts.Add(contract.ContractNumber);
                        }

                        // Check for depleting contracts
                        var burnAnalysis = contract.AnalyzeBurnRate();
                        if (burnAnalysis.ProjectedDepletionDate.HasValue &&
                            burnAnalysis.ProjectedDepletionDate.Value.Year == projectionDate.Year &&
                            burnAnalysis.ProjectedDepletionDate.Value.Month == projectionDate.Month)
                        {
                            monthProjection.DepletingContracts.Add(contract.ContractNumber);
                        }
                    }
                }

                monthProjection.ProjectedProfit = monthProjection.ProjectedRevenue - monthProjection.ProjectedCost;
                cumulativeRevenue += monthProjection.ProjectedRevenue;
                cumulativeCost += monthProjection.ProjectedCost;
                monthProjection.CumulativeRevenue = cumulativeRevenue;
                monthProjection.CumulativeCost = cumulativeCost;

                projections.Projections.Add(monthProjection);
            }

            // Calculate totals
            projections.TotalProjectedRevenue = projections.Projections.Sum(p => p.ProjectedRevenue);
            projections.TotalProjectedCost = projections.Projections.Sum(p => p.ProjectedCost);
            projections.TotalProjectedProfit = projections.TotalProjectedRevenue - projections.TotalProjectedCost;

            // Build depletion schedule
            foreach (var contract in contracts)
            {
                var burnAnalysis = contract.AnalyzeBurnRate();
                if (burnAnalysis.ProjectedDepletionDate.HasValue && 
                    burnAnalysis.ProjectedDepletionDate.Value <= DateTime.UtcNow.AddMonths(monthsAhead))
                {
                    projections.DepletionSchedule.Add(new ContractDepletion
                    {
                        ContractId = contract.Id,
                        ContractNumber = contract.ContractNumber,
                        EstimatedDepletionDate = burnAnalysis.ProjectedDepletionDate.Value,
                        RemainingFunds = contract.FundedValue - contract.GetTotalBurned(),
                        DaysUntilDepletion = (int)(burnAnalysis.ProjectedDepletionDate.Value - DateTime.UtcNow).TotalDays,
                        ImpactSeverity = DetermineDepletionImpact(contract, burnAnalysis)
                    });
                }
            }

            projections.DepletionSchedule = projections.DepletionSchedule
                .OrderBy(d => d.EstimatedDepletionDate)
                .ToList();

            return projections;
        }

        public async Task<List<AlertNotification>> GetCriticalAlertsAsync()
        {
            var alerts = new List<AlertNotification>();
            
            // Get contracts and resources
            var contracts = await _context.Contracts
                .Include(c => c.ContractResources)
                .Where(c => c.Status == ContractStatus.Active)
                .ToListAsync();
            
            var resources = await _context.Resources
                .Include(r => r.LCAT)
                .Include(r => r.ContractResources)
                .Where(r => r.IsActive)
                .ToListAsync();

            // Check contract funding
            foreach (var contract in contracts)
            {
                var burnAnalysis = contract.AnalyzeBurnRate();
                
                if (burnAnalysis.WarningLevel == FundingWarningLevel.Critical)
                {
                    alerts.Add(new AlertNotification
                    {
                        Id = Guid.NewGuid(),
                        Type = AlertType.FundingCritical,
                        Severity = AlertSeverity.Critical,
                        Title = $"Critical Funding Alert: {contract.ContractNumber}",
                        Message = $"Contract will deplete funding in {burnAnalysis.MonthsUntilFundingDepleted} months",
                        EntityType = "Contract",
                        EntityId = contract.Id,
                        CreatedAt = DateTime.UtcNow,
                        Metadata = new Dictionary<string, object>
                        {
                            ["RemainingFunds"] = burnAnalysis.RemainingFunds,
                            ["MonthlyBurn"] = burnAnalysis.CurrentMonthlyBurn,
                            ["DepletionDate"] = burnAnalysis.ProjectedDepletionDate
                        }
                    });
                }

                // Check for contract expiration
                var daysUntilExpiration = (contract.EndDate - DateTime.UtcNow).TotalDays;
                if (daysUntilExpiration <= 60)
                {
                    alerts.Add(new AlertNotification
                    {
                        Id = Guid.NewGuid(),
                        Type = AlertType.ContractExpiring,
                        Severity = daysUntilExpiration <= 30 ? AlertSeverity.High : AlertSeverity.Warning,
                        Title = $"Contract Expiring: {contract.ContractNumber}",
                        Message = $"Contract expires in {(int)daysUntilExpiration} days",
                        EntityType = "Contract",
                        EntityId = contract.Id,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            // Check resource issues
            foreach (var resource in resources)
            {
                var totalAllocation = resource.ContractResources
                    .Where(cr => cr.IsActive)
                    .Sum(cr => cr.AllocationPercentage);

                // Over-allocation
                if (totalAllocation > 100)
                {
                    alerts.Add(new AlertNotification
                    {
                        Id = Guid.NewGuid(),
                        Type = AlertType.OverAllocation,
                        Severity = AlertSeverity.High,
                        Title = $"Resource Over-Allocated: {resource.FirstName} {resource.LastName}",
                        Message = $"Resource is allocated at {totalAllocation}% across contracts",
                        EntityType = "Resource",
                        EntityId = resource.Id,
                        CreatedAt = DateTime.UtcNow
                    });
                }

                // Underwater resource
                var monthlyCost = CalculateResourceMonthlyCost(resource);
                var monthlyRevenue = CalculateResourceMonthlyRevenue(resource);
                if (monthlyRevenue < monthlyCost && monthlyRevenue > 0)
                {
                    var margin = ((monthlyRevenue - monthlyCost) / monthlyRevenue) * 100;
                    alerts.Add(new AlertNotification
                    {
                        Id = Guid.NewGuid(),
                        Type = AlertType.ResourceUnderwater,
                        Severity = AlertSeverity.Warning,
                        Title = $"Underwater Resource: {resource.FirstName} {resource.LastName}",
                        Message = $"Resource margin is {margin:F1}% (losing ${monthlyCost - monthlyRevenue:F2}/month)",
                        EntityType = "Resource",
                        EntityId = resource.Id,
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            return alerts.OrderBy(a => a.Severity).ThenBy(a => a.CreatedAt).ToList();
        }

        // Helper methods
        private decimal CalculateMonthlyRevenue(List<Contract> contracts)
        {
            return contracts.Sum(c => CalculateContractMonthlyRevenue(c));
        }

        private decimal CalculateMonthlyProfit(List<Contract> contracts, List<Resource> resources)
        {
            var revenue = CalculateMonthlyRevenue(contracts);
            var cost = resources.Where(r => r.IsActive).Sum(r => CalculateResourceMonthlyCost(r));
            return revenue - cost;
        }

        private decimal CalculateContractMonthlyRevenue(Contract contract)
        {
            // This would use actual bill rates from contract
            // For now, using estimate based on funded value and duration
            if (contract.Status != ContractStatus.Active) return 0;
            
            var monthsInContract = ((contract.EndDate - contract.StartDate).Days / 30.0);
            return monthsInContract > 0 ? contract.FundedValue / (decimal)monthsInContract : 0;
        }

        private decimal CalculateContractMonthlyCost(Contract contract)
        {
            return contract.ContractResources
                .Where(cr => cr.IsActive)
                .Sum(cr => cr.CalculateMonthlyBurn());
        }

        private decimal CalculateResourceMonthlyCost(Resource resource)
        {
            return resource.BurdenedCost * 174; // Average monthly hours
        }

        private decimal CalculateResourceMonthlyRevenue(Resource resource)
        {
            if (resource.LCAT == null) return 0;
            
            // Get the current bill rate from LCAT
            var billRate = resource.LCAT.GetCurrentBillRate();
            if (billRate == 0) 
            {
                // Fallback to a default rate if no rate is set
                billRate = 150m;
            }
            
            var totalAllocation = resource.ContractResources
                .Where(cr => cr.IsActive)
                .Sum(cr => cr.AllocationPercentage) / 100;
            
            return billRate * 174 * totalAllocation; // Average monthly hours * allocation
        }

        private PortfolioHealth DeterminePortfolioHealth(DashboardMetrics metrics)
        {
            var totalContracts = metrics.ActiveContracts + metrics.DraftContracts;
            if (totalContracts == 0) return PortfolioHealth.Good;

            var criticalRatio = (double)metrics.CriticalContracts / totalContracts;
            var warningRatio = (double)metrics.WarningContracts / totalContracts;

            if (criticalRatio > 0.3) return PortfolioHealth.Critical;
            if (criticalRatio > 0.15 || warningRatio > 0.5) return PortfolioHealth.Poor;
            if (criticalRatio > 0.05 || warningRatio > 0.25) return PortfolioHealth.Fair;
            if (warningRatio > 0.1) return PortfolioHealth.Good;
            
            return PortfolioHealth.Excellent;
        }

        private ContractTrend DetermineContractTrend(Contract contract)
        {
            // This would analyze historical data to determine trend
            // For now, using simple logic based on burn rate vs funding
            var burnAnalysis = contract.AnalyzeBurnRate();
            
            if (burnAnalysis.WillExceedFunding)
                return ContractTrend.Declining;
            
            if (burnAnalysis.MonthsUntilFundingDepleted > 6)
                return ContractTrend.Stable;
            
            return ContractTrend.Improving;
        }

        private void GenerateContractAlerts(ContractHealthCard card, Contract contract, BurnRateAnalysis analysis)
        {
            if (analysis.WarningLevel == FundingWarningLevel.Critical)
                card.Alerts.Add($"⚠️ Critical: Funding depletes in {analysis.MonthsUntilFundingDepleted} months");
            
            if (analysis.WillExceedFunding)
                card.Alerts.Add($"💸 Projected shortfall: ${analysis.FundingShortfall:N0}");
            
            if (card.ResourceUtilization > 90)
                card.Alerts.Add("📊 High resource utilization");
            
            if (card.ProfitMargin < 10 && card.ProfitMargin > 0)
                card.Alerts.Add($"📉 Low margin: {card.ProfitMargin:F1}%");
            
            if (card.ProfitMargin < 0)
                card.Alerts.Add($"🔴 Negative margin: {card.ProfitMargin:F1}%");

            var daysUntilEnd = (contract.EndDate - DateTime.UtcNow).TotalDays;
            if (daysUntilEnd <= 60)
                card.Alerts.Add($"📅 Contract expires in {(int)daysUntilEnd} days");
        }

        private string DetermineDepletionImpact(Contract contract, BurnRateAnalysis analysis)
        {
            if (contract.TotalValue > 5000000 || analysis.MonthsUntilFundingDepleted <= 1)
                return "High";
            if (contract.TotalValue > 1000000 || analysis.MonthsUntilFundingDepleted <= 3)
                return "Medium";
            return "Low";
        }
    }
}