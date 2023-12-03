using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Trainee_Details.Migrations
{
    /// <inheritdoc />
    public partial class CreateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    TraineeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraineeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsRegular = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.TraineeId);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CourseFee = table.Column<decimal>(type: "money", nullable: false),
                    AdmissionDate = table.Column<DateTime>(type: "date", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "TraineeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Trainees",
                columns: new[] { "TraineeId", "Age", "Gender", "IsRegular", "Picture", "TraineeName" },
                values: new object[,]
                {
                    { 1, 25, 1, null, "1jpg", "Trainee 1" },
                    { 2, 33, 1, null, "2jpg", "Trainee 2" },
                    { 3, 31, 2, null, "3jpg", "Trainee 3" },
                    { 4, 21, 3, null, "4jpg", "Trainee 4" },
                    { 5, 39, 3, null, "5jpg", "Trainee 5" },
                    { 6, 21, 4, null, "6jpg", "Trainee 6" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "AdmissionDate", "CourseFee", "CourseName", "TraineeId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 10, 22, 9, 51, 15, 587, DateTimeKind.Local).AddTicks(140), 10314m, "Course1", 1 },
                    { 2, new DateTime(2022, 9, 14, 9, 51, 15, 587, DateTimeKind.Local).AddTicks(204), 38980m, "Course2", 2 },
                    { 3, new DateTime(2022, 10, 17, 9, 51, 15, 587, DateTimeKind.Local).AddTicks(218), 38154m, "Course3", 3 },
                    { 4, new DateTime(2022, 8, 1, 9, 51, 15, 587, DateTimeKind.Local).AddTicks(232), 69384m, "Course4", 4 },
                    { 5, new DateTime(2022, 10, 10, 9, 51, 15, 587, DateTimeKind.Local).AddTicks(244), 88915m, "Course5", 5 },
                    { 6, new DateTime(2022, 7, 16, 9, 51, 15, 587, DateTimeKind.Local).AddTicks(267), 94716m, "Course6", 1 },
                    { 7, new DateTime(2022, 9, 1, 9, 51, 15, 587, DateTimeKind.Local).AddTicks(281), 123242m, "Course7", 2 },
                    { 8, new DateTime(2022, 9, 22, 9, 51, 15, 587, DateTimeKind.Local).AddTicks(294), 115936m, "Course8", 3 },
                    { 9, new DateTime(2022, 9, 1, 9, 51, 15, 587, DateTimeKind.Local).AddTicks(307), 145422m, "Course9", 4 },
                    { 10, new DateTime(2022, 9, 11, 9, 51, 15, 587, DateTimeKind.Local).AddTicks(336), 174790m, "Course10", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TraineeId",
                table: "Courses",
                column: "TraineeId");
            string procInsert = @"CREATE PROC InsertTrainee @n VARCHAR(50), @a int, @g INT, @pi VARCHAR(50), @ir BIT
                         AS
                         INSERT INTO Trainees (TraineeName, Age, Gender, Picture, IsRegular)
                         VALUES (@n, @a, @g, @pi, @ir )

                         GO";
            migrationBuilder.Sql(procInsert);
            string procUpdate = @"CREATE PROC UpdateTrainee @i INT, @n VARCHAR(50), @a int, @g INT, @pi VARCHAR(50), @ir BIT
                         AS
                         UPDATE Trainees SET TraineeName=@n, Age=@a, Gender=@g, Picture=@pi, IsRegular=@ir
                         WHERE TraineeId=@i
                         GO";
            migrationBuilder.Sql(procUpdate);
            string procDelete = @"CREATE PROC DeleteTrainee @i INT
                         AS
                         DELETE Trainees 
                         WHERE TraineeId=@i
                         GO";
            migrationBuilder.Sql(procDelete);
            string sqlS = @"CREATE PROC InsertCourse @cn VARCHAR(50), @cf MONEY , @ad DATE, @tid INT
                         AS
                         INSERT INTO Courses (CourseName, CourseFee, AdmissionDate, TraineeId)
                         VALUES (@cn, @cf, @ad, @tid )
                         GO";
            migrationBuilder.Sql(sqlS);
            string sqlSU = @"CREATE PROC UpdateCourse @id INT, @cn VARCHAR(50), @cf MONEY , @ad DATE, @tid INT
                         AS
                         UPDATE Courses SET CourseName=@cn, CourseFee=@cf, AdmissionDate=@ad, TraineeId=@tid
                         WHERE CourseId = @id
                         GO";
            migrationBuilder.Sql(sqlSU);
            string sqlDU = @"CREATE PROC DeleteCourse @id INT
                     AS
                     DELETE Courses 
                     WHERE CourseId = @id
                     GO";
            migrationBuilder.Sql(sqlDU);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Trainees");
        migrationBuilder.Sql("DROP PROC InsertTrainee");
        migrationBuilder.Sql("DROP PROC UpdateTrainee");
        migrationBuilder.Sql("DROP PROC DeleteTrainee");
        migrationBuilder.Sql("DROP PROC InsertCourse");
        migrationBuilder.Sql("DROP PROC UpdateCourse");
        migrationBuilder.Sql("DROP PROC DeleteCourse");
         }
    }
}
