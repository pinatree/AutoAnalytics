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
using AutoAnalytics.WebPortal.Domain;
using AutoAnalytics.WebPortal.Domain.DetailAnalysis;
using Microsoft.EntityFrameworkCore;
using AutoAnalytics.WebPortal.Business;

namespace AutoAnalyticsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubGroupController : ControllerBase
    {
        private DetailAnalysisBusiness _detailAnalysisBusiness;

        public SubGroupController(DetailAnalysisBusiness detailAnalysisBusiness)
        {
            _detailAnalysisBusiness = detailAnalysisBusiness;
        }

        [HttpGet]
        public string[] GetSubGroups(string groupName)
        {
            TGroup group = _detailAnalysisBusiness.GetGroupByName(groupName);

            return _detailAnalysisBusiness.GetSubgroups(group.Id).Select(x => x.CName).ToArray();
        }
    }
}
