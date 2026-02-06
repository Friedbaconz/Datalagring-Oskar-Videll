using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Datalagring_Oskar_Videll.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntityFullDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deltagare",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Fornamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Mellannamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Efternamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Telefonnummer = table.Column<string>(type: "character varying(13)", unicode: false, maxLength: 13, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deltagare_ID", x => x.ID);
                    table.CheckConstraint("CK_Deltagare_Email_NotEmpty", "LTRIM(RTRIM('Email')) <> ''");
                });

            migrationBuilder.CreateTable(
                name: "Kurs",
                columns: table => new
                {
                    Kurskod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Kursnamn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Beskrivning = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurs_Kurskod", x => x.Kurskod);
                });

            migrationBuilder.CreateTable(
                name: "KursRegi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Antagen = table.Column<int>(type: "integer", nullable: false),
                    RegiDatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursRegi_ID", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KurstillfalleLarare",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Larare = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KurstillfalleLarare_ID", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Larare",
                columns: table => new
                {
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Fornamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Mellannamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Efternamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Kompentens = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Larare_LarareEmail", x => x.Email);
                    table.CheckConstraint("CK_Larare_LarareEmail_NotEmpty", "LTRIM(RTRIM('LarareEmail')) <> ''");
                });

            migrationBuilder.CreateTable(
                name: "Ort",
                columns: table => new
                {
                    OrtId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrtNamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ort_Ortid", x => x.OrtId);
                });

            migrationBuilder.CreateTable(
                name: "AtagnaKurser",
                columns: table => new
                {
                    DeltagareID = table.Column<Guid>(type: "uuid", nullable: false),
                    KursRegiID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtagnaKurser", x => new { x.DeltagareID, x.KursRegiID });
                    table.ForeignKey(
                        name: "FK_AtagnaKurser_Deltagare_DeltagareID",
                        column: x => x.DeltagareID,
                        principalTable: "Deltagare",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AtagnaKurser_KursRegi_KursRegiID",
                        column: x => x.KursRegiID,
                        principalTable: "KursRegi",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LarareKurser",
                columns: table => new
                {
                    LarareEmail = table.Column<string>(type: "character varying(255)", nullable: false),
                    KurstillfalleLarareEmail = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LarareKurser", x => new { x.LarareEmail, x.KurstillfalleLarareEmail });
                    table.ForeignKey(
                        name: "FK_LarareKurser_KurstillfalleLarare_KurstillfalleLarareEmail",
                        column: x => x.KurstillfalleLarareEmail,
                        principalTable: "KurstillfalleLarare",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LarareKurser_Larare_LarareEmail",
                        column: x => x.LarareEmail,
                        principalTable: "Larare",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "KursTillfalle",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    KursKodID = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MaxSeats = table.Column<int>(type: "integer", nullable: false),
                    Startdatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Slutdatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ortid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursTillfalle_KursId", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KursTillfalle_Kurs_KursKod",
                        column: x => x.KursKodID,
                        principalTable: "Kurs",
                        principalColumn: "Kurskod",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KursTillfalle_Ort_Ortid",
                        column: x => x.Ortid,
                        principalTable: "Ort",
                        principalColumn: "OrtId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegiKursTillfallen",
                columns: table => new
                {
                    KursTillfallenID = table.Column<Guid>(type: "uuid", nullable: false),
                    KursRegiID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegiKursTillfallen", x => new { x.KursTillfallenID, x.KursRegiID });
                    table.ForeignKey(
                        name: "FK_RegiKursTillfallen_KursRegi_KursRegiID",
                        column: x => x.KursRegiID,
                        principalTable: "KursRegi",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegiKursTillfallen_KursTillfalle_KursTillfallenID",
                        column: x => x.KursTillfallenID,
                        principalTable: "KursTillfalle",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "RegiLarareTillfallen",
                columns: table => new
                {
                    KursTillfallenID = table.Column<Guid>(type: "uuid", nullable: false),
                    KurstillfalleLarareID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegiLarareTillfallen", x => new { x.KursTillfallenID, x.KurstillfalleLarareID });
                    table.ForeignKey(
                        name: "FK_RegiLarareTillfallen_KursTillfalle_KursTillfallenID",
                        column: x => x.KursTillfallenID,
                        principalTable: "KursTillfalle",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RegiLarareTillfallen_KurstillfalleLarare_KurstillfalleLarar~",
                        column: x => x.KurstillfalleLarareID,
                        principalTable: "KurstillfalleLarare",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtagnaKurser_KursRegiID",
                table: "AtagnaKurser",
                column: "KursRegiID");

            migrationBuilder.CreateIndex(
                name: "UQ_Deltagare_Email",
                table: "Deltagare",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Kurs_Kursnamn",
                table: "Kurs",
                column: "Kursnamn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KursTillfalle_KursKodID",
                table: "KursTillfalle",
                column: "KursKodID");

            migrationBuilder.CreateIndex(
                name: "IX_KursTillfalle_Ortid",
                table: "KursTillfalle",
                column: "Ortid");

            migrationBuilder.CreateIndex(
                name: "UQ_Larare_LarareEmail",
                table: "Larare",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LarareKurser_KurstillfalleLarareEmail",
                table: "LarareKurser",
                column: "KurstillfalleLarareEmail");

            migrationBuilder.CreateIndex(
                name: "UQ_Ort_Ortnamn",
                table: "Ort",
                column: "OrtNamn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegiKursTillfallen_KursRegiID",
                table: "RegiKursTillfallen",
                column: "KursRegiID");

            migrationBuilder.CreateIndex(
                name: "IX_RegiLarareTillfallen_KurstillfalleLarareID",
                table: "RegiLarareTillfallen",
                column: "KurstillfalleLarareID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtagnaKurser");

            migrationBuilder.DropTable(
                name: "LarareKurser");

            migrationBuilder.DropTable(
                name: "RegiKursTillfallen");

            migrationBuilder.DropTable(
                name: "RegiLarareTillfallen");

            migrationBuilder.DropTable(
                name: "Deltagare");

            migrationBuilder.DropTable(
                name: "Larare");

            migrationBuilder.DropTable(
                name: "KursRegi");

            migrationBuilder.DropTable(
                name: "KursTillfalle");

            migrationBuilder.DropTable(
                name: "KurstillfalleLarare");

            migrationBuilder.DropTable(
                name: "Kurs");

            migrationBuilder.DropTable(
                name: "Ort");
        }
    }
}
