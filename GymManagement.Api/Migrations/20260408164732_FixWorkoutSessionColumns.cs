using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixWorkoutSessionColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "WorkoutSessions",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "DurationOnMinutes",
                table: "WorkoutSessions",
                newName: "DurationInMinutes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "WorkoutSessions",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "DurationInMinutes",
                table: "WorkoutSessions",
                newName: "DurationOnMinutes");
        }
    }
}
