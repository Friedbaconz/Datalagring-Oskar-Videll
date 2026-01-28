

using Datalagring_Oskar_Videll.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Datalagring_Oskar_Videll.Infrastructure.Data;

public sealed class KursDBContext(DbContextOptions<KursDBContext> options) : DbContext(options)
{
    public DbSet<Ort_Entity> Ort => Set<Ort_Entity>();
    public DbSet<Kurstillfalle_Entity> KursTillfalle => Set<Kurstillfalle_Entity>();
    public DbSet<Kurs_Entity> Kurs => Set<Kurs_Entity>();
    public DbSet<KursRegi_Entity> KursRegi => Set<KursRegi_Entity>();
    public DbSet<KurstillfalleLarare_Entity> Larare_Kurstillfalle => Set<KurstillfalleLarare_Entity>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kurstillfalle_Entity>(entity =>
        {
            entity.ToTable("KursTillfalle");
            entity.HasKey(e => e.KurstillfalleId).HasName("PK_KursTillfalle_KursId");
            entity.Property(e => e.KurstillfalleId);
            entity.Property(e => e.KursKod)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Ortid)
                .IsRequired();
            entity.Property(e => e.MaxSeats)
                .IsRequired();
            entity.Property(e => e.Startdatum)
                .IsRequired();
            entity.Property(e => e.Slutdatum)
                .IsRequired();

            entity.HasOne<Kurs_Entity>()
                .WithMany()
                .HasForeignKey(e => e.KursKod)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_KursTillfalle_Kurs_Kurskod");

            entity.HasOne<Ort_Entity>()
                .WithMany()
                .HasForeignKey(e => e.Ortid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_KursTillfalle_Ort_Ortid");

        });

        modelBuilder.Entity<Kurs_Entity>(entity =>
        {
            entity.ToTable("Kurs");
            entity.HasKey(e => e.Kurskod).HasName("PK_Kurs_Kurskod");
            entity.Property(e => e.Kurskod)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Kursnamn)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Beskrivning)
                .IsRequired()
                .HasMaxLength(1000);
            entity.HasIndex(e => e.Kursnamn, "UQ_Kurs_Kursnamn").IsUnique();
        });

        modelBuilder.Entity<Ort_Entity>(entity =>
        {
            entity.ToTable("Ort");
            entity.HasKey(e => e.OrtId).HasName("PK_Ort_Ortid");
            entity.Property(e => e.OrtNamn)
                .IsRequired()
                .HasMaxLength(100);
            entity.HasIndex(e => e.OrtNamn, "UQ_Ort_Ortnamn").IsUnique();
        });

        modelBuilder.Entity<KursRegi_Entity>(entity =>
        {
            entity.ToTable("KursRegi");
            entity.HasKey(e => new { e.KurstillfalleId, e.DeltagareEmail }).HasName("PK_KursRegi_KurstillfalleId");
            entity.Property(e => e.KurstillfalleId)
                .IsRequired();
            entity.Property(e => e.DeltagareEmail)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.RegiDatum)
                .IsRequired();
            entity.Property(e => e.status)
                .IsRequired()
                .HasMaxLength(50);

        });

        modelBuilder.Entity<KurstillfalleLarare_Entity>(entity =>
        {
            entity.ToTable("KurstillfalleLarare");
            entity.HasKey(e => new { e.KurstillfalleId, e.LarareEmail }).HasName("PK_KurstillfalleLarare_KurstillfalleId_LarareEmail");
            entity.Property(e => e.KurstillfalleId)
                .IsRequired();
            entity.Property(e => e.LarareEmail)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Kurstillfalle_Entity>()
            .HasMany(k => k.KursRegi)
            .WithOne()
            .HasForeignKey(kr => kr.KurstillfalleId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_KursRegi_KursTillfalle_KurstillfalleId");

        modelBuilder.Entity<Kurstillfalle_Entity>()
            .HasMany(k => k.KurstillfalleLarare)
            .WithOne()
            .HasForeignKey(kl => kl.KurstillfalleId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_KurstillfalleLarare_KursTillfalle_KurstillfalleId");
    }

}
