using System;

namespace ContractTracker.Domain.Entities
{
    public class BurnRateAnalysis
    {
        public decimal CurrentMonthlyBurn { get; set; }
        public decimal CurrentQuarterlyBurn { get; set; }
        public decimal CurrentAnnualBurn { get; set; }
        public decimal RemainingFunds { get; set; }
        public int MonthsRemaining { get; set; }
        public int MonthsUntilFundingDepleted { get; set; }
        public DateTime? ProjectedDepletionDate { get; set; }
        public bool WillExceedFunding { get; set; }
        public decimal FundingShortfall { get; set; }
        public FundingWarningLevel WarningLevel { get; set; }
    }
}