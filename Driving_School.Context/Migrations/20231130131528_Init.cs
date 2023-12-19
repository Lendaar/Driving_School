using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Driving_School.Context.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCourses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TPersons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Passport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPersons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TPlaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPlaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TTransports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GSBType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTransports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeType = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TEmployees_TPersons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "TPersons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TLessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PlaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TLessons_TCourses_CourceId",
                        column: x => x.CourceId,
                        principalTable: "TCourses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TLessons_TEmployees_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "TEmployees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TLessons_TEmployees_StudentId",
                        column: x => x.StudentId,
                        principalTable: "TEmployees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TLessons_TPlaces_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "TPlaces",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TLessons_TTransports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "TTransports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_Name",
                table: "TCourses",
                column: "Name",
                filter: "DeletedAt is null");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Experience",
                table: "TEmployees",
                column: "Experience",
                filter: "DeletedAt is null");

            migrationBuilder.CreateIndex(
                name: "IX_TEmployees_PersonId",
                table: "TEmployees",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_StartDate",
                table: "TLessons",
                column: "StartDate",
                filter: "DeletedAt is null");

            migrationBuilder.CreateIndex(
                name: "IX_TLessons_CourceId",
                table: "TLessons",
                column: "CourceId");

            migrationBuilder.CreateIndex(
                name: "IX_TLessons_InstructorId",
                table: "TLessons",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_TLessons_PlaceId",
                table: "TLessons",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TLessons_StudentId",
                table: "TLessons",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TLessons_TransportId",
                table: "TLessons",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_LastName",
                table: "TPersons",
                column: "LastName",
                filter: "DeletedAt is null");

            migrationBuilder.CreateIndex(
                name: "IX_Place_Name",
                table: "TPlaces",
                column: "Name",
                filter: "DeletedAt is null");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_Name",
                table: "TTransports",
                column: "Name",
                filter: "DeletedAt is null");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TLessons");

            migrationBuilder.DropTable(
                name: "TCourses");

            migrationBuilder.DropTable(
                name: "TEmployees");

            migrationBuilder.DropTable(
                name: "TPlaces");

            migrationBuilder.DropTable(
                name: "TTransports");

            migrationBuilder.DropTable(
                name: "TPersons");
        }
    }
}
