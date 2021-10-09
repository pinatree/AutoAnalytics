using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace AutoAnalytics.WebPortal.Domain.DetailAnalysis
{
    [Table("TGROUP", Schema = "DetailAnalytics")]
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
