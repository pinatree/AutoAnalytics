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
using AutoAnalytics.WebPortal.DAL.PostgreSQL;
using AutoAnalytics.WebPortal.IBusiness.Models;

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

            IQueryable<TAssocRule> detailAssoRules = _dbContext.TAssocRules.Where(ar => ar.ReasonDetailId == detail.Id);

            foreach (var assocRule in detailAssoRules)
            {
                TDetail assocDetail = _dbContext.TDetails.First(x => x.Id == assocRule.ConseqDetailId);

                Recommendation recommendation = new Recommendation()
                {
                    Detail = assocDetail.CName,
                    Subgroup = assocDetail.Subgroup.CName,
                    Group = assocDetail.Subgroup.Group.CName,
                    Confidence = Convert.ToDouble(assocRule.CReliability)
                };

                result.Add(recommendation);
            }

            return result;
        }        
    }
}
