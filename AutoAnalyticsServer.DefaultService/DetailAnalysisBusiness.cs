using AutoAnalytics.WebPortal.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

using AutoAnalytics.WebPortal.Domain.DetailAnalysis;
using System.Linq;
using AutoAnalytics.WebPortal.DAL.PostgreSQL;
using AutoAnalytics.WebPortal.IBusiness;
using AutoAnalytics.WebPortal.IBusiness.Models;

namespace AutoAnalytics.WebPortal.Business
{
    public class DetailAnalysisBusiness : IDetailAnalysisBusiness
    {
        private AutoAnalyticsPostgresDBContext _dbContext;

        public DetailAnalysisBusiness(AutoAnalyticsPostgresDBContext dbContext)
        {
            if (dbContext == null)
                throw new NullReferenceException("Database context must be not null");

            _dbContext = dbContext;
        }

        public IEnumerable<TGroup> GetGroups()
        {
            return _dbContext.TGroups;
        }

        public TGroup GetGroupByName(string name)
        {
            //Read subgroup
            TGroup subgroup = _dbContext.TGroups.FirstOrDefault(gr => gr.CName == name);

            if (subgroup == null)
            {
                throw new KeyNotFoundException("Group with name '" + name + "' not found");
            }

            return subgroup;
        }

        public IEnumerable<TSubgroup> GetSubgroups(long groupId)
        {
            //Read group
            TGroup group = _dbContext.TGroups.FirstOrDefault(gr => gr.Id == groupId);

            if (group == null)
            {
                throw new KeyNotFoundException("Group with key '" + groupId + "' not found");
            }

            //Load subgroups
            _dbContext.Entry(group).Collection(cntxt => cntxt.TSubgroups).Load();

            //Read details
            ICollection<TSubgroup> foundSubgroups = group.TSubgroups;

            if (foundSubgroups == null || foundSubgroups.Count() == 0)
            {
                throw new KeyNotFoundException("Subgroups for Group with key '" + groupId + "' not found");
            }

            //return details
            return group.TSubgroups;
        }

        public TSubgroup GetSubgroupByName(string name)
        {
            //Read subgroup
            TSubgroup subgroup = _dbContext.TSubgroups.FirstOrDefault(sg => sg.CName == name);

            if (subgroup == null)
            {
                throw new KeyNotFoundException("Subgroup with name '" + name + "' not found");
            }

            return subgroup;
        }

        public IEnumerable<TDetail> GetDetails(long subGroupId)
        {
            //Read subgroup
            TSubgroup subgroup = _dbContext.TSubgroups.FirstOrDefault(sg => sg.Id == subGroupId);

            if(subgroup == null)
            {
                throw new KeyNotFoundException("Subgroup with key '" + subGroupId + "' not found");
            }

            //Load details
            _dbContext.Entry(subgroup).Collection(cntxt => cntxt.TDetails).Load();

            //Read details
            ICollection<TDetail> foundDetails = subgroup.TDetails;

            if (foundDetails == null || foundDetails.Count() == 0)
            {
                throw new KeyNotFoundException("Details for Subgroup with key '" + subGroupId + "' not found");
            }

            //return details
            return foundDetails;
        }

        public IEnumerable<Recommendation> GetRecommendationForDetail(long detailId)
        {
            List<Recommendation> result = new List<Recommendation>();

            //Получаем деталь
            TDetail detail = _dbContext.TDetails.FirstOrDefault(det => det.Id == detailId);

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
