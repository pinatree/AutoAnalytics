/*
 * REST controller
 * 
 * Getting subgroups of parts from a car
 * ROUTE: /api/subgroup
 * INPUT: (detail group)
 * OUTPUT: array of (detail subgroup)
 */

using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using AutoAnalyticsServer.DbModel;
using AutoAnalyticsServer.EFModel;

namespace AutoAnalyticsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubGroupController : ControllerBase
    {
        private IAutoAnalyticsContext _dbContext;

        public SubGroupController(AutoAnalyticsEFContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public string[] GetSubGroups(string group)
        {
            return _dbContext.DetailInfos.
                //filter by detail group
                Where(detailInfo => detailInfo.DetGroup == group)
                //select subgroup
                .Select(detailInfo => detailInfo.DetSubgroup).
                //delete dublicates
                Distinct().
                ToArray();
        }
    }
}
