// ContractTracker.Api/DTOs/ContractDTOs.cs

using System;
using System.ComponentModel.DataAnnotations;

namespace ContractTracker.Api.DTOs
{
    // Create DTO
    public class CreateContractDto
    {
        [Required]
        [StringLength(50)]
        public string ContractNumber { get; set; }

        [Required]
        [StringLength(200)]
        public string ContractName { get; set; }

        [Required]
        [StringLength(200)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(200)]
        public string PrimeContractor { get; set; }

        [Required]
        public bool IsPrime { get; set; }

        [Required]
        public string ContractType { get; set; } // "FixedPrice", "TimeAndMaterials", "CostPlus", "LaborHourOnly"

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total value must be positive")]
        public decimal TotalValue { get; set; }

        [Range(0, double.MaxValue)]
        public decimal FundedValue { get; set; }

        [Range(1, 2080)]
        public decimal StandardFullTimeHours { get; set; } = 1912;

        [StringLength(1000)]
        public string? Description { get; set; }
    }

    // Read DTO - Simplified without calculated fields
    public class ContractDto
    {
        public Guid Id { get; set; }
        public string ContractNumber { get; set; }
        public string ContractName { get; set; }
        public string CustomerName { get; set; }
        public string PrimeContractor { get; set; }
        public bool IsPrime { get; set; }
        public string ContractType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalValue { get; set; }
        public decimal FundedValue { get; set; }
        public decimal StandardFullTimeHours { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }

    // Funding Update DTO
    public class UpdateFundingDto
    {
        [Required]
        [StringLength(50)]
        public string ModificationNumber { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal FundedAmount { get; set; }

        [StringLength(500)]
        public string? Justification { get; set; }
    }
}