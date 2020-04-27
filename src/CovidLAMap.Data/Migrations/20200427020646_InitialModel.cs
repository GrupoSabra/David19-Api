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
                name: "Countries",
                columns: table => new
                {
                    gid = table.Column<int>(nullable: false, defaultValueSql: "nextval('countries_gid_seq'::regclass)"),
                    featurecla = table.Column<string>(maxLength: 15, nullable: true),
                    scalerank = table.Column<short>(nullable: true),
                    labelrank = table.Column<short>(nullable: true),
                    sovereignt = table.Column<string>(maxLength: 32, nullable: true),
                    sov_a3 = table.Column<string>(maxLength: 3, nullable: true),
                    adm0_dif = table.Column<short>(nullable: true),
                    level = table.Column<short>(nullable: true),
                    type = table.Column<string>(maxLength: 17, nullable: true),
                    admin = table.Column<string>(maxLength: 36, nullable: true),
                    adm0_a3 = table.Column<string>(maxLength: 3, nullable: true),
                    geou_dif = table.Column<short>(nullable: true),
                    geounit = table.Column<string>(maxLength: 36, nullable: true),
                    gu_a3 = table.Column<string>(maxLength: 3, nullable: true),
                    su_dif = table.Column<short>(nullable: true),
                    subunit = table.Column<string>(maxLength: 36, nullable: true),
                    su_a3 = table.Column<string>(maxLength: 3, nullable: true),
                    brk_diff = table.Column<short>(nullable: true),
                    name = table.Column<string>(maxLength: 25, nullable: true),
                    name_long = table.Column<string>(maxLength: 36, nullable: true),
                    brk_a3 = table.Column<string>(maxLength: 3, nullable: true),
                    brk_name = table.Column<string>(maxLength: 32, nullable: true),
                    brk_group = table.Column<string>(maxLength: 17, nullable: true),
                    abbrev = table.Column<string>(maxLength: 13, nullable: true),
                    postal = table.Column<string>(maxLength: 4, nullable: true),
                    formal_en = table.Column<string>(maxLength: 52, nullable: true),
                    formal_fr = table.Column<string>(maxLength: 35, nullable: true),
                    name_ciawf = table.Column<string>(maxLength: 45, nullable: true),
                    note_adm0 = table.Column<string>(maxLength: 22, nullable: true),
                    note_brk = table.Column<string>(maxLength: 63, nullable: true),
                    name_sort = table.Column<string>(maxLength: 36, nullable: true),
                    name_alt = table.Column<string>(maxLength: 19, nullable: true),
                    mapcolor7 = table.Column<short>(nullable: true),
                    mapcolor8 = table.Column<short>(nullable: true),
                    mapcolor9 = table.Column<short>(nullable: true),
                    mapcolor13 = table.Column<short>(nullable: true),
                    pop_est = table.Column<decimal>(type: "numeric(10,0)", nullable: true),
                    pop_rank = table.Column<short>(nullable: true),
                    gdp_md_est = table.Column<double>(nullable: true),
                    pop_year = table.Column<short>(nullable: true),
                    lastcensus = table.Column<short>(nullable: true),
                    gdp_year = table.Column<short>(nullable: true),
                    economy = table.Column<string>(maxLength: 26, nullable: true),
                    income_grp = table.Column<string>(maxLength: 23, nullable: true),
                    wikipedia = table.Column<short>(nullable: true),
                    fips_10_ = table.Column<string>(maxLength: 3, nullable: true),
                    iso_a2 = table.Column<string>(maxLength: 3, nullable: true),
                    iso_a3 = table.Column<string>(maxLength: 3, nullable: true),
                    iso_a3_eh = table.Column<string>(maxLength: 3, nullable: true),
                    iso_n3 = table.Column<string>(maxLength: 3, nullable: true),
                    un_a3 = table.Column<string>(maxLength: 4, nullable: true),
                    wb_a2 = table.Column<string>(maxLength: 3, nullable: true),
                    wb_a3 = table.Column<string>(maxLength: 3, nullable: true),
                    woe_id = table.Column<int>(nullable: true),
                    woe_id_eh = table.Column<int>(nullable: true),
                    woe_note = table.Column<string>(maxLength: 167, nullable: true),
                    adm0_a3_is = table.Column<string>(maxLength: 3, nullable: true),
                    adm0_a3_us = table.Column<string>(maxLength: 3, nullable: true),
                    adm0_a3_un = table.Column<short>(nullable: true),
                    adm0_a3_wb = table.Column<short>(nullable: true),
                    continent = table.Column<string>(maxLength: 23, nullable: true),
                    region_un = table.Column<string>(maxLength: 23, nullable: true),
                    subregion = table.Column<string>(maxLength: 25, nullable: true),
                    region_wb = table.Column<string>(maxLength: 26, nullable: true),
                    name_len = table.Column<short>(nullable: true),
                    long_len = table.Column<short>(nullable: true),
                    abbrev_len = table.Column<short>(nullable: true),
                    tiny = table.Column<short>(nullable: true),
                    homepart = table.Column<short>(nullable: true),
                    min_zoom = table.Column<double>(nullable: true),
                    min_label = table.Column<double>(nullable: true),
                    max_label = table.Column<double>(nullable: true),
                    ne_id = table.Column<decimal>(type: "numeric(10,0)", nullable: true),
                    wikidataid = table.Column<string>(maxLength: 8, nullable: true),
                    name_ar = table.Column<string>(maxLength: 72, nullable: true),
                    name_bn = table.Column<string>(maxLength: 148, nullable: true),
                    name_de = table.Column<string>(maxLength: 46, nullable: true),
                    name_en = table.Column<string>(maxLength: 44, nullable: true),
                    name_es = table.Column<string>(maxLength: 44, nullable: true),
                    name_fr = table.Column<string>(maxLength: 54, nullable: true),
                    name_el = table.Column<string>(maxLength: 88, nullable: true),
                    name_hi = table.Column<string>(maxLength: 126, nullable: true),
                    name_hu = table.Column<string>(maxLength: 52, nullable: true),
                    name_id = table.Column<string>(maxLength: 46, nullable: true),
                    name_it = table.Column<string>(maxLength: 48, nullable: true),
                    name_ja = table.Column<string>(maxLength: 63, nullable: true),
                    name_ko = table.Column<string>(maxLength: 47, nullable: true),
                    name_nl = table.Column<string>(maxLength: 49, nullable: true),
                    name_pl = table.Column<string>(maxLength: 47, nullable: true),
                    name_pt = table.Column<string>(maxLength: 43, nullable: true),
                    name_ru = table.Column<string>(maxLength: 86, nullable: true),
                    name_sv = table.Column<string>(maxLength: 57, nullable: true),
                    name_tr = table.Column<string>(maxLength: 42, nullable: true),
                    name_vi = table.Column<string>(maxLength: 56, nullable: true),
                    name_zh = table.Column<string>(maxLength: 36, nullable: true),
                    geom = table.Column<MultiPolygon>(type: "geometry(MultiPolygon)", nullable: true),
                    centroid = table.Column<Geometry>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("countries_pkey", x => x.gid);
                });

            migrationBuilder.CreateTable(
                name: "EthEvents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FilterId = table.Column<string>(nullable: true),
                    NodeName = table.Column<string>(nullable: true),
                    TransactionHash = table.Column<string>(nullable: true),
                    LogIndex = table.Column<int>(nullable: false),
                    BlockNumber = table.Column<int>(nullable: false),
                    BlockHash = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    EventSpecificationSignature = table.Column<string>(nullable: true),
                    NetworkName = table.Column<string>(nullable: true),
                    FailedTimes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EthEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    gid = table.Column<int>(nullable: false, defaultValueSql: "nextval('ne_10m_admin_1_states_provinces_gid_seq'::regclass)"),
                    featurecla = table.Column<string>(maxLength: 20, nullable: true),
                    scalerank = table.Column<short>(nullable: true),
                    adm1_code = table.Column<string>(maxLength: 9, nullable: true),
                    diss_me = table.Column<int>(nullable: true),
                    iso_3166_2 = table.Column<string>(maxLength: 8, nullable: true),
                    wikipedia = table.Column<string>(maxLength: 84, nullable: true),
                    iso_a2 = table.Column<string>(maxLength: 2, nullable: true),
                    adm0_sr = table.Column<short>(nullable: true),
                    name = table.Column<string>(maxLength: 44, nullable: true),
                    name_alt = table.Column<string>(maxLength: 129, nullable: true),
                    name_local = table.Column<string>(maxLength: 66, nullable: true),
                    type = table.Column<string>(maxLength: 38, nullable: true),
                    type_en = table.Column<string>(maxLength: 27, nullable: true),
                    code_local = table.Column<string>(maxLength: 5, nullable: true),
                    code_hasc = table.Column<string>(maxLength: 8, nullable: true),
                    note = table.Column<string>(maxLength: 114, nullable: true),
                    hasc_maybe = table.Column<string>(maxLength: 13, nullable: true),
                    region = table.Column<string>(maxLength: 43, nullable: true),
                    region_cod = table.Column<string>(maxLength: 15, nullable: true),
                    provnum_ne = table.Column<int>(nullable: true),
                    gadm_level = table.Column<short>(nullable: true),
                    check_me = table.Column<short>(nullable: true),
                    datarank = table.Column<short>(nullable: true),
                    abbrev = table.Column<string>(maxLength: 9, nullable: true),
                    postal = table.Column<string>(maxLength: 3, nullable: true),
                    area_sqkm = table.Column<short>(nullable: true),
                    sameascity = table.Column<short>(nullable: true),
                    labelrank = table.Column<short>(nullable: true),
                    name_len = table.Column<short>(nullable: true),
                    mapcolor9 = table.Column<short>(nullable: true),
                    mapcolor13 = table.Column<short>(nullable: true),
                    fips = table.Column<string>(maxLength: 5, nullable: true),
                    fips_alt = table.Column<string>(maxLength: 9, nullable: true),
                    woe_id = table.Column<int>(nullable: true),
                    woe_label = table.Column<string>(maxLength: 64, nullable: true),
                    woe_name = table.Column<string>(maxLength: 44, nullable: true),
                    latitude = table.Column<double>(nullable: true),
                    longitude = table.Column<double>(nullable: true),
                    sov_a3 = table.Column<string>(maxLength: 3, nullable: true),
                    adm0_a3 = table.Column<string>(maxLength: 3, nullable: true),
                    adm0_label = table.Column<short>(nullable: true),
                    admin = table.Column<string>(maxLength: 36, nullable: true),
                    geonunit = table.Column<string>(maxLength: 40, nullable: true),
                    gu_a3 = table.Column<string>(maxLength: 3, nullable: true),
                    gn_id = table.Column<int>(nullable: true),
                    gn_name = table.Column<string>(maxLength: 72, nullable: true),
                    gns_id = table.Column<int>(nullable: true),
                    gns_name = table.Column<string>(maxLength: 80, nullable: true),
                    gn_level = table.Column<short>(nullable: true),
                    gn_region = table.Column<string>(maxLength: 1, nullable: true),
                    gn_a1_code = table.Column<string>(maxLength: 10, nullable: true),
                    region_sub = table.Column<string>(maxLength: 41, nullable: true),
                    sub_code = table.Column<string>(maxLength: 5, nullable: true),
                    gns_level = table.Column<short>(nullable: true),
                    gns_lang = table.Column<string>(maxLength: 3, nullable: true),
                    gns_adm1 = table.Column<string>(maxLength: 4, nullable: true),
                    gns_region = table.Column<string>(maxLength: 4, nullable: true),
                    min_label = table.Column<double>(nullable: true),
                    max_label = table.Column<double>(nullable: true),
                    min_zoom = table.Column<double>(nullable: true),
                    wikidataid = table.Column<string>(maxLength: 9, nullable: true),
                    name_ar = table.Column<string>(maxLength: 85, nullable: true),
                    name_bn = table.Column<string>(maxLength: 128, nullable: true),
                    name_de = table.Column<string>(maxLength: 51, nullable: true),
                    name_en = table.Column<string>(maxLength: 47, nullable: true),
                    name_es = table.Column<string>(maxLength: 52, nullable: true),
                    name_fr = table.Column<string>(maxLength: 52, nullable: true),
                    name_el = table.Column<string>(maxLength: 82, nullable: true),
                    name_hi = table.Column<string>(maxLength: 134, nullable: true),
                    name_hu = table.Column<string>(maxLength: 41, nullable: true),
                    name_id = table.Column<string>(maxLength: 45, nullable: true),
                    name_it = table.Column<string>(maxLength: 49, nullable: true),
                    name_ja = table.Column<string>(maxLength: 93, nullable: true),
                    name_ko = table.Column<string>(maxLength: 58, nullable: true),
                    name_nl = table.Column<string>(maxLength: 44, nullable: true),
                    name_pl = table.Column<string>(maxLength: 45, nullable: true),
                    name_pt = table.Column<string>(maxLength: 43, nullable: true),
                    name_ru = table.Column<string>(maxLength: 91, nullable: true),
                    name_sv = table.Column<string>(maxLength: 48, nullable: true),
                    name_tr = table.Column<string>(maxLength: 44, nullable: true),
                    name_vi = table.Column<string>(maxLength: 49, nullable: true),
                    name_zh = table.Column<string>(maxLength: 61, nullable: true),
                    ne_id = table.Column<long>(nullable: true),
                    geom = table.Column<MultiPolygon>(type: "geometry(MultiPolygon)", nullable: true),
                    centroid = table.Column<Geometry>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ne_10m_admin_1_states_provinces_pkey", x => x.gid);
                });

            migrationBuilder.CreateTable(
                name: "EthValueDTO",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    EthEventDTOId = table.Column<string>(nullable: true),
                    EthEventDTOId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EthValueDTO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EthValueDTO_EthEvents_EthEventDTOId",
                        column: x => x.EthEventDTOId,
                        principalTable: "EthEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EthValueDTO_EthEvents_EthEventDTOId1",
                        column: x => x.EthEventDTOId1,
                        principalTable: "EthEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredCredentials",
                columns: table => new
                {
                    HashId = table.Column<string>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CitizenAddress = table.Column<string>(nullable: true),
                    SubjectHashId = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    CredentialCreation = table.Column<DateTime>(nullable: false),
                    Sex = table.Column<int>(nullable: false),
                    Location = table.Column<Point>(type: "geometry(Point)", nullable: true),
                    Lat = table.Column<double>(nullable: false),
                    Lon = table.Column<double>(nullable: false),
                    CredentialType = table.Column<int>(nullable: false),
                    Reason = table.Column<int>(nullable: false),
                    IsRevoked = table.Column<bool>(nullable: false),
                    Age = table.Column<short>(nullable: false),
                    HasNoSymptoms = table.Column<bool>(nullable: true),
                    HasSymptoms = table.Column<bool>(nullable: true),
                    HasFever = table.Column<bool>(nullable: true),
                    HasCought = table.Column<bool>(nullable: true),
                    HasBreathingIssues = table.Column<bool>(nullable: true),
                    HasLossSmell = table.Column<bool>(nullable: true),
                    HasHeadache = table.Column<bool>(nullable: true),
                    HasMusclePain = table.Column<bool>(nullable: true),
                    HasSoreThroat = table.Column<bool>(nullable: true),
                    CountryGId = table.Column<int>(nullable: false),
                    StateGId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredCredentials", x => x.HashId);
                    table.ForeignKey(
                        name: "InCountry",
                        column: x => x.CountryGId,
                        principalTable: "Countries",
                        principalColumn: "gid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "InState",
                        column: x => x.StateGId,
                        principalTable: "States",
                        principalColumn: "gid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "countries_geom_idx",
                table: "Countries",
                column: "geom")
                .Annotation("Npgsql:IndexMethod", "gist");

            migrationBuilder.CreateIndex(
                name: "IX_EthValueDTO_EthEventDTOId",
                table: "EthValueDTO",
                column: "EthEventDTOId");

            migrationBuilder.CreateIndex(
                name: "IX_EthValueDTO_EthEventDTOId1",
                table: "EthValueDTO",
                column: "EthEventDTOId1");

            migrationBuilder.CreateIndex(
                name: "fki_InCountry",
                table: "RegisteredCredentials",
                column: "CountryGId");

            migrationBuilder.CreateIndex(
                name: "fki_InState",
                table: "RegisteredCredentials",
                column: "StateGId");

            migrationBuilder.CreateIndex(
                name: "SubjectIdIdx",
                table: "RegisteredCredentials",
                columns: new[] { "IsRevoked", "SubjectHashId" })
                .Annotation("Npgsql:IndexCollation", new[] { null, "C.UTF-8" })
                .Annotation("Npgsql:IndexOperators", new[] { null, "varchar_ops" });

            migrationBuilder.CreateIndex(
                name: "ne_10m_admin_1_states_provinces_geom_idx",
                table: "States",
                column: "geom")
                .Annotation("Npgsql:IndexMethod", "gist");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EthValueDTO");

            migrationBuilder.DropTable(
                name: "RegisteredCredentials");

            migrationBuilder.DropTable(
                name: "EthEvents");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
