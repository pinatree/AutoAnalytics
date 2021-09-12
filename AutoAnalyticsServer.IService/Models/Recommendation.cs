using System;
using System.Collections.Generic;
using System.Text;

namespace AutoAnalytics.WebPortal.IBusiness.Models
{
    [Serializable]
    public class Recommendation
    {
        public string Detail { get; set; }
        public string Subgroup { get; set; }
        public string Group { get; set; }
        public double Confidence { get; set; }
    }
}
