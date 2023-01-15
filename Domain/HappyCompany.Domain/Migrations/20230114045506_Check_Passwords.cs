using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HappyCompany.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CheckPasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63ff880a-38f7-410a-a654-48a377a2de91", "admin@happywarehouse.com", "AQAAAAEAACcQAAAAEPSeMxw5o7IHiXq/mL1kdWZR9xOSk+URLqkhkz4OLElGoiWTdTl7cdHnpRcwCFQHFw==", "d1af8d18-6b8b-4885-9a23-6eaf988d5ad0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1940173c-5b04-4479-9208-02436a30843d", null, null, "7b6360e8-152a-4900-973d-5ca356033b35" });
        }
    }
}
