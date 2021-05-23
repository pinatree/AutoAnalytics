/*
 * REST controller
 * 
 * Get recommendations for checking parts based on the group, subgroup, and part found
 * ROUTE: /api/кecommendation
 * INPUT: (group) (subgroup) and (detail)
 * OUTPUT: array of (recommendation)
 */

using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using AutoAnalyticsServer.DbModel;

namespace AutoAnalyticsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private IAutoAnalyticsContext _dbContext;

        public RecommendationController(IAutoAnalyticsContext dbContext)
        {
            _dbContext = dbContext;
            dbContext.FillByValues();
        }

        [HttpGet]
        public IEnumerable<Recommendation> GetRecommendations(string group, string subgroup, string detail)
        {
            string searchGroup = group.Trim();
            string searchSubgroup = subgroup.Trim();
            string searchDetail = detail.Trim();

            var recommendations = _dbContext.AssociationRules.Where(x =>
                //where cause detail is our detail
                x.CauseNavigation.DetGroup == searchGroup &&
                //cause subgroup is our subgroup
                x.CauseNavigation.DetSubgroup == searchSubgroup &&
                //cause group is our group
                x.CauseNavigation.Detail == searchDetail).
                    //formation of a recommendation
                    Select(rule =>
                        new Recommendation()
                        {
                            //get group from association rule consequence
                            Group = rule.СonsequenceNavigation.DetGroup,
                            //get subgroup from association rule consequence
                            Subgroup = rule.СonsequenceNavigation.DetSubgroup,
                            //get detail from association rule consequence
                            Detail = rule.СonsequenceNavigation.Detail,
                            //get confidence
                            Confidence = rule.Confidence
                        });

            return recommendations;
        }

        public class Recommendation
        {
            public string Detail { get; set; }
            public string Subgroup { get; set; }
            public string Group { get; set; }
            public double Confidence { get; set; }
        }
    }
}
