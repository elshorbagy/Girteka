using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Repository.DBContext
{
    public partial class SQLDBContext : DbContext
    {
        public SQLDBContext()
        {
        }

        public SQLDBContext(DbContextOptions<SQLDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Electricity> Electricities { get; set; } = null!;
        public virtual DbSet<Region> Regions { get; set; } = null!;
        //public virtual DbSet<VwElectricityDatum> VwElectricityDatum { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Electricity>(entity =>
            {
                entity.HasKey(x=>x.Id);

                entity.ToTable("Electricity");

                entity.Property(e => e.Pavadinimas)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Plt)
                    .HasColumnType("datetime")
                    .HasColumnName("PLT");

                entity.Property(e => e.Pminusas)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PMinusas");

                entity.Property(e => e.Ppliusas).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Tipas)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");

                entity.Property(e => e.RegionId).ValueGeneratedNever();

                entity.Property(e => e.RegionName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
