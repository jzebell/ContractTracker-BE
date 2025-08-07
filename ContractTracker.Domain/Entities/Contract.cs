// ContractTracker.Domain/Entities/Contract.cs

using System;
using System.Collections.Generic;
using System.Linq;

namespace ContractTracker.Domain.Entities
{
    public class Contract
    {
        private readonly List<ContractLCAT> _contractLCATs = new();
        private readonly List<ContractResource> _contractResources = new();
        private readonly List<ContractModification> _modifications = new();

        // Private constructor for EF
        private Contract() { }

        public Contract(
            string contractNumber,
            string contractName,
            string customerName,
            string primeContractor,
            bool isPrime,
            ContractType contractType,
            DateTime startDate,
            DateTime endDate,
            decimal totalValue,
            string? description = null)
        {
            if (string.IsNullOrWhiteSpace(contractNumber))
                throw new ArgumentException("Contract number is required", nameof(contractNumber));
            if (string.IsNullOrWhiteSpace(contractName))
                throw new ArgumentException("Contract name is required", nameof(contractName));
            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Customer name is required", nameof(customerName));
            if (string.IsNullOrWhiteSpace(primeContractor))
                throw new ArgumentException("Prime contractor is required", nameof(primeContractor));
            if (endDate <= startDate)
                throw new ArgumentException("End date must be after start date", nameof(endDate));
            if (totalValue <= 0)
                throw new ArgumentException("Total value must be positive", nameof(totalValue));

            Id = Guid.NewGuid();
            ContractNumber = contractNumber;
            ContractName = contractName;
            CustomerName = customerName;
            PrimeContractor = primeContractor;
            IsPrime = isPrime;
            ContractType = contractType;
            StartDate = startDate;
            EndDate = endDate;
            TotalValue = totalValue;
            Description = description;
            Status = ContractStatus.Draft;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            
            // Initialize standard work hours (can be overridden)
            StandardFullTimeHours = 1912; // Default annual hours
        }

