using System;

namespace ContractTracker.Domain.Entities
{
    public class Resource
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public ResourceType ResourceType { get; private set; }
        public Guid LCATId { get; private set; }
        public LCAT LCAT { get; private set; }
        public Guid? ContractId { get; private set; }
        public Contract Contract { get; private set; }
        public decimal HourlyRate { get; private set; }
        public decimal? AnnualSalary { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string CreatedBy { get; private set; }

        // For fixed price resources
        public decimal? FixedPriceAmount { get; private set; }
        public int? FixedPriceHours { get; private set; }

        public string FullName => $"{FirstName} {LastName}";

        protected Resource() { }

        public Resource(string firstName, string lastName, string email, 
            ResourceType resourceType, Guid lcatId, decimal hourlyRate, 
            DateTime startDate, string createdBy)
        {
            Id = Guid.NewGuid();
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            ResourceType = resourceType;
            LCATId = lcatId;
            
            if (hourlyRate <= 0)
                throw new ArgumentException("Hourly rate must be positive", nameof(hourlyRate));
                
            HourlyRate = hourlyRate;
            StartDate = startDate;
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
            CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
        }

        public void AssignToContract(Guid contractId)
        {
            ContractId = contractId;
        }

        public void Terminate(DateTime endDate)
        {
            if (endDate < StartDate)
                throw new ArgumentException("End date cannot be before start date", nameof(endDate));
                
            EndDate = endDate;
            IsActive = false;
        }
    }

    public enum ResourceType
    {
        W2Internal,
        Subcontractor,
        Contractor1099,
        FixedPrice
    }
}