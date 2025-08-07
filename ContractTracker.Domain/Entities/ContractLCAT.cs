using System;

namespace ContractTracker.Domain.Entities
{
    public class ContractLCAT
    {
        public Guid Id { get; private set; }
        public Guid ContractId { get; private set; }
        public Guid LCATId { get; private set; }
        public decimal OverrideRate { get; private set; }
        public DateTime EffectiveDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // Navigation properties
        public Contract Contract { get; private set; }
        public LCAT LCAT { get; private set; }

        // Constructor for EF Core
        protected ContractLCAT() { }

        // Constructor for creating new overrides
        public ContractLCAT(Guid contractId, Guid lcatId, decimal overrideRate, DateTime effectiveDate)
        {
            Id = Guid.NewGuid();
            ContractId = contractId;
            LCATId = lcatId;
            OverrideRate = overrideRate;
            EffectiveDate = effectiveDate;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateRate(decimal newRate, DateTime effectiveDate)
        {
            OverrideRate = newRate;
            EffectiveDate = effectiveDate;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate(DateTime endDate)
        {
            EndDate = endDate;
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}