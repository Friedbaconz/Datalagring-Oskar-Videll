using Datalagring_Oskar_Videll.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Datalagring_Oskar_Videll.Infrastructure.Data;

public sealed class DeltagareDBContext(DbContextOptions<DeltagareDBContext> options) : DbContext(options)
{
    public DbSet<DeltagareEntity> Deltagare => Set<DeltagareEntity>();
    public DbSet<Role_Entity> Roles => Set<Role_Entity>();
    public DbSet<StatusTypeEntity> StatusTypes => Set<StatusTypeEntity>();

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

    }
}
