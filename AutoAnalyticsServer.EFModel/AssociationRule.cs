using System;
using System.Collections.Generic;

#nullable disable

namespace AutoAnalyticsServer.EFModel
{
    public partial class AssociationRule
    {
        public long Cause { get; set; }
        public long Сonsequence { get; set; }
        public double Confidence { get; set; }

        public virtual DetailInfo CauseNavigation { get; set; }
        public virtual DetailInfo СonsequenceNavigation { get; set; }
    }
}
