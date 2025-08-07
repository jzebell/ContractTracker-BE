using System;
using System.Collections.Generic;
using System.Linq;

namespace ContractTracker.Domain.Entities
{
    public class Contract
    {
        private readonly List<ContractLCAT> _contractLCATs = new();
        private readonly List<ContractResource> _contractResources = new();
        private readonly List<ContractModification> _contractModifications = new();

        public Guid Id { get; private set; }
        public string ContractNumber { get; private set; }
        public string CustomerName { get; private set; }
        public string PrimeContractorName { get; private set; }
        public bool IsPrime { get; private set; }
        public ContractType Type { get; private set; }
        public ContractStatus Status { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal TotalValue { get; private set; }
        public decimal FundedValue { get; private set; }
        public int StandardFullTimeHours { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string CreatedBy { get; private set; }
        public string ModifiedBy { get; private set; }

        // Navigation properties
        public IReadOnlyCollection<ContractLCAT> ContractLCATs => _contractLCATs.AsReadOnly();
        public IReadOnlyCollection<ContractResource> ContractResources => _contractResources.AsReadOnly();
        public IReadOnlyCollection<ContractModification> ContractModifications => _contractModifications.AsReadOnly();

        // Constructor for EF Core
        protected Contract() { }

        // Constructor for creating new contracts
        public Contract(
            string contractNumber,
            string customerName,
            string primeContractorName,
            bool isPrime,
            ContractType type,
            DateTime startDate,
            DateTime endDate,
            decimal totalValue,
            decimal fundedValue,
            int standardFullTimeHours,
            string description,
            string createdBy)
        {
            Id = Guid.NewGuid();
            ContractNumber = contractNumber;
            CustomerName = customerName;
            PrimeContractorName = primeContractorName;
            IsPrime = isPrime;
            Type = type;
            Status = ContractStatus.Draft;
            StartDate = startDate;
            EndDate = endDate;
            TotalValue = totalValue;
            FundedValue = fundedValue;
            StandardFullTimeHours = standardFullTimeHours > 0 ? standardFullTimeHours : 1912;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
            ModifiedBy = createdBy;
        }

        // Contract lifecycle methods
        public void Activate()
        {
            if (Status != ContractStatus.Draft)
                throw new InvalidOperationException("Only draft contracts can be activated");
            
            Status = ContractStatus.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Close()
        {
            if (Status != ContractStatus.Active)
                throw new InvalidOperationException("Only active contracts can be closed");
            
            Status = ContractStatus.Closed;
            UpdatedAt = DateTime.UtcNow;
        }

        // Funding management
        public void UpdateFunding(decimal newFundedValue, string justification, string modifiedBy)
        {
            if (newFundedValue > TotalValue)
                throw new InvalidOperationException("Funded value cannot exceed total contract value");
            
            var modification = new ContractModification(
                Id,
                ModificationType.FundingChange,
                FundedValue,
                newFundedValue,
                justification,
                modifiedBy
            );
            
            _contractModifications.Add(modification);
            FundedValue = newFundedValue;
            ModifiedBy = modifiedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        // LCAT rate override management
        public void AddLCATOverride(Guid lcatId, decimal overrideRate, DateTime effectiveDate)
        {
            var existingOverride = _contractLCATs.FirstOrDefault(cl => cl.LCATId == lcatId);
            if (existingOverride != null)
            {
                existingOverride.UpdateRate(overrideRate, effectiveDate);
            }
            else
            {
                _contractLCATs.Add(new ContractLCAT(Id, lcatId, overrideRate, effectiveDate));
            }
            UpdatedAt = DateTime.UtcNow;
        }

        // Resource assignment management
        public void AssignResource(
            Guid resourceId, 
            decimal allocationPercentage, 
            DateTime startDate,
            decimal? annualHours = null)
        {
            // Check if resource is already assigned
            var existingAssignment = _contractResources.FirstOrDefault(cr => cr.ResourceId == resourceId && cr.IsActive);
            if (existingAssignment != null)
                throw new InvalidOperationException("Resource is already assigned to this contract");

            // Validate allocation doesn't exceed 100%
            // Note: This would need to check across all contracts for the resource
            if (allocationPercentage > 100 || allocationPercentage <= 0)
                throw new InvalidOperationException("Allocation percentage must be between 0 and 100");

            var contractResource = new ContractResource(
                Id,
                resourceId,
                allocationPercentage,
                startDate,
                annualHours ?? (StandardFullTimeHours * allocationPercentage / 100)
            );
            
            _contractResources.Add(contractResource);
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveResource(Guid resourceId, DateTime endDate)
        {
            var assignment = _contractResources.FirstOrDefault(cr => cr.ResourceId == resourceId && cr.IsActive);
            if (assignment == null)
                throw new InvalidOperationException("Resource is not assigned to this contract");

            assignment.EndAssignment(endDate);
            UpdatedAt = DateTime.UtcNow;
        }

        // Financial calculation methods
        public decimal CalculateMonthlyBurnRate()
        {
            decimal monthlyBurn = 0;
            var activeResources = _contractResources.Where(cr => cr.IsActive);
            
            foreach (var resource in activeResources)
            {
                // Calculate based on annual hours divided by 12
                var monthlyHours = resource.AnnualHours / 12;
                // This would need to join with Resource entity to get burdened cost
                // Placeholder for now - would be implemented with repository pattern
                monthlyBurn += resource.CalculateMonthlyBurn();
            }

            return monthlyBurn;
        }

        public decimal CalculateQuarterlyBurnRate()
        {
            return CalculateMonthlyBurnRate() * 3;
        }

        public decimal CalculateAnnualBurnRate()
        {
            return CalculateMonthlyBurnRate() * 12;
        }

        public decimal GetTotalBurned()
        {
            // This is a placeholder - in reality, you'd calculate from timesheets or actual costs
            // For now, estimate based on time elapsed
            if (StartDate > DateTime.UtcNow)
                return 0; // Contract hasn't started yet
            
            var effectiveEndDate = DateTime.UtcNow < EndDate ? DateTime.UtcNow : EndDate;
            var monthsElapsed = ((effectiveEndDate - StartDate).Days / 30.0);
            
            return CalculateMonthlyBurnRate() * (decimal)Math.Max(0, monthsElapsed);
        }

        public BurnRateAnalysis AnalyzeBurnRate()
        {
            var monthlyBurn = CalculateMonthlyBurnRate();
            var remainingFunds = FundedValue - GetTotalBurned();
            var monthsUntilContractEnd = ((EndDate - DateTime.UtcNow).Days / 30.0);
            
            var analysis = new BurnRateAnalysis
            {
                CurrentMonthlyBurn = monthlyBurn,
                CurrentQuarterlyBurn = CalculateQuarterlyBurnRate(),
                CurrentAnnualBurn = CalculateAnnualBurnRate(),
                RemainingFunds = remainingFunds,
                MonthsRemaining = (int)monthsUntilContractEnd
            };

            if (monthlyBurn > 0)
            {
                analysis.MonthsUntilFundingDepleted = (int)(remainingFunds / monthlyBurn);
                analysis.ProjectedDepletionDate = DateTime.UtcNow.AddMonths(analysis.MonthsUntilFundingDepleted);
                
                // Calculate if we'll run out of money before contract ends
                analysis.WillExceedFunding = analysis.ProjectedDepletionDate < EndDate;
                
                if (analysis.WillExceedFunding)
                {
                    analysis.FundingShortfall = (monthlyBurn * (decimal)monthsUntilContractEnd) - remainingFunds;
                }
                
                // Set warning levels
                if (analysis.MonthsUntilFundingDepleted <= 2)
                    analysis.WarningLevel = FundingWarningLevel.Critical;
                else if (analysis.MonthsUntilFundingDepleted <= 3)
                    analysis.WarningLevel = FundingWarningLevel.High;
                else if (analysis.MonthsUntilFundingDepleted <= 6)
                    analysis.WarningLevel = FundingWarningLevel.Medium;
                else if (analysis.MonthsUntilFundingDepleted <= 12)
                    analysis.WarningLevel = FundingWarningLevel.Low;
                else
                    analysis.WarningLevel = FundingWarningLevel.None;
            }
            else
            {
                analysis.MonthsUntilFundingDepleted = int.MaxValue;
                analysis.WarningLevel = FundingWarningLevel.None;
            }

            return analysis;
        }

        public FundingWarningLevel CalculateFundingWarningLevel()
        {
            var analysis = AnalyzeBurnRate();
            return analysis.WarningLevel;
        }

        // Update methods for existing properties
        public void UpdateDetails(
            string customerName,
            string primeContractorName,
            bool isPrime,
            ContractType type,
            DateTime startDate,
            DateTime endDate,
            decimal totalValue,
            int standardFullTimeHours,
            string description,
            string modifiedBy)
        {
            CustomerName = customerName;
            PrimeContractorName = primeContractorName;
            IsPrime = isPrime;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            TotalValue = totalValue;
            StandardFullTimeHours = standardFullTimeHours;
            Description = description;
            ModifiedBy = modifiedBy;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    // Enums
    public enum ContractType
    {
        TimeAndMaterials,
        FixedPrice,
        CostPlus,
        LaborHour
    }

    public enum ContractStatus
    {
        Draft,
        Active,
        Closed
    }

    public enum FundingWarningLevel
    {
        None,
        Low,
        Medium,
        High,
        Critical
    }

    public enum ModificationType
    {
        FundingChange,
        ScopeChange,
        DateChange,
        RateChange
    }
}