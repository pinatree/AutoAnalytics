using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoAnalyticsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        [HttpGet]
        public string[] GetGroups()
        {
            return new string[]
            {
                "Шасси",
                "Колеса",
                "Стекла",
                "Двигатель"
            };
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SubGroupController : ControllerBase
    {
        [HttpGet]
        public string[] GetSubGroups(string group)
        {
            return new string[]
            {
                "11-" + group,
                "22-" + group,
                "33-" + group,
                "44-" + group,
            };
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class DetailController : ControllerBase
    {
        [HttpGet]
        public string[] GetDetails(string group, string subgroup)
        {
            return new string[]
            {
                "1" + "-" + group + "-" + subgroup,
                "2" + "-" + group + "-" + subgroup,
                "3" + "-" + group + "-" + subgroup,
                "4" + "-" + group + "-" + subgroup
            };
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Recommendation> GetRecommendations(string group, string subgroup, string detail)
        {
            return new List<Recommendation>()
            {
                new Recommendation()
                {
                    Group = "A",
                    Subgroup = "B",
                    Detail = "C"
                },
                new Recommendation()
                {
                    Group = "A1",
                    Subgroup = "B1",
                    Detail = "C1"
                },
                new Recommendation()
                {
                    Group = "A2",
                    Subgroup = "B2",
                    Detail = "C2"
                },
            };
        }

        public class Recommendation
        {
            public string Group { get; set; }
            public string Subgroup { get; set; }
            public string Detail { get; set; }
        }
    }
}
