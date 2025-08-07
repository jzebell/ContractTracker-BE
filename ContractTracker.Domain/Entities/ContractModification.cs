using System;

namespace ContractTracker.Domain.Entities
{
    public class ContractModification
    {
        public Guid Id { get; private set; }
        public Guid ContractId { get; private set; }
        public ModificationType Type { get; private set; }
        public decimal OldValue { get; private set; }
        public decimal NewValue { get; private set; }
        public string Justification { get; private set; }
        public DateTime ModificationDate { get; private set; }
        public string ModifiedBy { get; private set; }

        // Navigation property
        public Contract Contract { get; private set; }

        // Constructor for EF Core
        protected ContractModification() { }

        // Constructor for creating modifications
        public ContractModification(
            Guid contractId,
            ModificationType type,
            decimal oldValue,
            decimal newValue,
            string justification,
            string modifiedBy)
        {
            Id = Guid.NewGuid();
            ContractId = contractId;
            Type = type;
            OldValue = oldValue;
            NewValue = newValue;
            Justification = justification;
            ModificationDate = DateTime.UtcNow;
            ModifiedBy = modifiedBy;
        }
    }
}