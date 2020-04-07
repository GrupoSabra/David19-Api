using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CovidLAMap.Data.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "RegisteredCredentials",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HashId = table.Column<string>(nullable: true),
                    CitizenAddress = table.Column<string>(nullable: true),
                    SubjectHashId = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    CredentialCreation = table.Column<DateTime>(nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    Location = table.Column<Point>(nullable: true),
                    OriginalLocation = table.Column<string>(nullable: true),
                    CredintialType = table.Column<int>(nullable: false),
                    Reason = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredCredentials", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisteredCredentials");
        }
    }
}
