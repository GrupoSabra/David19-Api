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
                    HashId = table.Column<string>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CitizenAddress = table.Column<string>(nullable: true),
                    SubjectHashId = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    CredentialCreation = table.Column<DateTime>(nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    Location = table.Column<Point>(type: "geometry(POINT, 4326)", nullable: true),
                    Lat = table.Column<double>(nullable: false),
                    Lon = table.Column<double>(nullable: false),
                    CredintialType = table.Column<int>(nullable: false),
                    Reason = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredCredentials", x => x.HashId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisteredCredentials");
        }
    }
}
