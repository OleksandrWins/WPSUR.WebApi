using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPSUR.Repository.Migrations
{
    /// <inheritdoc />
    public partial class latest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PostEntityId",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_PostEntityId",
                table: "User",
                column: "PostEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Post_PostEntityId",
                table: "User",
                column: "PostEntityId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Post_PostEntityId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_PostEntityId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PostEntityId",
                table: "User");
        }
    }
}
