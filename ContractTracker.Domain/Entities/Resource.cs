using System;
using System.Collections.Generic;
using System.Linq;

namespace ContractTracker.Domain.Entities
{
    public class Resource
    {
        private readonly List<ContractResource> _contractResources = new();

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Type { get; private set; } // W2Internal, Subcontractor, Contractor1099, FixedPrice
        public Guid? LCATId { get; private set; }
        public decimal PayRate { get; private set; }
        public decimal BurdenedCost { get; private set; } // Calculated: PayRate * WrapRate
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsActive { get; private set; }
        public string ClearanceLevel { get; private set; }
        public DateTime? ClearanceExpirationDate { get; private set; }
        public string Location { get; private set; }
        public string Notes { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string CreatedBy { get; private set; }
        public string ModifiedBy { get; private set; }

        // Navigation properties
        public LCAT LCAT { get; private set; }
        public IReadOnlyCollection<ContractResource> ContractResources => _contractResources.AsReadOnly();

        // Constructor for EF Core
        protected Resource() { }

        // Constructor for creating new resources
        public Resource(
            string firstName,
            string lastName,
            string email,
            string type,
            Guid? lcatId,
            decimal payRate,
            DateTime startDate,
            string clearanceLevel,
            string location,
            string notes,
            string createdBy)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Type = type;
            LCATId = lcatId;
            PayRate = payRate;
            BurdenedCost = CalculateBurdenedCost(payRate, type);
            StartDate = startDate;
            IsActive = true;
            ClearanceLevel = clearanceLevel;
            Location = location;
            Notes = notes;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
            ModifiedBy = createdBy;
        }

        // Business logic methods
        private decimal CalculateBurdenedCost(decimal payRate, string resourceType)
        {
            // Standard wrap rates - these could be configurable
            decimal wrapRate = resourceType switch
            {
                "W2Internal" => 2.28m,
                "Subcontractor" => 1.15m,
                "Contractor1099" => 1.15m,
                "FixedPrice" => 1.0m,
                _ => 2.28m
            };

            return payRate * wrapRate;
        }

        public void UpdatePayRate(decimal newPayRate, string modifiedBy)
        {
            PayRate = newPayRate;
            BurdenedCost = CalculateBurdenedCost(newPayRate, Type);
            ModifiedBy = modifiedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(
            string firstName,
            string lastName,
            string email,
            string type,
            Guid? lcatId,
            decimal payRate,
            string clearanceLevel,
            string location,
            string notes,
            string modifiedBy)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Type = type;
            LCATId = lcatId;
            PayRate = payRate;
            BurdenedCost = CalculateBurdenedCost(payRate, type);
            ClearanceLevel = clearanceLevel;
            Location = location;
            Notes = notes;
            ModifiedBy = modifiedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssignToLCAT(Guid lcatId, string modifiedBy)
        {
            LCATId = lcatId;
            ModifiedBy = modifiedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Terminate(DateTime endDate, string modifiedBy)
        {
            if (endDate < StartDate)
                throw new InvalidOperationException("End date cannot be before start date");

            EndDate = endDate;
            IsActive = false;
            ModifiedBy = modifiedBy;
            UpdatedAt = DateTime.UtcNow;

            // End all active contract assignments
            foreach (var assignment in _contractResources.Where(cr => cr.IsActive))
            {
                assignment.EndAssignment(endDate);
            }
        }

        public void Reactivate(string modifiedBy)
        {
            EndDate = null;
            IsActive = true;
            ModifiedBy = modifiedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateClearance(string clearanceLevel, DateTime? expirationDate, string modifiedBy)
        {
            ClearanceLevel = clearanceLevel;
            ClearanceExpirationDate = expirationDate;
            ModifiedBy = modifiedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        // Calculate total allocation across all contracts
        public decimal GetTotalAllocation()
        {
            return _contractResources
                .Where(cr => cr.IsActive)
                .Sum(cr => cr.AllocationPercentage);
        }

        // Check if resource is overallocated
        public bool IsOverallocated()
        {
            return GetTotalAllocation() > 100;
        }

        // Check if resource is underutilized
        public bool IsUnderutilized()
        {
            var totalAllocation = GetTotalAllocation();
            return totalAllocation > 0 && totalAllocation < 75; // Less than 75% is considered underutilized
        }

        // Calculate monthly cost for the resource
        public decimal CalculateMonthlyCost()
        {
            // Standard monthly hours (annual hours / 12)
            const decimal monthlyHours = 1912m / 12m; // ~159.33 hours
            return BurdenedCost * monthlyHours;
        }

        // Calculate monthly revenue if assigned to contracts
        public decimal CalculateMonthlyRevenue()
        {
            if (LCAT == null) return 0;

            var billRate = LCAT.GetCurrentDefaultRate()?.Rate ?? 0;
            if (billRate == 0) return 0;

            const decimal monthlyHours = 1912m / 12m;
            var totalAllocationPercentage = GetTotalAllocation() / 100m;
            
            return billRate * monthlyHours * totalAllocationPercentage;
        }

        // Calculate margin
        public decimal CalculateMargin()
        {
            var revenue = CalculateMonthlyRevenue();
            var cost = CalculateMonthlyCost();
            
            if (revenue == 0) return 0;
            
            return ((revenue - cost) / revenue) * 100;
        }

        // Check if resource is underwater (negative margin)
        public bool IsUnderwater()
        {
            return CalculateMargin() < 0;
        }

        // Financial analysis methods for dashboard
        public decimal CalculateMonthlyBurn()
        {
            // This is the resource's total monthly burn across all contracts
            return _contractResources
                .Where(cr => cr.IsActive)
                .Sum(cr => cr.CalculateMonthlyBurn());
        }

        public decimal CalculateQuarterlyBurn()
        {
            return CalculateMonthlyBurn() * 3;
        }

        public decimal CalculateAnnualBurn()
        {
            return CalculateMonthlyBurn() * 12;
        }

        // Get the full name for display
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        // Check if clearance is expired or expiring soon
        public bool IsClearanceExpired()
        {
            return ClearanceExpirationDate.HasValue && ClearanceExpirationDate.Value < DateTime.UtcNow;
        }

        public bool IsClearanceExpiringSoon(int daysThreshold = 60)
        {
            if (!ClearanceExpirationDate.HasValue) return false;
            
            var daysUntilExpiration = (ClearanceExpirationDate.Value - DateTime.UtcNow).TotalDays;
            return daysUntilExpiration > 0 && daysUntilExpiration <= daysThreshold;
        }
    }
}