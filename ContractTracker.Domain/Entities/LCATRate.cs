using System;

namespace ContractTracker.Domain.Entities
{
    public class LCATRate
    {
        public Guid Id { get; private set; }
        public Guid LCATId { get; private set; }
        public LCAT LCAT { get; private set; }
        public RateType RateType { get; private set; }
        public decimal Rate { get; private set; }
        public DateTime EffectiveDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string? Notes { get; private set; }

        protected LCATRate() { }

        public LCATRate(Guid lcatId, RateType rateType, decimal rate, DateTime effectiveDate, string createdBy, string? notes = null)
        {
            Id = Guid.NewGuid();
            LCATId = lcatId;
            RateType = rateType;
            
            if (rate <= 0)
                throw new ArgumentException("Rate must be positive", nameof(rate));
                
            Rate = rate;
            EffectiveDate = effectiveDate;
            CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
            CreatedDate = DateTime.UtcNow;
            Notes = notes;
        }

        public void SetEndDate(DateTime endDate)
        {
            if (endDate <= EffectiveDate)
                throw new ArgumentException("End date must be after effective date", nameof(endDate));
                
            EndDate = endDate;
        }

        public bool IsEffective(DateTime? asOfDate = null)
        {
            var checkDate = asOfDate ?? DateTime.UtcNow;
            return EffectiveDate <= checkDate && (!EndDate.HasValue || EndDate.Value > checkDate);
        }
    }

    public enum RateType
    {
        Published,
        DefaultBill
    }
}