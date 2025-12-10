using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseWork.Migrations
{
    /// <inheritdoc />
    public partial class SeedCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "Description", "LevelId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Вивчіть основи програмування на C# з нуля.", 1, "Основи C#" },
                    { 2, 1, "Створюйте сучасні веб-додатки з використанням ASP.NET Core.", 2, "Веб-розробка з ASP.NET Core" },
                    { 3, 2, "Основи створення інтерфейсів користувача та досвіду використання.", 1, "UI/UX Дизайн" },
                    { 4, 3, "Дізнайтеся, як оптимізувати сайти та створювати ефективний контент.", 2, "SEO та контент-маркетинг" },
                    { 5, 1, "Глибокий аналіз алгоритмів та ефективні структури даних.", 3, "Алгоритми та структури даних" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
