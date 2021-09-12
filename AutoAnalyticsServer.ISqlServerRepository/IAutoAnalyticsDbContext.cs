using AutoAnalytics.WebPortal.Domain.DetailAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;

namespace AutoAnalytics.WebPortal.IDAL
{
    //Interface for DB context
    public interface IAutoAnalyticsDbContext
    {
        DbSet<TAssocRule> TAssocRules { get; set; }
        DbSet<TCrash> TCrashes { get; set; }
        DbSet<TDetail> TDetails { get; set; }
        DbSet<TGroup> TGroups { get; set; }
        DbSet<TModel> TModels { get; set; }
        DbSet<TRegion> TRegions { get; set; }
        DbSet<TSubgroup> TSubgroups { get; set; }
    }
}
