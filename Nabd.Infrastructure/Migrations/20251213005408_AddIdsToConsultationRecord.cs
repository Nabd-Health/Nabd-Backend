using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nabd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdsToConsultationRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "ConsultationRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PatientId",
                table: "ConsultationRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "ConsultationRecords");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "ConsultationRecords");
        }
    }
}
