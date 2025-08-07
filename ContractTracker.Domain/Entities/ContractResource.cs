using System;

namespace ContractTracker.Domain.Entities
{
    public class ContractResource
    {
        public Guid Id { get; private set; }
        public Guid ContractId { get; private set; }
        public Guid ResourceId { get; private set; }
        public decimal AllocationPercentage { get; private set; }
        public decimal AnnualHours { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string CreatedBy { get; private set; }
        public string ModifiedBy { get; private set; }

        // Navigation properties
        public Contract Contract { get; private set; }
        public Resource Resource { get; private set; }

        // Constructor for EF Core
        protected ContractResource() { }

        // Constructor for creating new assignments
        public ContractResource(
            Guid contractId,
            Guid resourceId,
            decimal allocationPercentage,
            DateTime startDate,
            decimal annualHours)
        {
            Id = Guid.NewGuid();
            ContractId = contractId;
            ResourceId = resourceId;
            AllocationPercentage = allocationPercentage;
            AnnualHours = annualHours;
            StartDate = startDate;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            CreatedBy = "System"; // Will be replaced with actual user
            ModifiedBy = "System";
        }

        // Methods
        public void UpdateAllocation(decimal newAllocationPercentage, decimal newAnnualHours)
        {
            if (newAllocationPercentage <= 0 || newAllocationPercentage > 100)
                throw new InvalidOperationException("Allocation percentage must be between 0 and 100");

            AllocationPercentage = newAllocationPercentage;
            AnnualHours = newAnnualHours;
            UpdatedAt = DateTime.UtcNow;
        }

        public void EndAssignment(DateTime endDate)
        {
            if (endDate < StartDate)
                throw new InvalidOperationException("End date cannot be before start date");

            EndDate = endDate;
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public decimal CalculateMonthlyBurn()
        {
            // Monthly hours = Annual hours / 12
            var monthlyHours = AnnualHours / 12m;
            
            // Get the burdened cost from the Resource entity
            if (Resource != null && Resource.BurdenedCost > 0)
            {
                var hourlyBurdenedCost = Resource.BurdenedCost; // This is already the hourly rate
                return monthlyHours * hourlyBurdenedCost * (AllocationPercentage / 100m);
            }
            
            // Fallback if Resource is not loaded (shouldn't happen in production)
            // Using an estimated average rate
            var estimatedHourlyRate = 150m;
            return monthlyHours * estimatedHourlyRate * (AllocationPercentage / 100m);
        }

        public decimal CalculateQuarterlyBurn()
        {
            return CalculateMonthlyBurn() * 3;
        }

        public decimal CalculateAnnualBurn()
        {
            return CalculateMonthlyBurn() * 12;
        }
    }
}