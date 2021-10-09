using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace AutoAnalytics.WebPortal.Domain.DetailAnalysis
{
    [Table("TMODEL", Schema = "DetailAnalytics")]
    public partial class TModel
    {
        public long Id { get; set; }
        public string CName { get; set; }
    }
}
