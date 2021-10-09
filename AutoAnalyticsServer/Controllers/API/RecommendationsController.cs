/*
 * REST controller
 * 
 * Get recommendations for checking parts based on the group, subgroup, and part found
 * ROUTE: /api/кecommendation
 * INPUT: (group) (subgroup) and (detail)
 * OUTPUT: array of (recommendation)
 */

using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using AutoAnalytics.WebPortal.Domain;
using AutoAnalytics.WebPortal.Domain.DetailAnalysis;
using AutoAnalytics.WebPortal.IBusiness.Models;
using AutoAnalytics.WebPortal.Domain.Contexts;

namespace AutoAnalyticsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private AutoAnalyticsPostgresDBContext _dbContext;

        public RecommendationController(AutoAnalyticsPostgresDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Recommendation> GetRecommendations(string groupName, string subgroupName, string detailName)
        {
            List<Recommendation> result = new List<Recommendation>();

            //Получаем id-шник группы
            TGroup group = _dbContext.TGroups.FirstOrDefault(g => g.CName == groupName);

            _dbContext.Entry(group).Collection(g => g.TSubgroups).Load();

            //Получаем id-шник подгруппы
            TSubgroup subgroup = _dbContext.TSubgroups.FirstOrDefault(sg => sg.CName == subgroupName);

            _dbContext.Entry(subgroup).Collection(s => s.TDetails).Load();
            //Получаем id-шник подгруппы
            TDetail detail = _dbContext.TDetails.FirstOrDefault(det => det.CName == detailName);

            _dbContext.Entry(detail).Collection(s => s.TAssocRuleReasonDetails).Load();
            var detailAssoRules = detail.TAssocRuleReasonDetails;

            foreach (var assocRule in detailAssoRules)
            {
                _dbContext.Entry(assocRule).Reference(s => s.ConseqDetail).Load();
                TDetail assocDetail = assocRule.ConseqDetail;

                _dbContext.Entry(assocDetail).Reference(s => s.Subgroup).Load();
                TSubgroup assocSubgroup = assocDetail.Subgroup;

                _dbContext.Entry(assocSubgroup).Reference(s => s.Group).Load();
                TGroup assocGroup = assocSubgroup.Group;

                Recommendation recommendation = new Recommendation()
                {
                    Detail = assocDetail.CName,
                    Subgroup = assocSubgroup.CName,
                    Group = assocGroup.CName,
                    Confidence = Convert.ToDouble(assocRule.CReliability)
                };

                result.Add(recommendation);
            }

            return result;
        }        
    }
}
