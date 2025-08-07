using System;

namespace ContractTracker.Api.DTOs
{
    // Resource DTOs
    public class CreateResourceDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Type { get; set; } // W2Internal, Subcontractor, Contractor1099, FixedPrice
        public Guid? LCATId { get; set; }
        public decimal PayRate { get; set; }
        public DateTime StartDate { get; set; }
        public string ClearanceLevel { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
    }

    public class ResourceDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public Guid? LCATId { get; set; }
        public string LCATName { get; set; }
        public decimal PayRate { get; set; }
        public decimal BurdenedCost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public string ClearanceLevel { get; set; }
        public DateTime? ClearanceExpirationDate { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public decimal TotalAllocation { get; set; }
        public decimal Margin { get; set; }
        public bool IsUnderwater { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class UpdateResourceDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public Guid? LCATId { get; set; }
        public decimal PayRate { get; set; }
        public string ClearanceLevel { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
    }

    // LCAT DTOs
    public class CreateLCATDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal PublishedRate { get; set; }
        public decimal DefaultBillRate { get; set; }
    }

    public class LCATDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public decimal CurrentPublishedRate { get; set; }
        public decimal CurrentDefaultBillRate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class UpdateLCATDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }

    public class AddRateDto
    {
        public decimal PublishedRate { get; set; }
        public decimal DefaultBillRate { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}