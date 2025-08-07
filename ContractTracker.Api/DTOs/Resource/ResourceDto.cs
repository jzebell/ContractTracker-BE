using System;
using System.ComponentModel.DataAnnotations;

namespace ContractTracker.Api.DTOs.Resource
{
    public class ResourceDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // ResourceType enum as string
        public Guid? LCATId { get; set; }
        public string? LCATName { get; set; }
        public decimal CurrentPayRate { get; set; }
        public string ClearanceLevel { get; set; } = string.Empty;
        public DateTime? ClearanceExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        // Calculated fields
        public string FullName => $"{FirstName} {LastName}";
        public decimal BurdenedCost { get; set; }
        public decimal? BillRate { get; set; }
        public decimal? Margin { get; set; }
        public bool IsUnderwater { get; set; }
    }

    public class CreateResourceDto
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = "W2Internal"; // W2Internal, Subcontractor, Contractor1099, FixedPrice

        public Guid? LCATId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal CurrentPayRate { get; set; }

        public string ClearanceLevel { get; set; } = "None"; // None, Public, Secret, TopSecret, TopSecretSCI
        
        public DateTime? ClearanceExpirationDate { get; set; }
    }

    public class UpdateResourceDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Type { get; set; }
        public Guid? LCATId { get; set; }
        public decimal? CurrentPayRate { get; set; }
        public string? ClearanceLevel { get; set; }
        public DateTime? ClearanceExpirationDate { get; set; }
        public bool? IsActive { get; set; }
    }

    public class BatchUpdateResourcesDto
    {
        [Required]
        public DateTime EffectiveDate { get; set; }
        
        [StringLength(500)]
        public string Notes { get; set; } = string.Empty;
        
        [Required]
        public List<UpdateResourceDto> ResourceUpdates { get; set; } = new();
    }

    // For backwards compatibility with existing frontend
    public class LegacyResourceDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ResourceType { get; set; } = string.Empty; // Maps to Type
        public Guid LCATId { get; set; } // Maps to LCATId (nullable in new)
        public string? LCATName { get; set; }
        public decimal HourlyRate { get; set; } // Maps to CurrentPayRate
        public decimal? BurdenedCost { get; set; }
        public decimal? BillRate { get; set; }
        public DateTime StartDate { get; set; } // Maps to CreatedDate
        public bool IsActive { get; set; }
    }
}