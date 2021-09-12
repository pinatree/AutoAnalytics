using System;
using System.Collections;
using System.Collections.Generic;

using AutoAnalytics.WebPortal.Domain.DetailAnalysis;
using AutoAnalytics.WebPortal.IBusiness.Models;

namespace AutoAnalytics.WebPortal.IBusiness
{
    public interface IDetailAnalysisBusiness
    {
        IEnumerable<TGroup> GetGroups();

        IEnumerable<TSubgroup> GetSubgroups(long groupId);

        IEnumerable<TDetail> GetDetails(long subGroupId);

        IEnumerable<Recommendation> GetRecommendationForDetail(long detailId);
    }
}
