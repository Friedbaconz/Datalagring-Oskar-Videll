using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datalagring_Oskar_Videll.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class entityframeworktest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "KurstillfalleLarare",
                columns: table => new
                {
                    KursTillfallenId = table.Column<int>(type: "integer", nullable: false),
                    LarareEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KurstillfalleLarare_KurstillfalleId_LarareEmail", x => new { x.KursTillfallenId, x.LarareEmail });
                });

            migrationBuilder.CreateTable(
                name: "Ort",
                columns: table => new
                {
                    OrtId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrtNamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ort_Ortid", x => x.OrtId);
                });

            migrationBuilder.CreateTable(
                name: "StatusTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusTypes_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Larare",
                columns: table => new
                {
                    LarareEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Fornamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Mellannamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Efternamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Kompentens = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    KurstillfalleLarare_EntityKursTillfallenId = table.Column<int>(type: "integer", nullable: true),
                    KurstillfalleLarare_EntityLarareEmail = table.Column<string>(type: "character varying(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Larare_LarareEmail", x => x.LarareEmail);
                    table.CheckConstraint("CK_Larare_LarareEmail_NotEmpty", "LTRIM(RTRIM('LarareEmail')) <> ''");
                    table.ForeignKey(
                        name: "FK_Larare_KurstillfalleLarare_KurstillfalleLarare_EntityKursTi~",
                        columns: x => new { x.KurstillfalleLarare_EntityKursTillfallenId, x.KurstillfalleLarare_EntityLarareEmail },
                        principalTable: "KurstillfalleLarare",
                        principalColumns: new[] { "KursTillfallenId", "LarareEmail" });
                });

            migrationBuilder.CreateTable(
                name: "KursTillfalle",
                columns: table => new
                {
                    KursTillfallenId = table.Column<Guid>(type: "uuid", nullable: false),
                    KursKod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MaxSeats = table.Column<int>(type: "integer", nullable: false),
                    Startdatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Slutdatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ortid = table.Column<Guid>(type: "uuid", nullable: false),
                    LarareEmail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursTillfalle_KursId", x => x.KursTillfallenId);
                    table.ForeignKey(
                        name: "FK_KursTillfalle_Kurs_KursKod",
                        column: x => x.KursKod,
                        principalTable: "Kurs",
                        principalColumn: "Kurskod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KursTillfalle_Ort_Ortid",
                        column: x => x.Ortid,
                        principalTable: "Ort",
                        principalColumn: "OrtId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deltagare",
                columns: table => new
                {
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Concurrency = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false),
                    Fornamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Mellannamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Efternamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Telefonnummer = table.Column<string>(type: "character varying(13)", unicode: false, maxLength: 13, nullable: true),
                    StatusTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deltagare_Email", x => x.Email);
                    table.CheckConstraint("CK_Deltagare_Email_NotEmpty", "LTRIM(RTRIM('Email')) <> ''");
                    table.ForeignKey(
                        name: "FK_Deltagare_StatusTypes_StatusTypeId",
                        column: x => x.StatusTypeId,
                        principalTable: "StatusTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KursRegi",
                columns: table => new
                {
                    KursRegiId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeltagareEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    RegiDatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    Kurs_EntityKurskod = table.Column<string>(type: "character varying(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursRegi_KursRegiId", x => x.KursRegiId);
                    table.ForeignKey(
                        name: "FK_Deltagare_KursRegi_DeltagareEmail",
                        column: x => x.DeltagareEmail,
                        principalTable: "Deltagare",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KursRegi_Kurs_Kurs_EntityKurskod",
                        column: x => x.Kurs_EntityKurskod,
                        principalTable: "Kurs",
                        principalColumn: "Kurskod");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deltagare_StatusTypeId",
                table: "Deltagare",
                column: "StatusTypeId");

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
                name: "IX_KursRegi_DeltagareEmail",
                table: "KursRegi",
                column: "DeltagareEmail");

            migrationBuilder.CreateIndex(
                name: "IX_KursRegi_Kurs_EntityKurskod",
                table: "KursRegi",
                column: "Kurs_EntityKurskod");

            migrationBuilder.CreateIndex(
                name: "IX_KursTillfalle_KursKod",
                table: "KursTillfalle",
                column: "KursKod");

            migrationBuilder.CreateIndex(
                name: "IX_KursTillfalle_Ortid",
                table: "KursTillfalle",
                column: "Ortid");

            migrationBuilder.CreateIndex(
                name: "IX_Larare_KurstillfalleLarare_EntityKursTillfallenId_Kurstillf~",
                table: "Larare",
                columns: new[] { "KurstillfalleLarare_EntityKursTillfallenId", "KurstillfalleLarare_EntityLarareEmail" });

            migrationBuilder.CreateIndex(
                name: "UQ_Larare_LarareEmail",
                table: "Larare",
                column: "LarareEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Ort_Ortnamn",
                table: "Ort",
                column: "OrtNamn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_StatusTypes_StatusName",
                table: "StatusTypes",
                column: "StatusName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KursRegi");

            migrationBuilder.DropTable(
                name: "KursTillfalle");

            migrationBuilder.DropTable(
                name: "Larare");

            migrationBuilder.DropTable(
                name: "Deltagare");

            migrationBuilder.DropTable(
                name: "Kurs");

            migrationBuilder.DropTable(
                name: "Ort");

            migrationBuilder.DropTable(
                name: "KurstillfalleLarare");

            migrationBuilder.DropTable(
                name: "StatusTypes");
        }
    }
}
