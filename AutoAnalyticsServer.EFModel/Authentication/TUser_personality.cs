using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AutoAnalytics.WebPortal.Domain.Authentication
{
    [Table("TUSER_PERSONALITY", Schema = "authentication")]
    public class TUser_personality
    {
        [ForeignKey("TUser")]
        [Key]
        public long TUserId { get; set; }

        public string CName { get; set; }
        public string CSurname { get; set; }
        public string CPatronimyc { get; set; }

        //должность. Чисто описательный реквизит, заниматься кадровым учетом - не наша задача. Просто выгрузим строку из другой БД.
        public string CPosition { get; set; }

        public virtual TUser TUser { get; set; }
    }
}
