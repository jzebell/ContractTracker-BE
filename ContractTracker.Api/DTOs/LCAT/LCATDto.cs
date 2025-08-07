using System;
using System.Collections.Generic;

namespace ContractTracker.Api.DTOs.LCAT
{
    public class LCATDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? CurrentPublishedRate { get; set; }
        public decimal? CurrentDefaultBillRate { get; set; }
        public List<string> PositionTitles { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class CreateLCATDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PublishedRate { get; set; }
        public decimal DefaultBillRate { get; set; }
        public List<string> PositionTitles { get; set; }
    }

    public class UpdateLCATRatesDto
    {
        public Guid LCATId { get; set; }
        public decimal? PublishedRate { get; set; }
        public decimal? DefaultBillRate { get; set; }
    }
    public class BatchUpdateRatesDto
{
    public DateTime EffectiveDate { get; set; }
    public string Notes { get; set; }
    public List<UpdateLCATRatesDto> RateUpdates { get; set; }
}
}