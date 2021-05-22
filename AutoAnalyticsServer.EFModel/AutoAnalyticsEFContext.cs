/*
 * Контекст Entity framework для связи с SQL Server
 */

using AutoAnalyticsServer.DbModel;
using AutoAnalyticsServer.DbModel.DataTypes;
using Microsoft.EntityFrameworkCore;

namespace AutoAnalyticsServer.SqlServerEFModel
{
    public partial class AutoAnalyticsSqlServerEFContext : DbContext, IAutoAnalyticsContext
    {
        public AutoAnalyticsSqlServerEFContext()
        {
        }

        public AutoAnalyticsSqlServerEFContext(DbContextOptions<AutoAnalyticsSqlServerEFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssociationRule> AssociationRules { get; set; }
        public virtual DbSet<DetailInfo> DetailInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-NF5SOVM\\SQLEXPRESS;Database=AutoAnalytics;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<AssociationRule>(entity =>
            {
                entity.HasKey(e => new { e.Cause, e.Сonsequence });

                entity.ToTable("AssociationRule");

                entity.HasOne(d => d.CauseNavigation)
                    .WithMany(p => p.AssociationRuleCauseNavigations)
                    .HasForeignKey(d => d.Cause)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociationRule_DetailInfo");

                entity.HasOne(d => d.СonsequenceNavigation)
                    .WithMany(p => p.AssociationRuleСonsequenceNavigations)
                    .HasForeignKey(d => d.Сonsequence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AssociationRule_DetailInfo1");
            });

            modelBuilder.Entity<DetailInfo>(entity =>
            {
                entity.ToTable("DetailInfo");

                entity.Property(e => e.DetGroup)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.DetSubgroup)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Detail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
