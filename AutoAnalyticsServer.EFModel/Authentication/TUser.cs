using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AutoAnalytics.WebPortal.Domain.Authentication
{
    [Table("TUSER", Schema = "authentication")]
    public partial class TUser
    {
        public long Id { get; set; }
        public string CLogin { get; set; }
        public DateTime CRegistration { get; set; }

        public virtual TUser_personality TUser_personality { get; set; }
        public virtual TUser_auth_info TUser_Auth_Info { get; set; }
    }
}
