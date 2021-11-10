using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class FixForeignKeyProblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCast_Cast_MovieId",
                table: "MovieCast");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCast_Movie_CastId",
                table: "MovieCast");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCrew_Crew_MovieId",
                table: "MovieCrew");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCrew_Movie_CrewId",
                table: "MovieCrew");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCast_Cast_CastId",
                table: "MovieCast",
                column: "CastId",
                principalTable: "Cast",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCast_Movie_MovieId",
                table: "MovieCast",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCrew_Crew_CrewId",
                table: "MovieCrew",
                column: "CrewId",
                principalTable: "Crew",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCrew_Movie_MovieId",
                table: "MovieCrew",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieCast_Cast_CastId",
                table: "MovieCast");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCast_Movie_MovieId",
                table: "MovieCast");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCrew_Crew_CrewId",
                table: "MovieCrew");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieCrew_Movie_MovieId",
                table: "MovieCrew");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCast_Cast_MovieId",
                table: "MovieCast",
                column: "MovieId",
                principalTable: "Cast",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCast_Movie_CastId",
                table: "MovieCast",
                column: "CastId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCrew_Crew_MovieId",
                table: "MovieCrew",
                column: "MovieId",
                principalTable: "Crew",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieCrew_Movie_CrewId",
                table: "MovieCrew",
                column: "CrewId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
