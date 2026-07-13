using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modify_DoctorSchedule_table_Base : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "SlotDurationInMinutes",
                table: "DoctorSchedules");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "DoctorSchedules",
                type: "int",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedById",
                table: "DoctorSchedules",
                type: "int",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedById",
                table: "DoctorSchedules",
                type: "int",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "DoctorSchedules");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DoctorSchedules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "DoctorSchedules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "DoctorSchedules",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SlotDurationInMinutes",
                table: "DoctorSchedules",
                type: "int",
                nullable: false,
                defaultValue: 15);
        }
    }
}
