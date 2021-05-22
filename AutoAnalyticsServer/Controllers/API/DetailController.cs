/*
 * REST controller
 * 
 * Getting car parts from the database by group and subgroup
 * ROUTE: /api/detail
 * INPUT: (group) and (subgroup)
 * OUTPUT: array of (detail)
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
    public class DetailController : ControllerBase
    {
        private IAutoAnalyticsContext _dbContext;

        public DetailController(AutoAnalyticsEFContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public string[] GetDetails(string group, string subgroup)
        {
            //В идеале конечно, тут нужно обращаться к буфферу, надо будет подумать над этим...

            return _dbContext.DetailInfos.
                //filter by group and subgroup
                Where(detailInfo => detailInfo.DetGroup == group && detailInfo.DetSubgroup == subgroup).
                //select detail
                Select(detailInfo => detailInfo.Detail).
                //remove dublicates
                Distinct().
                ToArray();
        }
    }
}
