using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datalagring_Oskar_Videll.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DatabasEntitySyntaxChange3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deltagare",
                columns: table => new
                {
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Fornamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Mellannamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Efternamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Telefonnummer = table.Column<string>(type: "character varying(13)", unicode: false, maxLength: 13, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deltagare_Email", x => x.Email);
                    table.CheckConstraint("CK_Deltagare_Email_NotEmpty", "LTRIM(RTRIM('Email')) <> ''");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleEmail = table.Column<string>(type: "text", nullable: false),
                    RoleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles_Email", x => x.RoleEmail);
                });

            migrationBuilder.CreateTable(
                name: "DeltagareRoles",
                columns: table => new
                {
                    DeltagareEmail = table.Column<string>(type: "character varying(255)", nullable: false),
                    RoleEmail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeltagareRoles", x => new { x.DeltagareEmail, x.RoleEmail });
                    table.ForeignKey(
                        name: "FK_DeltagareRoles_Deltagare_DeltagareEmail",
                        column: x => x.DeltagareEmail,
                        principalTable: "Deltagare",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "FK_DeltagareRoles_Roles_RoleEmail",
                        column: x => x.RoleEmail,
                        principalTable: "Roles",
                        principalColumn: "RoleEmail");
                });

            migrationBuilder.CreateIndex(
                name: "UQ_Deltagare_Email",
                table: "Deltagare",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeltagareRoles_RoleEmail",
                table: "DeltagareRoles",
                column: "RoleEmail");

            migrationBuilder.CreateIndex(
                name: "UQ_Roles_RoleName",
                table: "Roles",
                column: "RoleName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeltagareRoles");

            migrationBuilder.DropTable(
                name: "Deltagare");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
