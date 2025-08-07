using System;

namespace ContractTracker.Domain.Entities
{
    public class ResourceTypeDefinition
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal DefaultWrapRate { get; private set; }
        public bool RequiresFixedPrice { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string CreatedBy { get; private set; }

        protected ResourceTypeDefinition() { }

        public ResourceTypeDefinition(string name, string description, 
            decimal defaultWrapRate, bool requiresFixedPrice, string createdBy)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            DefaultWrapRate = defaultWrapRate;
            RequiresFixedPrice = requiresFixedPrice;
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
            CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
        }
    }
}