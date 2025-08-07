using System;

namespace ContractTracker.Domain.Entities
{
    public class PositionTitle
    {
        public Guid Id { get; private set; }
        public Guid LCATId { get; private set; }
        public LCAT LCAT { get; private set; }
        public string Title { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string CreatedBy { get; private set; }

        protected PositionTitle() { }

        public PositionTitle(Guid lcatId, string title, string createdBy)
        {
            Id = Guid.NewGuid();
            LCATId = lcatId;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
            CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}