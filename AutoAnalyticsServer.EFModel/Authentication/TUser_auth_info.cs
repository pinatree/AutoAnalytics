using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AutoAnalytics.WebPortal.Domain.Authentication
{
    [Table("TUSER_AUTH_INFO", Schema = "authentication")]
    public class TUser_auth_info
    {
        [ForeignKey("TUser")]
        [Key]
        public long TUserId { get; set; }

        public string CPass_hash { get; set; }

        public virtual TUser TUser { get; set; }
    }
}
