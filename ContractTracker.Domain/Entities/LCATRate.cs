using System;

namespace ContractTracker.Domain.Entities
{
    public class LCATRate
    {
        public Guid Id { get; private set; }
        public Guid LCATId { get; private set; }
        public decimal Rate { get; private set; }
        public RateType RateType { get; private set; }
        public DateTime EffectiveDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string Notes { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string CreatedBy { get; private set; }

        // Navigation property
        public LCAT LCAT { get; private set; }

        // Constructor for EF Core
        protected LCATRate() { }

        // Constructor for creating new rates
        public LCATRate(
            Guid lcatId,
            decimal publishedRate,
            decimal defaultBillRate,
            DateTime effectiveDate,
            string createdBy)
        {
            Id = Guid.NewGuid();
            LCATId = lcatId;
            
            // Create two rate records - one for published, one for default bill
            if (publishedRate > 0)
            {
                Rate = publishedRate;
                RateType = RateType.Published;
            }
            else
            {
                Rate = defaultBillRate;
                RateType = RateType.DefaultBill;
            }
            
            EffectiveDate = effectiveDate;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        // Alternative constructor for single rate type
        public LCATRate(
            Guid lcatId,
            decimal rate,
            RateType rateType,
            DateTime effectiveDate,
            string notes,
            string createdBy)
        {
            Id = Guid.NewGuid();
            LCATId = lcatId;
            Rate = rate;
            RateType = rateType;
            EffectiveDate = effectiveDate;
            Notes = notes;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        public void EndRate(DateTime endDate)
        {
            if (endDate < EffectiveDate)
                throw new InvalidOperationException("End date cannot be before effective date");
            
            EndDate = endDate;
        }

        public bool IsCurrentlyEffective()
        {
            var now = DateTime.UtcNow;
            return EffectiveDate <= now && (!EndDate.HasValue || EndDate.Value > now);
        }
    }
}