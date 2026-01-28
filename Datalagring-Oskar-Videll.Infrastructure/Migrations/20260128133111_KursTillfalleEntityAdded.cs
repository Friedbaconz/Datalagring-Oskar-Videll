using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Datalagring_Oskar_Videll.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class KursTillfalleEntityAdded : Migration
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
                name: "Larare",
                columns: table => new
                {
                    LarareEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    fornamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    mellannamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    efternamn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    kompentens = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Larare_LarareEmail", x => x.LarareEmail);
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
                name: "KursTillfalle",
                columns: table => new
                {
                    KurstillfalleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KursKod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Kurskod = table.Column<string>(type: "character varying(50)", nullable: false),
                    MaxSeats = table.Column<int>(type: "integer", nullable: false),
                    Startdatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Slutdatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Ortid = table.Column<int>(type: "integer", nullable: false),
                    OrtId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursTillfalle_KursId", x => x.KurstillfalleId);
                    table.ForeignKey(
                        name: "FK_KursTillfalle_Kurs_Kurskod",
                        column: x => x.KursKod,
                        principalTable: "Kurs",
                        principalColumn: "Kurskod",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KursTillfalle_Kurs_Kurskod1",
                        column: x => x.Kurskod,
                        principalTable: "Kurs",
                        principalColumn: "Kurskod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KursTillfalle_Ort_OrtId",
                        column: x => x.OrtId,
                        principalTable: "Ort",
                        principalColumn: "OrtId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KursTillfalle_Ort_Ortid",
                        column: x => x.Ortid,
                        principalTable: "Ort",
                        principalColumn: "OrtId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KursRegi",
                columns: table => new
                {
                    KurstillfalleId = table.Column<int>(type: "integer", nullable: false),
                    DeltagareEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    KurstillfalleId1 = table.Column<int>(type: "integer", nullable: false),
                    RegiDatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursRegi_KurstillfalleId", x => new { x.KurstillfalleId, x.DeltagareEmail });
                    table.ForeignKey(
                        name: "FK_KursRegi_Deltagare_DeltagareEmail",
                        column: x => x.DeltagareEmail,
                        principalTable: "Deltagare",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KursRegi_KursTillfalle_KurstillfalleId",
                        column: x => x.KurstillfalleId,
                        principalTable: "KursTillfalle",
                        principalColumn: "KurstillfalleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KursRegi_KursTillfalle_KurstillfalleId1",
                        column: x => x.KurstillfalleId1,
                        principalTable: "KursTillfalle",
                        principalColumn: "KurstillfalleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KurstillfalleLarare",
                columns: table => new
                {
                    KurstillfalleId = table.Column<int>(type: "integer", nullable: false),
                    LarareEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    KurstillfalleId1 = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KurstillfalleLarare_KurstillfalleId_LarareEmail", x => new { x.KurstillfalleId, x.LarareEmail });
                    table.ForeignKey(
                        name: "FK_KurstillfalleLarare_KursTillfalle_KurstillfalleId",
                        column: x => x.KurstillfalleId,
                        principalTable: "KursTillfalle",
                        principalColumn: "KurstillfalleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KurstillfalleLarare_KursTillfalle_KurstillfalleId1",
                        column: x => x.KurstillfalleId1,
                        principalTable: "KursTillfalle",
                        principalColumn: "KurstillfalleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KurstillfalleLarare_Larare_LarareEmail",
                        column: x => x.LarareEmail,
                        principalTable: "Larare",
                        principalColumn: "LarareEmail",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_KursRegi_KurstillfalleId1",
                table: "KursRegi",
                column: "KurstillfalleId1");

            migrationBuilder.CreateIndex(
                name: "IX_KursTillfalle_Kurskod",
                table: "KursTillfalle",
                column: "Kurskod");

            migrationBuilder.CreateIndex(
                name: "IX_KursTillfalle_KursKod",
                table: "KursTillfalle",
                column: "KursKod");

            migrationBuilder.CreateIndex(
                name: "IX_KursTillfalle_Ortid",
                table: "KursTillfalle",
                column: "Ortid");

            migrationBuilder.CreateIndex(
                name: "IX_KursTillfalle_OrtId",
                table: "KursTillfalle",
                column: "OrtId");

            migrationBuilder.CreateIndex(
                name: "IX_KurstillfalleLarare_KurstillfalleId1",
                table: "KurstillfalleLarare",
                column: "KurstillfalleId1");

            migrationBuilder.CreateIndex(
                name: "IX_KurstillfalleLarare_LarareEmail",
                table: "KurstillfalleLarare",
                column: "LarareEmail");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KursRegi");

            migrationBuilder.DropTable(
                name: "KurstillfalleLarare");

            migrationBuilder.DropTable(
                name: "KursTillfalle");

            migrationBuilder.DropTable(
                name: "Larare");

            migrationBuilder.DropTable(
                name: "Kurs");

            migrationBuilder.DropTable(
                name: "Ort");
        }
    }
}
