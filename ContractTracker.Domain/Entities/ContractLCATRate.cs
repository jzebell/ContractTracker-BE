using System;

namespace ContractTracker.Domain.Entities
{
    public class ContractLCATRate
    {
        public Guid Id { get; private set; }
        public Guid ContractId { get; private set; }
        public Contract Contract { get; private set; }
        public Guid LCATId { get; private set; }
        public LCAT LCAT { get; private set; }
        public decimal BillRate { get; private set; }
        public DateTime EffectiveDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string? Notes { get; private set; }

        protected ContractLCATRate() { }

        public ContractLCATRate(Guid contractId, Guid lcatId, decimal billRate, 
            DateTime effectiveDate, string createdBy, string? notes = null)
        {
            Id = Guid.NewGuid();
            ContractId = contractId;
            LCATId = lcatId;
            
            if (billRate <= 0)
                throw new ArgumentException("Bill rate must be positive", nameof(billRate));
                
            BillRate = billRate;
            EffectiveDate = effectiveDate;
            CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
            CreatedDate = DateTime.UtcNow;
            Notes = notes;
        }

        public bool IsEffective(DateTime? asOfDate = null)
        {
            var checkDate = asOfDate ?? DateTime.UtcNow;
            return EffectiveDate <= checkDate && (!EndDate.HasValue || EndDate.Value > checkDate);
        }
    }
}