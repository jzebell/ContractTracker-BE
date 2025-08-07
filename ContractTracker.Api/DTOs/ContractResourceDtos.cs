using System;
using System.Collections.Generic;

namespace ContractTracker.Api.DTOs
{
    /// <summary>
    /// DTO for assigning a resource to a contract
    /// </summary>
    public class AssignResourceDto
    {
        public Guid ResourceId { get; set; }
        public decimal AllocationPercentage { get; set; }
        public decimal AnnualHours { get; set; } = 1912; // Default federal FTE hours
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }
        public decimal? ContractBillRateOverride { get; set; }
        public Guid ContractId { get; set; }
    }


    /// <summary>
    /// DTO for updating a resource assignment
    /// </summary>
    public class UpdateResourceAssignmentDto
    {
        public decimal? AllocationPercentage { get; set; }
        public decimal? AnnualHours { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? ContractBillRateOverride { get; set; }
    }

    /// <summary>
    /// DTO for returning contract resource assignment details
    /// </summary>
    public class ContractResourceDto
    {
        public Guid Id { get; set; }
        public Guid ContractId { get; set; }
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; } = string.Empty;
        public string ResourceType { get; set; } = string.Empty;
        public string? LCATTitle { get; set; }
        public decimal AllocationPercentage { get; set; }
        public decimal AnnualHours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? ContractBillRateOverride { get; set; }
        public bool IsActive { get; set; }
        
        // Calculated fields
        public decimal PayRate { get; set; }
        public decimal BurdenedCost { get; set; }
        public decimal BillRate { get; set; }
        public decimal MonthlyBurn { get; set; }
        public decimal AnnualBurn { get; set; }
        public decimal Margin { get; set; }
        public bool IsUnderwater { get; set; }
    }

    /// <summary>
    /// DTO for batch assigning multiple resources
    /// </summary>
    public class BatchAssignResourcesDto
    {
        public Guid ContractId { get; set; }
        public List<AssignResourceDto> Resources { get; set; } = new();
    }

    /// <summary>
    /// DTO for resource availability check
    /// </summary>
    public class ResourceAvailabilityDto
    {
        public Guid ResourceId { get; set; }
        public string ResourceName { get; set; } = string.Empty;
        public decimal CurrentAllocation { get; set; }
        public decimal AvailableAllocation { get; set; }
        public bool IsFullyAllocated { get; set; }
        public bool IsOnBench { get; set; }
        public List<ContractAllocationDto> CurrentContracts { get; set; } = new();
    }

    /// <summary>
    /// DTO for showing a resource's allocation to a specific contract
    /// </summary>
    public class ContractAllocationDto
    {
        public Guid ContractId { get; set; }
        public string ContractNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public decimal AllocationPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    /// <summary>
    /// DTO for contract burn rate analysis
    /// </summary>
    public class ContractBurnRateDto
    {
        public Guid ContractId { get; set; }
        public string ContractNumber { get; set; } = string.Empty;
        public decimal TotalValue { get; set; }
        public decimal FundedValue { get; set; }
        public decimal? ActualBurnedAmount { get; set; }
        public decimal EstimatedMonthlyBurn { get; set; }
        public decimal ActualMonthlyBurn { get; set; }
        public decimal EstimatedAnnualBurn { get; set; }
        public decimal ActualAnnualBurn { get; set; }
        public int? MonthsUntilDepletion { get; set; }
        public DateTime? ProjectedDepletionDate { get; set; }
        public string FundingWarningLevel { get; set; } = string.Empty;
        public decimal FundingPercentageUsed { get; set; }
        public int AssignedResourceCount { get; set; }
        public List<ContractResourceDto> AssignedResources { get; set; } = new();
    }
}