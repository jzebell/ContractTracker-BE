using System;
using System.Collections.Generic;
using System.Linq;

namespace ContractTracker.Domain.Entities
{
    public class LCAT
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedDate { get; private set; }
        public string ModifiedBy { get; private set; }

        public ICollection<LCATRate> Rates { get; private set; }
        public ICollection<PositionTitle> PositionTitles { get; private set; }
        public ICollection<ContractLCATRate> ContractRates { get; private set; }

        protected LCAT() 
        { 
            Rates = new List<LCATRate>();
            PositionTitles = new List<PositionTitle>();
            ContractRates = new List<ContractLCATRate>();
        }

        public LCAT(string name, string description, string createdBy)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
            ModifiedBy = createdBy; // Set ModifiedBy same as CreatedBy initially
            CreatedDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            ModifiedDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
            IsActive = true;
            
            Rates = new List<LCATRate>();
            PositionTitles = new List<PositionTitle>();
            ContractRates = new List<ContractLCATRate>();
        }

        public LCATRate GetCurrentPublishedRate(DateTime? asOfDate = null)
        {
            var effectiveDate = asOfDate ?? DateTime.UtcNow;
            return Rates
                .Where(r => r.RateType == RateType.Published && r.EffectiveDate <= effectiveDate)
                .OrderByDescending(r => r.EffectiveDate)
                .FirstOrDefault();
        }

        public LCATRate GetCurrentDefaultRate(DateTime? asOfDate = null)
        {
            var effectiveDate = asOfDate ?? DateTime.UtcNow;
            return Rates
                .Where(r => r.RateType == RateType.DefaultBill && r.EffectiveDate <= effectiveDate)
                .OrderByDescending(r => r.EffectiveDate)
                .FirstOrDefault();
        }
    }
}