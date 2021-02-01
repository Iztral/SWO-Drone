using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SWO.Server.Migrations
{
    public partial class dataseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Simulators",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulators", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Simulators_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Instructors_Members_UserID",
                        column: x => x.UserID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Trainees_Members_UserID",
                        column: x => x.UserID,
                        principalTable: "Members",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradeTemplates",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxPoints = table.Column<int>(type: "int", nullable: false),
                    SimulatorID = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phase = table.Column<int>(type: "int", nullable: true),
                    OptimalTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeTemplates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GradeTemplates_Simulators_SimulatorID",
                        column: x => x.SimulatorID,
                        principalTable: "Simulators",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scenarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SimulatorID = table.Column<int>(type: "int", nullable: false),
                    MaxGradeSum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scenarios", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Scenarios_Simulators_SimulatorID",
                        column: x => x.SimulatorID,
                        principalTable: "Simulators",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Addendum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SimulatorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sensors_Simulators_SimulatorID",
                        column: x => x.SimulatorID,
                        principalTable: "Simulators",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScenarioGradesTemplates",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScenarioID = table.Column<int>(type: "int", nullable: false),
                    TemplateID = table.Column<int>(type: "int", nullable: false),
                    Assigned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScenarioGradesTemplates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ScenarioGradesTemplates_GradeTemplates_TemplateID",
                        column: x => x.TemplateID,
                        principalTable: "GradeTemplates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScenarioGradesTemplates_Scenarios_ScenarioID",
                        column: x => x.ScenarioID,
                        principalTable: "Scenarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Simulations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraineeID = table.Column<int>(type: "int", nullable: false),
                    InstructorID = table.Column<int>(type: "int", nullable: false),
                    ScenarioID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GradeSum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Simulations_Instructors_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Instructors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Simulations_Scenarios_ScenarioID",
                        column: x => x.ScenarioID,
                        principalTable: "Scenarios",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Simulations_Trainees_TraineeID",
                        column: x => x.TraineeID,
                        principalTable: "Trainees",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateID = table.Column<int>(type: "int", nullable: false),
                    SimulationID = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    TimeTaken = table.Column<int>(type: "int", nullable: false),
                    Addendum = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Grades_GradeTemplates_TemplateID",
                        column: x => x.TemplateID,
                        principalTable: "GradeTemplates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Simulations_SimulationID",
                        column: x => x.SimulationID,
                        principalTable: "Simulations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SensorValues",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SensorID = table.Column<int>(type: "int", nullable: false),
                    SimulationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorValues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SensorValues_Sensors_SensorID",
                        column: x => x.SensorID,
                        principalTable: "Sensors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SensorValues_Simulations_SimulationID",
                        column: x => x.SimulationID,
                        principalTable: "Simulations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f5b7c2c8-1ed7-421c-babb-d304d1904acd", "84b802fb-8d58-4c10-bb83-c573e1a8e623", "User", "USER" },
                    { "8a3f11a8-c2e6-44f0-be52-d9df0fa1a105", "fb2b1eb2-4f9b-499f-9218-72ce2a7b17ad", "Instructor", "INSTRUCTOR" },
                    { "9d0bb5f4-ebcf-402d-97e6-9a7ba62e408c", "68e07141-0639-4d36-a430-759d45fd7557", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "ID", "Address", "Description", "Name", "Photo", "Website" },
                values: new object[,]
                {
                    { 1, "ul. Henryka Dąbrowskiego 50\n59-100 Polkowice\nPolska", "Zakłady Górnicze „Rudna” to jedna z największych głębinowych kopalń rudy miedzi na świecie. Jej budowę rozpoczęto we wrześniu 1969 roku, a w lipcu 1974 roku kopalnia osiągnęła 25% projektowanego wydobycia i została oficjalnie przekazana do eksploatacji.", "Oddział Zakłady Górnicze „Rudna”", null, "https://kghm.com/pl/biznes/wydobycie-i-wzbogacanie/rudna" },
                    { 2, "ul. M. Skłodowskiej-Curie 188\n59-301 Lubin\nPolska", "Zakłady Górnicze Lubin to najstarsza kopalnia w polskim Zagłębiu Miedziowym. Wydobywana w nich polimetaliczna ruda zawiera głównie miedź oraz srebro.", "Zakłady Górnicze „Lubin”", null, "https://kghm.com/pl/biznes/wydobycie-i-wzbogacanie-sx-ew/lubin" },
                    { 3, "C-115-B, Taltal,\nRegión de Antofagasta\nChile", "Kopalnia Franke znajduje się w południowej części regionu Antofagasta, który jest największym chilijskim zagłębiem miedziowym.", "Minera Franke Sociedad Contractual", null, "https://kghm.com/pl/biznes/wydobycie-i-wzbogacanie-sx-ew/franke" }
                });

            migrationBuilder.InsertData(
                table: "Simulators",
                columns: new[] { "ID", "Description", "LocationID", "Name", "Photo", "Type" },
                values: new object[,]
                {
                    { 2, "Kotwiarka znajdująca się w Rudnie", 1, "Kotwiarka Rudna", null, 1 },
                    { 4, "Symulator VR LKP903 znajdujący się w Rudnicy", 1, "Symulator VR LKP903 Rudnica", null, 3 },
                    { 6, "Symulator VR SWW znajdujący się w  Rudnicy", 1, "Symulator VR SWW Rudnica", null, 5 },
                    { 1, "Ładowarka znajdująca się w Lublinie", 2, "Ładowarka Lublin", null, 0 },
                    { 3, "Wiertnica znajdująca się w Lublinie", 2, "Wiertnica Lublin", null, 2 },
                    { 5, "Symulator VR SWKF znajdujący się w Lublinie", 2, "Symulator VR SWKF Lublin", null, 4 }
                });

            migrationBuilder.InsertData(
                table: "GradeTemplates",
                columns: new[] { "ID", "Description", "MaxPoints", "Name", "Note", "OptimalTime", "Phase", "SimulatorID" },
                values: new object[,]
                {
                    { 11, "postój w wyznaczonym miejscu (swobodny załadunek obudowy)", 3, "SWKF.1.1", null, 1, null, 2 },
                    { 4, "jeżeli nie jest to miejsce parkowania to włączone światła", 2, "LKP-903.1.4", null, 0, null, 1 },
                    { 5, "zaciągnięcie hamulca postojowego HAP", 3, "LKP-903.1.5", null, 20, null, 1 },
                    { 7, "zgaszenie maszyny", 1, "LKP-903.1.7", null, 8, null, 1 },
                    { 8, "założenie okularów ochronnych przed wyjściem z maszyny", 1, "LKP-903.1.8", null, 8, null, 1 },
                    { 9, "zabranie aparatu ucieczkowego", 2, "LKP-903.1.9", null, 4, null, 1 },
                    { 10, "podklinowanie maszyny", 3, "LKP-903.1.10", null, 15, null, 1 },
                    { 22, "poziomy równy spąg", 2, "SWW.1.1", null, 8, null, 3 },
                    { 3, "ustawnie dźwigni w pozycji neutralnej", 2, "LKP-903.1.3", null, 8, null, 1 },
                    { 23, "maksymalne opuszczenie organu roboczego ", 2, "SWW.1.2", null, 10, null, 3 },
                    { 25, "jeżeli nie jest to miejsce parkowania to włączone światła", 2, "SWW.1.4", null, 13, null, 3 },
                    { 26, "zaciągnięcie hamulca postojowego HAP", 3, "SWW.1.5", null, 3, null, 3 },
                    { 27, "odpięcie pasów bezpieczeństwa", 0, "SWW.1.6", "Sytuacja wymagana", 16, null, 3 },
                    { 28, "zgaszenie maszyny", 1, "SWW.1.7", null, 1, null, 3 },
                    { 29, "założenie okularów ochronnych przed wyjściem z maszyny", 1, "SWW.1.8", null, 20, null, 3 },
                    { 30, "zabranie aparatu ucieczkowego", 2, "SWW.1.9", null, 4, null, 3 },
                    { 31, "podklinowanie maszyny", 3, "SWW.1.10", null, 6, null, 3 },
                    { 24, "ustawnie dźwigni w pozycji neutralnej", 2, "SWW.1.3", null, 17, null, 3 },
                    { 2, "organ roboczy położony na spągu", 2, "LKP-903.1.2", null, 13, null, 1 },
                    { 6, "odpięcie pasów bezpieczeństwa", 0, "LKP-903.1.6", "Sytuacja wymagana", 12, null, 1 },
                    { 12, "brak wyznaczonego miejsca, maszyna pozostaje na włączonych światłach", 2, "SWKF.1.2", null, 13, null, 2 },
                    { 13, "maksymalne opuszczenie organu roboczego", 2, "SWKF.1.3", null, 17, null, 2 },
                    { 14, "ustawnie dźwigni w pozycji neutralnej", 2, "SWKF.1.4", null, 12, null, 2 },
                    { 15, "jeżeli nie jest to miejsce parkowania to włączone światła", 2, "SWKF.1.5", null, 7, null, 2 },
                    { 16, "zaciągnięcie hamulca postojowego HAP", 2, "SWKF.1.6", null, 12, null, 2 },
                    { 17, "odpięcie pasów bezpieczeństwa", 0, "SWKF.1.7", "Sytuacja wymagana", 16, null, 2 },
                    { 18, "zgaszenie maszyny", 1, "SWKF.1.8", null, 3, null, 2 },
                    { 19, "założenie okularów ochronnych przed wyjściem z maszyny lub wyjście w goglach", 1, "SWKF.1.9", null, 11, null, 2 },
                    { 20, "zabranie aparatu ucieczkowego", 2, "SWKF.1.10", null, 10, null, 2 },
                    { 21, "podklinowanie maszyny", 3, "SWKF.1.11", null, 4, null, 2 },
                    { 1, "poziomy równy spąg", 2, "LKP-903.1.1", null, 6, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Sensors",
                columns: new[] { "ID", "Addendum", "Category", "Name", "SimulatorID" },
                values: new object[,]
                {
                    { 14, null, "Pulpit sterowania układem roboczym", "Manometr hydrauliczny", 2 },
                    { 17, null, "Stanowisko do wiercenia rozdzielacze hydrauliczne", "Posuw wiertarki przód / tył", 3 },
                    { 16, null, "Stanowisko do wiercenia rozdzielacze hydrauliczne", "Obroty w dół prawe / w górę lewe", 3 },
                    { 15, null, "Stanowisko do wiercenia rozdzielacze hydrauliczne", "Udary w dół połowa (zawiercanie) / do góry pełne", 3 },
                    { 6, null, "Pulpit operatora", "Przycisk (żółty) – sygnały dźwiękowe", 1 },
                    { 4, null, "Pulpit operatora", "Przycisk (niebieski) – światła drogowe i pomocnicze „kierunek na łyżkę”", 1 },
                    { 3, null, "Pulpit operatora", "Włącznik wycieraczek (biały)", 1 },
                    { 2, null, "Pulpit operatora", "Przycisk (niebieski) – (światła pomocnicze) – reflektor „tamy”", 1 },
                    { 1, null, "Pulpit operatora", "Przyciski odłącznika akumulatorów", 1 },
                    { 7, null, "Pulpit sterowania układem roboczym", "Dźwignia rozdzielacza awaryjnego zasilania układu roboczego", 2 },
                    { 8, null, "Pulpit sterowania układem roboczym", "Dźwignia rozdzielacza podnoszenia wysięgnika dół/góra", 2 }
                });

            migrationBuilder.InsertData(
                table: "Sensors",
                columns: new[] { "ID", "Addendum", "Category", "Name", "SimulatorID" },
                values: new object[,]
                {
                    { 9, null, "Pulpit sterowania układem roboczym", "Dźwignia rozdzielacza skręt wysięgnika prawo/lewo", 2 },
                    { 10, null, "Pulpit sterowania układem roboczym", "Dźwignia rozdzielacza podpora lewa góra/dół", 2 },
                    { 18, null, "Stanowisko do wiercenia rozdzielacze hydrauliczne", "Przepłuczka w górę wodno / w dół powietrzna", 3 },
                    { 11, null, "Pulpit sterowania układem roboczym", "Dźwignia rozdzielacza podpora prawa góra/dół", 2 },
                    { 12, null, "Pulpit sterowania układem roboczym", "Dźwignia rozdzielacza podpory tylne góra/dół", 2 },
                    { 13, null, "Pulpit sterowania układem roboczym", "Dźwignia rozdzielacza zwijak kabla elektrycznego", 2 },
                    { 5, null, "Pulpit operatora", "Przycisk (niebieski) – światła drogowe „kierunek na ciągnik”", 1 },
                    { 19, null, "Stanowisko do wiercenia rozdzielacze hydrauliczne", "Regulacja ciśnienia posuwu", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_SimulationID",
                table: "Grades",
                column: "SimulationID");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_TemplateID",
                table: "Grades",
                column: "TemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_GradeTemplates_SimulatorID",
                table: "GradeTemplates",
                column: "SimulatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_UserID",
                table: "Instructors",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioGradesTemplates_ScenarioID",
                table: "ScenarioGradesTemplates",
                column: "ScenarioID");

            migrationBuilder.CreateIndex(
                name: "IX_ScenarioGradesTemplates_TemplateID",
                table: "ScenarioGradesTemplates",
                column: "TemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_Scenarios_SimulatorID",
                table: "Scenarios",
                column: "SimulatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_SimulatorID",
                table: "Sensors",
                column: "SimulatorID");

            migrationBuilder.CreateIndex(
                name: "IX_SensorValues_SensorID",
                table: "SensorValues",
                column: "SensorID");

            migrationBuilder.CreateIndex(
                name: "IX_SensorValues_SimulationID",
                table: "SensorValues",
                column: "SimulationID");

            migrationBuilder.CreateIndex(
                name: "IX_Simulations_InstructorID",
                table: "Simulations",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_Simulations_ScenarioID",
                table: "Simulations",
                column: "ScenarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Simulations_TraineeID",
                table: "Simulations",
                column: "TraineeID");

            migrationBuilder.CreateIndex(
                name: "IX_Simulators_LocationID",
                table: "Simulators",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_UserID",
                table: "Trainees",
                column: "UserID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "ScenarioGradesTemplates");

            migrationBuilder.DropTable(
                name: "SensorValues");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "GradeTemplates");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "Simulations");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Scenarios");

            migrationBuilder.DropTable(
                name: "Trainees");

            migrationBuilder.DropTable(
                name: "Simulators");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
