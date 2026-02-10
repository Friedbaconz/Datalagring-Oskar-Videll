using DatalagringOskarVidell.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatalagringOskarVidell.Infrastructure.Data;

public sealed class DeltagareDBContext(DbContextOptions<DeltagareDBContext> options) : DbContext(options)
{
    public DbSet<DeltagareEntity> Deltagare_Entity => Set<DeltagareEntity>();
    public DbSet<Ort_Entity> Ort => Set<Ort_Entity>();
    public DbSet<Kurstillfalle_Entity> KursTillfalle => Set<Kurstillfalle_Entity>();
    public DbSet<Kurs_Entity> Kurs => Set<Kurs_Entity>();
    public DbSet<KursRegi_Entity> KursRegi => Set<KursRegi_Entity>();
    public DbSet<KurstillfalleLarare_Entity> Larare_Kurstillfalle => Set<KurstillfalleLarare_Entity>();
    public DbSet<Larare_Entity> Larare => Set<Larare_Entity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DeltagareEntity>(entity =>
        {

            entity.ToTable("Deltagare");

            entity.HasKey(e => e.ID).HasName("PK_Deltagare_ID");

            entity.Property(e => e.ID)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Fornamn)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Mellannamn)
                .HasMaxLength(100);

            entity.Property(e => e.Efternamn)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Telefonnummer)
                .IsUnicode(false)
                .IsRequired(false)
                .HasMaxLength(13);

            entity.HasIndex(e => e.Email, "UQ_Deltagare_Email").IsUnique();

            entity.ToTable(tb => tb.HasCheckConstraint("CK_Deltagare_Email_NotEmpty", "LTRIM(RTRIM('Email')) <> ''"));

        });

        modelBuilder.Entity<Larare_Entity>(entity =>
        {
            entity.ToTable("Larare");
            entity.HasKey(e => e.Email).HasName("PK_Larare_LarareEmail");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Fornamn)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Mellannamn)
                .HasMaxLength(100);
            entity.Property(e => e.Efternamn)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Kompentens)
                .IsRequired()
                .HasMaxLength(500);

            entity.HasIndex(e => e.Email, "UQ_Larare_LarareEmail").IsUnique();

            entity.ToTable(tb => tb.HasCheckConstraint("CK_Larare_LarareEmail_NotEmpty", "LTRIM(RTRIM('LarareEmail')) <> ''"));

        });

        modelBuilder.Entity<Kurstillfalle_Entity>(entity =>
        {
            entity.ToTable("KursTillfalle");
            entity.HasKey(e => e.ID).HasName("PK_KursTillfalle_KursId");
            entity.Property(e => e.ID);
            entity.Property(e => e.KursKodID)
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

            entity.HasOne(e => e.Kurs)
                .WithMany(r => r.Kurstillfallen)
                .HasForeignKey(e => e.KursKodID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_KursTillfalle_Kurs_KursKod");

            entity.HasOne(e => e.Ort)
                .WithMany(r => r.Kurstillfallen)
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
            entity.Property(e => e.OrtId)
                .IsRequired();
            entity.Property(e => e.OrtNamn)
                .IsRequired()
                .HasMaxLength(100);
            entity.HasIndex(e => e.OrtNamn, "UQ_Ort_Ortnamn").IsUnique();
        });

        modelBuilder.Entity<Kurstillfalle_Entity>()
            .HasMany(m => m.KursTillfallenLarare)
            .WithMany(r => r.KurstillfalleLarare)
            .UsingEntity<KurstillfalleLarare_Entity>(
                r => r.HasOne(e => e.LarareRegi).WithMany().HasForeignKey(e => e.Larare).OnDelete(DeleteBehavior.ClientSetNull),
                m => m.HasOne(e => e.Kurstillfallen).WithMany().HasForeignKey(e => e.ID).OnDelete(DeleteBehavior.ClientSetNull),
                e =>
                {
                    e.ToTable("LarareRegi");
                    e.HasKey(e => new { e.ID, e.Larare }).HasName("PK_RegiLarare_ID_Larare");
                    e.Property(e => e.Larare)
                        .IsRequired();
                    e.Property(e => e.ID)
                        .IsRequired();
                }
            );

        modelBuilder.Entity<Kurstillfalle_Entity>()
            .HasMany(m => m.KursRegi)
            .WithMany(r => r.KursRegiDeltagare)
            .UsingEntity<KursRegi_Entity>(
                r => r.HasOne(e => e.DeltagareRegi).WithMany().HasForeignKey(e => e.Antagen).OnDelete(DeleteBehavior.ClientSetNull),
                m => m.HasOne(e => e.Kurstillfallen).WithMany().HasForeignKey(e => e.ID).OnDelete(DeleteBehavior.ClientSetNull),
                e => 
                {
                    e.ToTable("KursRegi");
                    e.HasKey( e => new { e.ID, e.Antagen }).HasName("PK_RegiDeltagare_ID_Antagen");
                    e.Property(e => e.ID)
                        .IsRequired();
                    e.Property(e => e.Antagen)
                        .IsRequired();
                    e.Property(e => e.status)
                        .HasMaxLength(200);
                    e.Property(e => e.RegiDatum)
                        .IsRequired();
                }
            );


    }

}