        public Guid Id { get; private set; }
        public string ContractNumber { get; private set; }
        public string ContractName { get; private set; }
        public string CustomerName { get; private set; } // End customer (e.g., government agency)
        public string PrimeContractor { get; private set; } // May or may not be us
        public bool IsPrime { get; private set; } // Are we the prime or a sub?
        public ContractType ContractType { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal TotalValue { get; private set; }
        public decimal FundedValue { get; private set; }
        public string? Description { get; private set; }
        public ContractStatus Status { get; private set; }
        public decimal StandardFullTimeHours { get; private set; } // Annual hours for FTE calculation
        
        // Tracking fields
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string? CreatedBy { get; private set; }
        public string? UpdatedBy { get; private set; }

        // Navigation properties
        public IReadOnlyCollection<ContractLCAT> ContractLCATs => _contractLCATs.AsReadOnly();
        public IReadOnlyCollection<ContractResource> ContractResources => _contractResources.AsReadOnly();
        public IReadOnlyCollection<ContractModification> Modifications => _modifications.AsReadOnly();

        // Business methods
        public void Activate()
        {
            if (Status != ContractStatus.Draft)
                throw new InvalidOperationException("Only draft contracts can be activated");
            
            if (FundedValue <= 0)
                throw new InvalidOperationException("Contract must have funding before activation");
            
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

        public void UpdateFunding(decimal fundedAmount, string modificationNumber, string? justification = null)
        {
            if (fundedAmount <= 0)
                throw new ArgumentException("Funded amount must be positive", nameof(fundedAmount));
            if (fundedAmount > TotalValue)
                throw new ArgumentException("Funded amount cannot exceed total contract value", nameof(fundedAmount));

            var modification = new ContractModification(
                Id,
                modificationNumber,
                ModificationType.FundingChange,
                FundedValue,
                fundedAmount,
                justification
            );

            _modifications.Add(modification);
            FundedValue = fundedAmount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetStandardHours(decimal annualFullTimeHours)
        {
            if (annualFullTimeHours <= 0 || annualFullTimeHours > 2080)
                throw new ArgumentException("Annual hours must be between 0 and 2080", nameof(annualFullTimeHours));
            
            StandardFullTimeHours = annualFullTimeHours;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetContractBillRate(Guid lcatId, decimal contractBillRate, DateTime effectiveDate, string? justification = null)
        {
            if (contractBillRate <= 0)
                throw new ArgumentException("Contract bill rate must be positive", nameof(contractBillRate));

            var existingRate = _contractLCATs.FirstOrDefault(cl => cl.LCATId == lcatId && cl.EffectiveDate == effectiveDate);
            if (existingRate != null)
            {
                existingRate.UpdateRate(contractBillRate, justification);
            }
            else
            {
                var contractLCAT = new ContractLCAT(Id, lcatId, contractBillRate, effectiveDate, justification);
                _contractLCATs.Add(contractLCAT);
            }

            UpdatedAt = DateTime.UtcNow;
        }

        public void AssignResource(
            Guid resourceId, 
            DateTime startDate, 
            DateTime? endDate = null, 
            decimal allocationPercentage = 100,
            decimal? customHoursPerYear = null)
        {
            if (allocationPercentage <= 0 || allocationPercentage > 100)
                throw new ArgumentException("Allocation percentage must be between 0 and 100", nameof(allocationPercentage));

            var existingAssignment = _contractResources.FirstOrDefault(cr => cr.ResourceId == resourceId && cr.IsActive);
            if (existingAssignment != null)
                throw new InvalidOperationException($"Resource is already assigned to this contract");

            var contractResource = new ContractResource(
                Id, 
                resourceId, 
                startDate, 
                endDate, 
                allocationPercentage,
                customHoursPerYear ?? (StandardFullTimeHours * allocationPercentage / 100)
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
                else
                    analysis.WarningLevel = FundingWarningLevel.None;
            }

            return analysis;
        }

        private decimal GetTotalBurned()
        {
            // This would calculate actual burned based on timesheet data
            // For now, estimate based on time elapsed
            var monthsElapsed = (DateTime.UtcNow - StartDate).Days / 30.0;
            return CalculateMonthlyBurnRate() * (decimal)monthsElapsed;
        }
    }

    public enum ContractType
    {
        FixedPrice,
        TimeAndMaterials,
        CostPlus,
        LaborHourOnly
    }

    public enum ContractStatus
    {
        Draft,      // For proposals and planning
        Active,     // Currently executing
        Closed      // Completed or terminated
    }

    public enum ModificationType
    {
        FundingChange,
        PeriodOfPerformanceChange,
        ScopeChange,
        RateChange,
        Other
    }

    public enum FundingWarningLevel
    {
        None,
        Low,
        Medium,
        High,
        Critical
    }

    // Supporting entities
    public class ContractLCAT
    {
        private ContractLCAT() { }

        public ContractLCAT(Guid contractId, Guid lcatId, decimal contractBillRate, DateTime effectiveDate, string? justification = null)
        {
            Id = Guid.NewGuid();
            ContractId = contractId;
            LCATId = lcatId;
            ContractBillRate = contractBillRate;
            EffectiveDate = effectiveDate;
            Justification = justification;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public Guid ContractId { get; private set; }
        public Guid LCATId { get; private set; }
        public decimal ContractBillRate { get; private set; } // What we bill the customer
        public DateTime EffectiveDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string? Justification { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // Navigation properties
        public Contract Contract { get; private set; }
        public LCAT LCAT { get; private set; }

        public void UpdateRate(decimal newRate, string? justification)
        {
            ContractBillRate = newRate;
            Justification = justification;
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public class ContractResource
    {
        private ContractResource() { }

        public ContractResource(
            Guid contractId, 
            Guid resourceId, 
            DateTime startDate, 
            DateTime? endDate, 
            decimal allocationPercentage,
            decimal annualHours)
        {
            Id = Guid.NewGuid();
            ContractId = contractId;
            ResourceId = resourceId;
            StartDate = startDate;
            EndDate = endDate;
            AllocationPercentage = allocationPercentage;
            AnnualHours = annualHours;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public Guid ContractId { get; private set; }
        public Guid ResourceId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public decimal AllocationPercentage { get; private set; }
        public decimal AnnualHours { get; private set; } // Actual hours for this resource (FT/PT)
        public bool IsActive => EndDate == null || EndDate > DateTime.UtcNow;
        
        // Future feature: Track individual resource funding if sub/fixed
        public decimal? FixedMonthlyAmount { get; private set; } // For subs or fixed price resources
        
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // Navigation properties
        public Contract Contract { get; private set; }
        public Resource Resource { get; private set; }

        public void EndAssignment(DateTime endDate)
        {
            if (endDate < StartDate)
                throw new ArgumentException("End date cannot be before start date");
            
            EndDate = endDate;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetCustomHours(decimal annualHours)
        {
            if (annualHours <= 0 || annualHours > 2080)
                throw new ArgumentException("Annual hours must be between 0 and 2080");
            
            AnnualHours = annualHours;
            UpdatedAt = DateTime.UtcNow;
        }

        // Future: Set fixed monthly amount for subs
        public void SetFixedMonthlyAmount(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Fixed amount must be positive");
            
            FixedMonthlyAmount = amount;
            UpdatedAt = DateTime.UtcNow;
        }

        public decimal CalculateMonthlyBurn()
        {
            // If fixed amount is set (for subs), use that
            if (FixedMonthlyAmount.HasValue)
                return FixedMonthlyAmount.Value;
            
            // Otherwise calculate based on hours
            var monthlyHours = AnnualHours / 12;
            // This needs Resource's burdened cost - will be injected via repository
            return 0; // Placeholder
        }
    }

    public class ContractModification
    {
        private ContractModification() { }

        public ContractModification(Guid contractId, string modificationNumber, ModificationType type, decimal? previousValue, decimal? newValue, string? justification)
        {
            Id = Guid.NewGuid();
            ContractId = contractId;
            ModificationNumber = modificationNumber;
            Type = type;
            PreviousValue = previousValue;
            NewValue = newValue;
            Justification = justification;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public Guid ContractId { get; private set; }
        public string ModificationNumber { get; private set; }
        public ModificationType Type { get; private set; }
        public decimal? PreviousValue { get; private set; }
        public decimal? NewValue { get; private set; }
        public string? Justification { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string? CreatedBy { get; private set; }

        // Navigation property
        public Contract Contract { get; private set; }
    }

    // Burn rate analysis result
    public class BurnRateAnalysis
    {
        public decimal CurrentMonthlyBurn { get; set; }
        public decimal CurrentQuarterlyBurn { get; set; }
        public decimal CurrentAnnualBurn { get; set; }
        public decimal RemainingFunds { get; set; }
        public int MonthsRemaining { get; set; } // Until contract end
        public int MonthsUntilFundingDepleted { get; set; }
        public DateTime? ProjectedDepletionDate { get; set; }
        public bool WillExceedFunding { get; set; }
        public decimal FundingShortfall { get; set; }
        public FundingWarningLevel WarningLevel { get; set; }
    }
}