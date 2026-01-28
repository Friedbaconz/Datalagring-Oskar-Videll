using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Datalagring_Oskar_Videll.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StatusTypeEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusTypeId",
                table: "Deltagare",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StatusTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusTypes_Id", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deltagare_StatusTypeId",
                table: "Deltagare",
                column: "StatusTypeId");

            migrationBuilder.CreateIndex(
                name: "UQ_StatusTypes_StatusName",
                table: "StatusTypes",
                column: "StatusName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deltagare_StatusTypes_StatusTypeId",
                table: "Deltagare",
                column: "StatusTypeId",
                principalTable: "StatusTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deltagare_StatusTypes_StatusTypeId",
                table: "Deltagare");

            migrationBuilder.DropTable(
                name: "StatusTypes");

            migrationBuilder.DropIndex(
                name: "IX_Deltagare_StatusTypeId",
                table: "Deltagare");

            migrationBuilder.DropColumn(
                name: "StatusTypeId",
                table: "Deltagare");
        }
    }
}
