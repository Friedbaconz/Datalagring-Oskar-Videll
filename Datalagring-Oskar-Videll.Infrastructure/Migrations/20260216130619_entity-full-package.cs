using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Datalagring_Oskar_Videll.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class entityfullpackage : Migration
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
                name: "KursRegi",
                columns: table => new
                {
                    IDUQ = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Antagen = table.Column<Guid>(type: "uuid", nullable: false),
                    RegiDatum = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KursRegi_IDUQ", x => new { x.ID, x.Antagen, x.IDUQ });
                    table.ForeignKey(
                        name: "FK_KursRegi_Deltagare_Antagen",
                        column: x => x.Antagen,
                        principalTable: "Deltagare",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KursRegi_KursTillfalle_ID",
                        column: x => x.ID,
                        principalTable: "KursTillfalle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LarareRegi",
                columns: table => new
                {
                    IDUQ = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Larare = table.Column<string>(type: "character varying(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LarareRegi_IDUQ", x => new { x.ID, x.Larare, x.IDUQ });
                    table.ForeignKey(
                        name: "FK_LarareRegi_KursTillfalle_ID",
                        column: x => x.ID,
                        principalTable: "KursTillfalle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LarareRegi_Larare_Larare",
                        column: x => x.Larare,
                        principalTable: "Larare",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_KursRegi_Antagen",
                table: "KursRegi",
                column: "Antagen");

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
                name: "IX_LarareRegi_Larare",
                table: "LarareRegi",
                column: "Larare");

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
                name: "LarareRegi");

            migrationBuilder.DropTable(
                name: "Deltagare");

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
