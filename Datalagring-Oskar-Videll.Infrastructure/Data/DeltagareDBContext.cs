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

        modelBuilder.Entity<KursRegi_Entity>(entity =>
        {
            entity.ToTable("KursRegi");
            entity.HasKey(e => e.Antagen).HasName("PK_KursRegi_Antagen"); ;
            entity.HasKey(e => e.ID).HasName("PK_KursRegi_ID"); ;
            entity.Property(e => e.ID)
                  .IsRequired();
            entity.Property(e => e.Antagen)
                  .IsRequired();
            entity.Property(e => e.status)
                .HasMaxLength(100)
                .IsRequired();
            entity.Property(e => e.RegiDatum)
                .IsRequired();
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

        modelBuilder.Entity<KurstillfalleLarare_Entity>(entity =>
        {
            entity.ToTable("KurstillfalleLarare");
            entity.HasKey(e => e.Larare).HasName("PK_KurstillfalleLarare_Email");
            entity.HasKey(e => e.ID).HasName("PK_KurstillfalleLarare_ID");
            entity.Property(e => e.ID)
                .IsRequired();
            entity.Property(e => e.Larare)
                .HasMaxLength(255)
                .IsRequired ();

        });

        modelBuilder.Entity<Kurstillfalle_Entity>()
            .HasMany(m => m.KursTillfallenLarare)
            .WithMany(r => r.Kurstillfallen)
            .UsingEntity<Dictionary<string, string>>(
                "RegistreradLarare",
                r => r.HasOne<KurstillfalleLarare_Entity>().WithMany().HasForeignKey("KurstillfalleLarareID").OnDelete(DeleteBehavior.ClientSetNull),
                m => m.HasOne<Kurstillfalle_Entity>().WithMany().HasForeignKey("KursTillfallenID").OnDelete(DeleteBehavior.ClientSetNull),
                e =>
                {
                    e.HasKey("KursTillfallenID", "KurstillfalleLarareID");
                    e.ToTable("RegiLarareTillfallen");
                }
            );

        modelBuilder.Entity<Kurstillfalle_Entity>()
            .HasMany(m => m.KursRegi)
            .WithMany(r => r.Kurstillfallen)
            .UsingEntity<Dictionary<string, string>>(
                "RegistreringsTillfalle",
                r => r.HasOne<KursRegi_Entity>().WithMany().HasForeignKey("KursRegiID").OnDelete(DeleteBehavior.ClientSetNull),
                m => m.HasOne<Kurstillfalle_Entity>().WithMany().HasForeignKey("KursTillfallenID").OnDelete(DeleteBehavior.ClientSetNull),
                e =>
                {
                    e.HasKey("KursTillfallenID", "KursRegiID");
                    e.ToTable("RegiKursTillfallen");
                }
            );


        modelBuilder.Entity<DeltagareEntity>()
            .HasMany(m => m.Kursregi)
            .WithMany(r => r.DeltagareRegi)
            .UsingEntity<Dictionary<string, string>>(
                "AntagenKurs",
                r => r.HasOne<KursRegi_Entity>().WithMany().HasForeignKey("KursRegiID").OnDelete(DeleteBehavior.ClientSetNull),
                m => m.HasOne<DeltagareEntity>().WithMany().HasForeignKey("DeltagareID").OnDelete(DeleteBehavior.ClientSetNull),
                e =>
                {
                    e.HasKey("DeltagareID", "KursRegiID");
                    e.ToTable("AtagnaKurser");
                }
            );

        modelBuilder.Entity<Larare_Entity>()
            .HasMany(m => m.KurstillfalleLarare)
            .WithMany(r => r.LarareRegi)
            .UsingEntity<Dictionary<string, string>>(
                "KursLarare",
                r => r.HasOne<KurstillfalleLarare_Entity>().WithMany().HasForeignKey("KurstillfalleLarareEmail").OnDelete(DeleteBehavior.ClientSetNull),
                m => m.HasOne<Larare_Entity>().WithMany().HasForeignKey("LarareEmail").OnDelete(DeleteBehavior.ClientSetNull),
                e =>
                {
                    e.HasKey("LarareEmail", "KurstillfalleLarareEmail");
                    e.ToTable("LarareKurser");
                }
            );


    }

}
