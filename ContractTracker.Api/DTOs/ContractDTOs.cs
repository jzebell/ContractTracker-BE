using System;
using ContractTracker.Domain.Entities;

namespace ContractTracker.Api.DTOs
{
    public class CreateContractDto
    {
        public string ContractNumber { get; set; }
        public string CustomerName { get; set; }
        public string PrimeContractorName { get; set; }
        public bool IsPrime { get; set; }
        public ContractType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalValue { get; set; }
        public decimal FundedValue { get; set; }
        public int StandardFullTimeHours { get; set; } = 1912;
        public string Description { get; set; }
    }

    public class ContractDto
    {
        public Guid Id { get; set; }
        public string ContractNumber { get; set; }
        public string CustomerName { get; set; }
        public string PrimeContractorName { get; set; }
        public bool IsPrime { get; set; }
        public ContractType Type { get; set; }
        public ContractStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalValue { get; set; }
        public decimal FundedValue { get; set; }
        public int StandardFullTimeHours { get; set; }
        public string Description { get; set; }
        public decimal MonthlyBurnRate { get; set; }
        public decimal AnnualBurnRate { get; set; }
        public decimal BurnedAmount { get; set; }
        public FundingWarningLevel WarningLevel { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class UpdateFundingDto
    {
        public decimal NewFundedValue { get; set; }
        public string Justification { get; set; }
    }

    public class UpdateAllocationDto
    {
        public decimal AllocationPercentage { get; set; }
        public decimal? AnnualHours { get; set; }
    }
}