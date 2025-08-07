using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContractTracker.Domain.Entities;

namespace ContractTracker.Application.Services
{
    public interface IDashboardAnalyticsService
    {
        Task<DashboardMetrics> GetDashboardMetricsAsync();
        Task<List<ContractHealthCard>> GetContractHealthCardsAsync();
        Task<ResourceUtilizationMetrics> GetResourceUtilizationAsync();
        Task<FinancialProjections> GetFinancialProjectionsAsync(int monthsAhead = 12);
        Task<List<AlertNotification>> GetCriticalAlertsAsync();
    }

    public class DashboardMetrics
    {
        public decimal TotalContractValue { get; set; }
        public decimal TotalFundedValue { get; set; }
        public decimal TotalBurnedAmount { get; set; }
        public decimal MonthlyBurnRate { get; set; }
        public decimal QuarterlyBurnRate { get; set; }
        public int ActiveContracts { get; set; }
        public int DraftContracts { get; set; }
        public int ClosedContracts { get; set; }
        public int CriticalContracts { get; set; }
        public int WarningContracts { get; set; }
        public PortfolioHealth OverallHealth { get; set; }
        public decimal ProjectedMonthlyRevenue { get; set; }
        public decimal ProjectedMonthlyProfit { get; set; }
        public DateTime CalculatedAt { get; set; }
    }

    public class ContractHealthCard
    {
        public Guid ContractId { get; set; }
        public string ContractNumber { get; set; }
        public string CustomerName { get; set; }
        public string PrimeContractorName { get; set; }
        public bool IsPrime { get; set; }
        public ContractStatus Status { get; set; }
        public decimal TotalValue { get; set; }
        public decimal FundedValue { get; set; }
        public decimal BurnedAmount { get; set; }
        public decimal MonthlyBurnRate { get; set; }
        public int MonthsUntilDepletion { get; set; }
        public DateTime? ProjectedDepletionDate { get; set; }
        public FundingWarningLevel WarningLevel { get; set; }
        public int ResourceCount { get; set; }
        public decimal ResourceUtilization { get; set; } // Average allocation %
        public decimal ProfitMargin { get; set; }
        public List<string> Alerts { get; set; }
        public ContractTrend Trend { get; set; } // Up, Down, Stable
    }

    public class ResourceUtilizationMetrics
    {
        public int TotalResources { get; set; }
        public int ActiveResources { get; set; }
        public int BenchResources { get; set; } // Not assigned to any contract
        public int UnderwaterResources { get; set; }
        public decimal AverageUtilization { get; set; }
        public decimal TotalMonthlyCost { get; set; }
        public decimal TotalMonthlyRevenue { get; set; }
        public Dictionary<string, ResourceTypeMetrics> MetricsByType { get; set; }
        public List<ResourceAllocation> TopUtilizedResources { get; set; }
        public List<ResourceAllocation> UnderutilizedResources { get; set; }
    }

    public class ResourceTypeMetrics
    {
        public string ResourceType { get; set; }
        public int Count { get; set; }
        public decimal AverageCost { get; set; }
        public decimal AverageRevenue { get; set; }
        public decimal AverageMargin { get; set; }
        public decimal UtilizationPercentage { get; set; }
    }

    public class ResourceAllocation
    {
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; }
        public string LCATTitle { get; set; }
        public decimal TotalAllocation { get; set; }
        public int ContractCount { get; set; }
        public decimal MonthlyCost { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public bool IsUnderwater { get; set; }
    }

    public class FinancialProjections
    {
        public List<MonthlyProjection> Projections { get; set; }
        public decimal TotalProjectedRevenue { get; set; }
        public decimal TotalProjectedCost { get; set; }
        public decimal TotalProjectedProfit { get; set; }
        public List<ContractDepletion> DepletionSchedule { get; set; }
        public DateTime ProjectionDate { get; set; }
        public int MonthsProjected { get; set; }
    }

    public class MonthlyProjection
    {
        public DateTime Month { get; set; }
        public decimal ProjectedRevenue { get; set; }
        public decimal ProjectedCost { get; set; }
        public decimal ProjectedProfit { get; set; }
        public decimal CumulativeRevenue { get; set; }
        public decimal CumulativeCost { get; set; }
        public int ActiveContractCount { get; set; }
        public List<string> ExpiringContracts { get; set; }
        public List<string> DepletingContracts { get; set; }
    }

    public class ContractDepletion
    {
        public Guid ContractId { get; set; }
        public string ContractNumber { get; set; }
        public DateTime EstimatedDepletionDate { get; set; }
        public decimal RemainingFunds { get; set; }
        public int DaysUntilDepletion { get; set; }
        public string ImpactSeverity { get; set; } // High, Medium, Low
    }

    public class AlertNotification
    {
        public Guid Id { get; set; }
        public AlertType Type { get; set; }
        public AlertSeverity Severity { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string EntityType { get; set; } // Contract, Resource, etc.
        public Guid? EntityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Dictionary<string, object> Metadata { get; set; }
    }

    public enum PortfolioHealth
    {
        Excellent,  // All contracts healthy
        Good,       // Mostly healthy, few warnings
        Fair,       // Multiple warnings
        Poor,       // Multiple critical issues
        Critical    // Immediate action needed
    }

    public enum ContractTrend
    {
        Improving,
        Stable,
        Declining
    }

    public enum AlertType
    {
        FundingCritical,
        FundingWarning,
        ResourceUnderwater,
        ContractExpiring,
        OverAllocation,
        UnderUtilization,
        AnomalyDetected
    }

    public enum AlertSeverity
    {
        Info,
        Warning,
        High,
        Critical
    }
}