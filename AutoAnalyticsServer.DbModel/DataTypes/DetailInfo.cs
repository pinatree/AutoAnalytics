using System;
using System.Collections.Generic;

#nullable disable

namespace AutoAnalyticsServer.DbModel.DataTypes
{
    public partial class DetailInfo
    {
        public DetailInfo()
        {
            AssociationRuleCauseNavigations = new HashSet<AssociationRule>();
            AssociationRuleСonsequenceNavigations = new HashSet<AssociationRule>();
        }

        public long Id { get; set; }
        public string DetGroup { get; set; }
        public string DetSubgroup { get; set; }
        public string Detail { get; set; }

        public virtual ICollection<AssociationRule> AssociationRuleCauseNavigations { get; set; }
        public virtual ICollection<AssociationRule> AssociationRuleСonsequenceNavigations { get; set; }
    }
}
