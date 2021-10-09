using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace AutoAnalytics.WebPortal.Domain.DetailAnalysis
{
    [Table("TSUBGROUP", Schema = "DetailAnalytics")]
    public partial class TSubgroup
    {
        public TSubgroup()
        {
            TDetails = new HashSet<TDetail>();
        }

        public long Id { get; set; }
        public long GroupId { get; set; }
        public string CName { get; set; }

        public virtual TGroup Group { get; set; }
        public virtual ICollection<TDetail> TDetails { get; set; }
    }
}
