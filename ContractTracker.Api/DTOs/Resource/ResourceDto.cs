using System;

namespace ContractTracker.Api.DTOs.Resource
{
    public class ResourceDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ResourceType { get; set; }
        public Guid LCATId { get; set; }
        public string LCATName { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal? BurdenedCost { get; set; }
        public decimal? BillRate { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateResourceDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ResourceType { get; set; }
        public Guid LCATId { get; set; }
        public decimal HourlyRate { get; set; }
        public DateTime StartDate { get; set; }
    }
}