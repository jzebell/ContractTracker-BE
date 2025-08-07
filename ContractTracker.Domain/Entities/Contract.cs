using System;
using System.Collections.Generic;

namespace ContractTracker.Domain.Entities
{
    public class Contract
    {
        public Guid Id { get; private set; }
        public string ContractNumber { get; private set; }
        public string Name { get; private set; }
        public string? ProgramName { get; private set; }
        public decimal TotalValue { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string CreatedBy { get; private set; }

        public ICollection<ContractLCATRate> LCATRates { get; private set; }
        public ICollection<Resource> Resources { get; private set; }

        protected Contract() 
        { 
            LCATRates = new List<ContractLCATRate>();
            Resources = new List<Resource>();
        }

        public Contract(string contractNumber, string name, string? programName, 
            decimal totalValue, DateTime startDate, DateTime endDate, string createdBy)
        {
            Id = Guid.NewGuid();
            ContractNumber = contractNumber ?? throw new ArgumentNullException(nameof(contractNumber));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ProgramName = programName;
            
            if (totalValue <= 0)
                throw new ArgumentException("Contract value must be positive", nameof(totalValue));
            if (endDate <= startDate)
                throw new ArgumentException("End date must be after start date");
                
            TotalValue = totalValue;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
            CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
            
            LCATRates = new List<ContractLCATRate>();
            Resources = new List<Resource>();
        }
    }
}