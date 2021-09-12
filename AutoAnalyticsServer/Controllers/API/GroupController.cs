/*
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
using AutoAnalytics.WebPortal.Domain;
using AutoAnalytics.WebPortal.DAL.PostgreSQL;
using AutoAnalytics.WebPortal.Business;

namespace AutoAnalyticsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private DetailAnalysisBusiness _detailAnalysisBusiness;

        public GroupController(DetailAnalysisBusiness detailAnalysisBusiness)
        {
            _detailAnalysisBusiness = detailAnalysisBusiness;
        }

        [HttpGet]
        public string[] GetGroups()
        {
            return _detailAnalysisBusiness.GetGroups().Select(x => x.CName).ToArray();
        }
    }
}
