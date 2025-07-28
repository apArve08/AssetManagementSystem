using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ITAssetManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetTag = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: false),
                    SerialNumber = table.Column<string>(type: "text", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    AssignedTo = table.Column<string>(type: "text", nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Location = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssetId = table.Column<int>(type: "integer", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    PerformedBy = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "AssetTag", "AssignedDate", "AssignedTo", "Brand", "Category", "CreatedAt", "Location", "Model", "Name", "Notes", "PurchaseDate", "PurchasePrice", "SerialNumber", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "IT-001", new DateTime(2025, 2, 28, 14, 29, 32, 968, DateTimeKind.Utc).AddTicks(6830), "John Doe", "Dell", "Laptop", new DateTime(2025, 7, 28, 14, 29, 32, 968, DateTimeKind.Utc).AddTicks(6810), "Floor 2, IT Dept", "Latitude 5520", "Dell Laptop", null, new DateTime(2025, 1, 28, 14, 29, 32, 968, DateTimeKind.Utc).AddTicks(6820), 1200m, "DL5520001", "Assigned", new DateTime(2025, 7, 28, 14, 29, 32, 968, DateTimeKind.Utc).AddTicks(6810) },
                    { 2, "IT-002", null, null, "HP", "Monitor", new DateTime(2025, 7, 28, 14, 29, 32, 968, DateTimeKind.Utc).AddTicks(6830), "Storage Room", "E24 G4", "HP Monitor", null, new DateTime(2025, 3, 28, 14, 29, 32, 968, DateTimeKind.Utc).AddTicks(6830), 250m, "HPE24001", "Available", new DateTime(2025, 7, 28, 14, 29, 32, 968, DateTimeKind.Utc).AddTicks(6830) },
                    { 3, "IT-003", null, null, "Apple", "Laptop", new DateTime(2025, 7, 28, 14, 29, 32, 968, DateTimeKind.Utc).AddTicks(6830), "IT Service Desk", "MacBook Pro 14", "MacBook Pro", null, new DateTime(2025, 5, 28, 14, 29, 32, 968, DateTimeKind.Utc).AddTicks(6830), 2500m, "MB14001", "Repair", new DateTime(2025, 7, 28, 14, 29, 32, 968, DateTimeKind.Utc).AddTicks(6830) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsActive", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 28, 14, 29, 32, 968, DateTimeKind.Utc).AddTicks(6770), "admin@company.com", "System Administrator", true, "admin123", "Admin", "admin" },
                    { 2, new DateTime(2025, 7, 28, 14, 29, 32, 968, DateTimeKind.Utc).AddTicks(6770), "user@company.com", "Normal User", true, "user123", "User", "user" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_AssetId",
                table: "ActivityLogs",
                column: "AssetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
