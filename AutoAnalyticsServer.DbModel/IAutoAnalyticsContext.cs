using AutoAnalyticsServer.DbModel.DataTypes;
using Microsoft.EntityFrameworkCore;

namespace AutoAnalyticsServer.DbModel
{
    public interface IAutoAnalyticsContext
    {
        DbSet<AssociationRule> AssociationRules { get; set; }
        DbSet<DetailInfo> DetailInfos { get; set; }
    }
}
