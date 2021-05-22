/*
 * Контекст Entity framework для связи с SQL Server
 */

using AutoAnalyticsServer.DbModel;
using AutoAnalyticsServer.DbModel.DataTypes;
using Microsoft.EntityFrameworkCore;

using System.Linq;

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
                //throw new System.Exception("The database connection was not initialized. Check its presence in the config file.");
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

        public void FillByValues()
        {
            if (DetailInfos.FirstOrDefault() != null)
                return;

            DetailInfos.Add(
                new DetailInfo()
                {
                    DetGroup = "Вентиляция, отопление",
                    DetSubgroup = "Привод управления отопителем",
                    Detail = "Привод в сборе",
                    Id = 2
                });
            DetailInfos.Add(
                new DetailInfo()
                {
                    DetGroup = "Двигатель",
                    DetSubgroup = "Коллекторы выпускные",
                    Detail = "Прокладка коллектора выпускного",
                    Id = 3
                });
            DetailInfos.Add(
                new DetailInfo()
                {
                    DetGroup = "Окно ветровое кабины",
                    DetSubgroup = "Стеклоочиститель и его привод",
                    Detail = "Омыв.элек.5,5-1,6-24",
                    Id = 4
                });
            DetailInfos.Add(
                new DetailInfo()
                {
                    DetGroup = "Ось передня",
                    DetSubgroup = "Ось предняя и поворотный кулак",
                    Detail = "Шайба опорного подшипника",
                    Id = 5
                });
            DetailInfos.Add(
                new DetailInfo()
                {
                    DetGroup = "Система тормозная",
                    DetSubgroup = "Тормоз рабочий передний и тормозной барабан",
                    Detail = "Накладка",
                    Id = 6
                });
            DetailInfos.Add(
                new DetailInfo()
                {
                    DetGroup = "Вентиляция, отопление",
                    DetSubgroup = "Краны системы отопления",
                    Detail = "Кран отопителя кабины",
                    Id = 7
                });
            DetailInfos.Add(
                new DetailInfo()
                {
                    DetGroup = "Колеса и шины",
                    DetSubgroup = "Колеса и ступицы",
                    Detail = "Колесо дисковое",
                    Id = 8
                });
            DetailInfos.Add(
                new DetailInfo()
                {
                    DetGroup = "Приборы",
                    DetSubgroup = "ВК403Б",
                    Detail = "Выключатель света заднего хода",
                    Id = 9
                });
            DetailInfos.Add(
                new DetailInfo()
                {
                    DetGroup = "Приборы",
                    DetSubgroup = "Датчик указателя давления масла",
                    Detail = "Датчик",
                    Id = 10
                });
            DetailInfos.Add(
                new DetailInfo()
                {
                    DetGroup = "Приборы",
                    DetSubgroup = "Манометры",
                    Detail = "Датчик давления",
                    Id = 11
                });
            DetailInfos.Add(
                new DetailInfo()
                {
                    DetGroup = "Система охлаждения",
                    DetSubgroup = "Бачок расширительный",
                    Detail = "Бачок расширительный",
                    Id = 12
                });
            DetailInfos.Add(
                new DetailInfo()
                {
                    DetGroup = "Система охлаждения",
                    DetSubgroup = "Вентилятор и его привод",
                    Detail = "Датчик включения электромагнитной муфты",
                    Id = 13
                });
            DetailInfos.Add(
                new DetailInfo()
                {
                    DetGroup = "Система охлаждения",
                    DetSubgroup = "Насос водяной",
                    Detail = "Ремень",
                    Id = 14
                });
            DetailInfos.Add(
                new DetailInfo()
                {
                    DetGroup = "Система охлаждения",
                    DetSubgroup = "Насос водяной",
                    Detail = "Сальник водян.насоса",
                    Id = 15
                });

            AssociationRules.Add(new AssociationRule() { Cause = 2, Сonsequence = 7, Confidence = 33 });
            AssociationRules.Add(new AssociationRule() { Cause = 3, Сonsequence = 10, Confidence = 21 });
            AssociationRules.Add(new AssociationRule() { Cause = 5, Сonsequence = 6, Confidence = 41 });
            AssociationRules.Add(new AssociationRule() { Cause = 6, Сonsequence = 5, Confidence = 76 });
            AssociationRules.Add(new AssociationRule() { Cause = 9, Сonsequence = 12, Confidence = 60 });
            AssociationRules.Add(new AssociationRule() { Cause = 10, Сonsequence = 11, Confidence = 75 });
            AssociationRules.Add(new AssociationRule() { Cause = 11, Сonsequence = 10, Confidence = 21 });

            SaveChanges();

        }
    }
}
