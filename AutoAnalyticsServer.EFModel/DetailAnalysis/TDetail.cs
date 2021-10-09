using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace AutoAnalytics.WebPortal.Domain.DetailAnalysis
{
    [Table("TDETAIL", Schema = "DetailAnalytics")]
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
