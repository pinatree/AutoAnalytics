using Microsoft.EntityFrameworkCore;

using AutoAnalytics.WebPortal.Domain.DetailAnalysis;
using AutoAnalytics.WebPortal.Domain.Authentication;

#nullable disable

namespace AutoAnalytics.WebPortal.Domain.Contexts
{
    public partial class AutoAnalyticsPostgresDBContext : DbContext, IAutoAnalyticsDbContext
    {
        public AutoAnalyticsPostgresDBContext()
        {
        }

        public AutoAnalyticsPostgresDBContext(DbContextOptions<AutoAnalyticsPostgresDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TAssocRule> TAssocRules { get; set; }
        public virtual DbSet<TCrash> TCrashes { get; set; }
        public virtual DbSet<TDetail> TDetails { get; set; }
        public virtual DbSet<TGroup> TGroups { get; set; }
        public virtual DbSet<TModel> TModels { get; set; }
        public virtual DbSet<TRegion> TRegions { get; set; }
        public virtual DbSet<TSubgroup> TSubgroups { get; set; }

        public virtual DbSet<TUser> TUsers { get; set; }
        public virtual DbSet<TUser_auth_info> User_Auth_Infos { get; set; }
        public virtual DbSet<TUser_personality> User_Personalities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=192.168.0.107;Database=auto_analytics_dev;Username=postgres;Password=qwe1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("plr")
                .HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<TAssocRule>(entity =>
            {
                entity.ToTable("t_assoc_rule", "auto_analytics");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, 9999999999L, null, null);

                entity.Property(e => e.CCalcDate)
                    .HasColumnType("date")
                    .HasColumnName("c_calc_date");

                entity.Property(e => e.CLift).HasColumnName("c_lift");

                entity.Property(e => e.CReliability).HasColumnName("c_reliability");

                entity.Property(e => e.CSupportCount).HasColumnName("c_support_count");

                entity.Property(e => e.CSupportPerc).HasColumnName("c_support_perc");

                entity.Property(e => e.ConseqDetailId).HasColumnName("conseq_detail_id");

                entity.Property(e => e.ModelId).HasColumnName("model_id");

                entity.Property(e => e.ReasonDetailId).HasColumnName("reason_detail_id");

                entity.Property(e => e.RegionId).HasColumnName("region_id");

                entity.HasOne(d => d.ConseqDetail)
                    .WithMany(p => p.TAssocRuleConseqDetails)
                    .HasForeignKey(d => d.ConseqDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("t_assoc_rule_conseq_detail_id_fkey");

                entity.HasOne(d => d.ReasonDetail)
                    .WithMany(p => p.TAssocRuleReasonDetails)
                    .HasForeignKey(d => d.ReasonDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("t_assoc_rule_reason_detail_id_fkey");
            });

            modelBuilder.Entity<TCrash>(entity =>
            {
                entity.ToTable("t_crash", "auto_analytics");

                entity.HasIndex(e => e.DamageDetailId, "IX_t_crash_damage_detail_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, 9999999999L, null, null);

                entity.Property(e => e.CDate)
                    .HasColumnType("date")
                    .HasColumnName("c_date");

                entity.Property(e => e.CDescription).HasColumnName("c_description");

                entity.Property(e => e.CrashId)
                    .IsRequired()
                    .HasColumnName("crash_id")
                    .HasComment("ComplaintId + \" - \" + EngineNum");

                entity.Property(e => e.DamageDetailId).HasColumnName("damage_detail_id");

                entity.HasOne(d => d.DamageDetail)
                    .WithMany(p => p.TCrashes)
                    .HasForeignKey(d => d.DamageDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detail_dependency");
            });

            modelBuilder.Entity<TDetail>(entity =>
            {
                entity.ToTable("t_detail", "auto_analytics");

                entity.HasIndex(e => e.SubgroupId, "IX_t_detail_subgroup_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, 9999999999L, null, null);

                entity.Property(e => e.CName)
                    .IsRequired()
                    .HasColumnName("c_name");

                entity.Property(e => e.SubgroupId).HasColumnName("subgroup_id");

                entity.HasOne(d => d.Subgroup)
                    .WithMany(p => p.TDetails)
                    .HasForeignKey(d => d.SubgroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subgroup_for_detail");
            });

            modelBuilder.Entity<TGroup>(entity =>
            {
                entity.ToTable("t_group", "auto_analytics");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, 9999999999L, null, null);

                entity.Property(e => e.CName)
                    .IsRequired()
                    .HasColumnName("c_name");
            });

            modelBuilder.Entity<TModel>(entity =>
            {
                entity.ToTable("t_model", "auto_analytics");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, 9999999999L, null, null);

                entity.Property(e => e.CName)
                    .IsRequired()
                    .HasColumnName("c_name");
            });

            modelBuilder.Entity<TRegion>(entity =>
            {
                entity.ToTable("t_region", "auto_analytics");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, 9999999999L, null, null);

                entity.Property(e => e.CName)
                    .IsRequired()
                    .HasColumnName("c_name");
            });

            modelBuilder.Entity<TSubgroup>(entity =>
            {
                entity.ToTable("t_subgroup", "auto_analytics");

                entity.HasIndex(e => e.GroupId, "IX_t_subgroup_group_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L, 9999999999L, null, null);

                entity.Property(e => e.CName)
                    .IsRequired()
                    .HasColumnName("c_name");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TSubgroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("group_for_subgroup");
            });


            modelBuilder.HasSequence("t_group_id_sequence", "auto_analytics")
                .HasMin(0)
                .HasMax(99999999999999999);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
