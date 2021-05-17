using AutoAnalyticsServer.EFModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//using AutoAnalyticsServer.EF;

namespace AutoAnalyticsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private AutoAnalyticsContext _dbContext;

        public GroupController(AutoAnalyticsContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public string[] GetGroups()
        {
            return _dbContext.DetailInfos.Select(x => x.DetGroup).Distinct().ToArray();
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SubGroupController : ControllerBase
    {
        private AutoAnalyticsContext _dbContext;

        public SubGroupController(AutoAnalyticsContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public string[] GetSubGroups(string group)
        {
            return _dbContext.DetailInfos.Where(x => x.DetGroup == group).Select(x => x.DetSubgroup).Distinct().ToArray();
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class DetailController : ControllerBase
    {
        private AutoAnalyticsContext _dbContext;

        public DetailController(AutoAnalyticsContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public string[] GetDetails(string group, string subgroup)
        {
            return _dbContext.DetailInfos.Where(x => x.DetGroup == group && x.DetSubgroup == subgroup).Select(x => x.Detail).Distinct().ToArray();
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private AutoAnalyticsContext _dbContext;

        public RecommendationsController(AutoAnalyticsContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Recommendation> GetRecommendations(string group, string subgroup, string detail)
        {
            var recommendations = _dbContext.AssociationRules.Where(x =>
                x.CauseNavigation.Detail.Trim() == detail.Trim() &&
                x.CauseNavigation.DetSubgroup.Trim() == subgroup.Trim() &&
                x.CauseNavigation.DetGroup.Trim() == group.Trim()).Select(x =>
                    new Recommendation()
                    {
                        Detail = x.СonsequenceNavigation.Detail,
                        Subgroup = x.СonsequenceNavigation.DetSubgroup,
                        Group = x.СonsequenceNavigation.DetGroup,
                        Confidence = x.Confidence
                    });

            return recommendations;
        }

        public class Recommendation
        {
            public string Group { get; set; }
            public string Subgroup { get; set; }
            public string Detail { get; set; }
            public double Confidence { get; set; }
        }
    }
}
