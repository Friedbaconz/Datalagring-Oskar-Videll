using Datalagring_Oskar_Videll.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Datalagring_Oskar_Videll.Infrastructure.Data;

public sealed class DeltagareDBContext(DbContextOptions<DeltagareDBContext> options) : DbContext(options)
{
    public DbSet<DeltagareEntity> Deltagare => Set<DeltagareEntity>();
    public DbSet<Role_Entity> Roles => Set<Role_Entity>();
    public DbSet<StatusTypeEntity> StatusTypes => Set<StatusTypeEntity>();
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

            entity.HasKey(e => e.Email).HasName("PK_Deltagare_Email");

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

            entity.Property(e => e.Telefonnummer)
                .IsUnicode(false)
                .IsRequired(false)
                .HasMaxLength(13);

            entity.HasIndex(e => e.Email, "UQ_Deltagare_Email").IsUnique();

            entity.ToTable(tb => tb.HasCheckConstraint("CK_Deltagare_Email_NotEmpty", "LTRIM(RTRIM('Email')) <> ''"));

            entity.HasOne(d => d.StatusType)
                .WithMany(p => p.Deltagare)
                .HasForeignKey(d => d.StatusTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Deltagare_StatusTypes_StatusTypeId");

        });

        modelBuilder.Entity<Role_Entity>(entity =>
        {
            entity.ToTable("Roles");

            entity.HasKey(e => e.RoleEmail).HasName("PK_Roles_Email");

            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(e => e.RoleName, "UQ_Roles_RoleName").IsUnique();
        });

        modelBuilder.Entity<DeltagareEntity>()
            .HasMany(m => m.Roles)
            .WithMany(r => r.Deltagare)
            .UsingEntity<Dictionary<string, object>>(
                "DeltagareRoles",
                r => r
                    .HasOne<Role_Entity>()
                    .WithMany()
                    .HasForeignKey("RoleEmail")
                    .HasConstraintName("FK_DeltagareRoles_Roles_RoleEmail")
                    .OnDelete(DeleteBehavior.ClientSetNull),
                m => m
                    .HasOne<DeltagareEntity>()
                    .WithMany()
                    .HasForeignKey("DeltagareEmail")
                    .HasConstraintName("FK_DeltagareRoles_Deltagare_DeltagareEmail")
                    .OnDelete(DeleteBehavior.ClientSetNull),
                e =>
                {
                    e.HasKey("DeltagareEmail", "RoleEmail").HasName("PK_DeltagareRoles");
                    e.ToTable("DeltagareRoles");
                }
            );

        modelBuilder.Entity<StatusTypeEntity>(entity =>
        {
            entity.ToTable("StatusTypes");
            entity.HasKey(e => e.Id).HasName("PK_StatusTypes_Id");
            entity.Property(e => e.StatusName)
                .IsRequired()
                .HasMaxLength(50);
            entity.HasIndex(e => e.StatusName, "UQ_StatusTypes_StatusName").IsUnique();


        });

        modelBuilder.Entity<Larare_Entity>(entity =>
        {
            entity.ToTable("Larare");
            entity.HasKey(e => e.LarareEmail).HasName("PK_Larare_LarareEmail");
            entity.Property(e => e.LarareEmail)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.fornamn)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.mellannamn)
                .HasMaxLength(100);
            entity.Property(e => e.efternamn)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.kompentens)
                .IsRequired()
                .HasMaxLength(500);
            entity.HasIndex(e => e.LarareEmail, "UQ_Larare_LarareEmail").IsUnique();
            entity.ToTable(tb => tb.HasCheckConstraint("CK_Larare_LarareEmail_NotEmpty", "LTRIM(RTRIM('LarareEmail')) <> ''"));
        });

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
