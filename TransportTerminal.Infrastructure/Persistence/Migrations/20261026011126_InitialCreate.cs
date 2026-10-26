using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportTerminal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Nit = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "terminals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_terminals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bays", x => x.Id);
                    table.UniqueConstraint("AK_bays_TerminalId_Id", x => new { x.TerminalId, x.Id });
                    table.ForeignKey(
                        name: "FK_bays_terminals_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "terminals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "routes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Origin = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Destination = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    EstimatedMinutes = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_routes", x => x.Id);
                    table.UniqueConstraint("AK_routes_TerminalId_Id", x => new { x.TerminalId, x.Id });
                    table.CheckConstraint("ck_routes_estimated_minutes_positive", "\"EstimatedMinutes\" > 0");
                    table.ForeignKey(
                        name: "FK_routes_terminals_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "terminals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "terminal_companies",
                columns: table => new
                {
                    TerminalId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_terminal_companies", x => new { x.TerminalId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_terminal_companies_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_terminal_companies_terminals_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "terminals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Plate = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Type = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.Id);
                    table.UniqueConstraint("AK_vehicles_TerminalId_Id", x => new { x.TerminalId, x.Id });
                    table.CheckConstraint("ck_vehicles_capacity_positive", "\"Capacity\" > 0");
                    table.ForeignKey(
                        name: "FK_vehicles_terminal_companies_TerminalId_CompanyId",
                        columns: x => new { x.TerminalId, x.CompanyId },
                        principalTable: "terminal_companies",
                        principalColumns: new[] { "TerminalId", "CompanyId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vehicles_terminals_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "terminals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dispatches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    RouteId = table.Column<Guid>(type: "uuid", nullable: false),
                    BayId = table.Column<Guid>(type: "uuid", nullable: false),
                    ScheduledDeparture = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ActualDeparture = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dispatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dispatches_bays_TerminalId_BayId",
                        columns: x => new { x.TerminalId, x.BayId },
                        principalTable: "bays",
                        principalColumns: new[] { "TerminalId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dispatches_routes_TerminalId_RouteId",
                        columns: x => new { x.TerminalId, x.RouteId },
                        principalTable: "routes",
                        principalColumns: new[] { "TerminalId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dispatches_terminals_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "terminals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dispatches_vehicles_TerminalId_VehicleId",
                        columns: x => new { x.TerminalId, x.VehicleId },
                        principalTable: "vehicles",
                        principalColumns: new[] { "TerminalId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_movements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TerminalId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_movements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vehicle_movements_terminals_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "terminals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_vehicle_movements_vehicles_TerminalId_VehicleId",
                        columns: x => new { x.TerminalId, x.VehicleId },
                        principalTable: "vehicles",
                        principalColumns: new[] { "TerminalId", "Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bays_TerminalId_Code",
                table: "bays",
                columns: new[] { "TerminalId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_companies_Nit",
                table: "companies",
                column: "Nit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dispatches_TerminalId_BayId",
                table: "dispatches",
                columns: new[] { "TerminalId", "BayId" });

            migrationBuilder.CreateIndex(
                name: "IX_dispatches_TerminalId_RouteId",
                table: "dispatches",
                columns: new[] { "TerminalId", "RouteId" });

            migrationBuilder.CreateIndex(
                name: "IX_dispatches_TerminalId_VehicleId",
                table: "dispatches",
                columns: new[] { "TerminalId", "VehicleId" });

            migrationBuilder.CreateIndex(
                name: "IX_terminal_companies_CompanyId",
                table: "terminal_companies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_terminals_Code",
                table: "terminals",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_movements_TerminalId_VehicleId_Timestamp",
                table: "vehicle_movements",
                columns: new[] { "TerminalId", "VehicleId", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_TerminalId_CompanyId",
                table: "vehicles",
                columns: new[] { "TerminalId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_TerminalId_Plate",
                table: "vehicles",
                columns: new[] { "TerminalId", "Plate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dispatches");

            migrationBuilder.DropTable(
                name: "vehicle_movements");

            migrationBuilder.DropTable(
                name: "bays");

            migrationBuilder.DropTable(
                name: "routes");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropTable(
                name: "terminal_companies");

            migrationBuilder.DropTable(
                name: "companies");

            migrationBuilder.DropTable(
                name: "terminals");
        }
    }
}
