using System;
using System.Collections.Generic;

#nullable disable

namespace AutoAnalytics.WebPortal.Domain.DetailAnalysis
{
    public partial class TGroup
    {
        public TGroup()
        {
            TSubgroups = new HashSet<TSubgroup>();
        }

        public long Id { get; set; }
        public string CName { get; set; }

        public virtual ICollection<TSubgroup> TSubgroups { get; set; }
    }
}
