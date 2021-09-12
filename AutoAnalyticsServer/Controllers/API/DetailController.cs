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
using AutoAnalytics.WebPortal.Domain.DetailAnalysis;
using AutoAnalytics.WebPortal.DAL.PostgreSQL;
using AutoAnalytics.WebPortal.Business;

namespace AutoAnalyticsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailController : ControllerBase
    {
        private DetailAnalysisBusiness _detailAnalysisBusiness;

        public DetailController(DetailAnalysisBusiness detailAnalysisBusiness)
        {
            _detailAnalysisBusiness = detailAnalysisBusiness;
        }

        [HttpGet]
        public string[] GetDetails(string groupName, string subgroupName)
        {
            TSubgroup subgroup = _detailAnalysisBusiness.GetSubgroupByName(subgroupName);

            return _detailAnalysisBusiness.GetDetails(subgroup.Id).Select(x => x.CName).ToArray();
        }
    }
}
