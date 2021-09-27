using System;
using System.Collections.Generic;

#nullable disable

namespace AutoAnalytics.WebPortal.Domain.DetailAnalysis
{
    public partial class TDetail
    {
        public TDetail()
        {
            TAssocRuleConseqDetails = new HashSet<TAssocRule>();
            TAssocRuleReasonDetails = new HashSet<TAssocRule>();
            TCrashes = new HashSet<TCrash>();
        }

        public long Id { get; set; }
        public long SubgroupId { get; set; }
        public string CName { get; set; }

        public virtual TSubgroup Subgroup { get; set; }
        public virtual ICollection<TAssocRule> TAssocRuleConseqDetails { get; set; }
        public virtual ICollection<TAssocRule> TAssocRuleReasonDetails { get; set; }
        public virtual ICollection<TCrash> TCrashes { get; set; }
    }
}
