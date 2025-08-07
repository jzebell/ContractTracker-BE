using System;
using System.Collections.Generic;
using System.Linq;

namespace ContractTracker.Domain.Entities
{
    public class LCAT
    {
        private readonly List<LCATRate> _rates = new();
        private readonly List<PositionTitle> _positionTitles = new();

        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }  // This is the title/name of the LCAT
        public string Description { get; private set; }
        public string Category { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string CreatedBy { get; private set; }
        public string ModifiedBy { get; private set; }

        // Navigation properties
        public IReadOnlyCollection<LCATRate> Rates => _rates.AsReadOnly();
        public IReadOnlyCollection<PositionTitle> PositionTitles => _positionTitles.AsReadOnly();

        // Constructor for EF Core
        protected LCAT() { }

        // Constructor for creating new LCATs
        public LCAT(
            string code,
            string name,
            string description,
            string category,
            string createdBy)
        {
            Id = Guid.NewGuid();
            Code = code;
            Name = name;
            Description = description;
            Category = category;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            CreatedBy = createdBy;
            ModifiedBy = createdBy;
        }

        // Business logic methods
        public void UpdateDetails(
            string code,
            string name,
            string description,
            string category,
            string modifiedBy)
        {
            Code = code;
            Name = name;
            Description = description;
            Category = category;
            ModifiedBy = modifiedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate(string modifiedBy)
        {
            IsActive = false;
            ModifiedBy = modifiedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Reactivate(string modifiedBy)
        {
            IsActive = true;
            ModifiedBy = modifiedBy;
            UpdatedAt = DateTime.UtcNow;
        }

        // Rate management
        public void AddRate(decimal publishedRate, decimal defaultBillRate, DateTime effectiveDate, string createdBy)
        {
            var rate = new LCATRate(Id, publishedRate, defaultBillRate, effectiveDate, createdBy);
            _rates.Add(rate);
            UpdatedAt = DateTime.UtcNow;
        }

        public LCATRate GetCurrentPublishedRate()
        {
            return _rates
                .Where(r => r.RateType == RateType.Published && r.EffectiveDate <= DateTime.UtcNow)
                .OrderByDescending(r => r.EffectiveDate)
                .FirstOrDefault();
        }

        public LCATRate GetCurrentDefaultRate()
        {
            return _rates
                .Where(r => r.RateType == RateType.DefaultBill && r.EffectiveDate <= DateTime.UtcNow)
                .OrderByDescending(r => r.EffectiveDate)
                .FirstOrDefault();
        }

        public decimal GetCurrentBillRate()
        {
            var defaultRate = GetCurrentDefaultRate();
            return defaultRate?.Rate ?? 0;
        }

        // Position title management
        public void AddPositionTitle(string title, string createdBy)
        {
            var positionTitle = new PositionTitle(Id, title, createdBy);
            _positionTitles.Add(positionTitle);
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemovePositionTitle(Guid positionTitleId)
        {
            var title = _positionTitles.FirstOrDefault(pt => pt.Id == positionTitleId);
            if (title != null)
            {
                title.Deactivate();
                UpdatedAt = DateTime.UtcNow;
            }
        }
    }

    public enum RateType
    {
        Published,
        DefaultBill
    }
}