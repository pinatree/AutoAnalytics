using System;
using System.Collections.Generic;

#nullable disable

namespace AutoAnalytics.WebPortal.Domain.DetailAnalysis
{
    public partial class TAssocRule
    {
        public long Id { get; set; }
        public long ReasonDetailId { get; set; }
        public long ConseqDetailId { get; set; }
        public DateTime CCalcDate { get; set; }
        public long CSupportCount { get; set; }
        public decimal CSupportPerc { get; set; }
        public decimal CReliability { get; set; }
        public decimal CLift { get; set; }
        public long RegionId { get; set; }
        public long ModelId { get; set; }

        public virtual TDetail ConseqDetail { get; set; }
        public virtual TDetail ReasonDetail { get; set; }
    }
}
