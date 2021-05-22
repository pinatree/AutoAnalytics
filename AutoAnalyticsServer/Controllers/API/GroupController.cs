﻿/*
 * REST controller
 * 
 * Getting subgroups of car parts
 * ROUTE: /api/group
 * INPUT: -
 * OUTPUT: array of (detail group)
 */

using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using AutoAnalyticsServer.DbModel;
using AutoAnalyticsServer.SqlServerEFModel;

namespace AutoAnalyticsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private IAutoAnalyticsContext _dbContext;

        public GroupController(AutoAnalyticsSqlServerEFContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public string[] GetGroups()
        {
            return _dbContext.DetailInfos.
                //select detail groups from db
                Select(detailInfo => detailInfo.DetGroup).
                //remove dublicates
                Distinct().ToArray();
        }
    }
}
