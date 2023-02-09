using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPSUR.Repository.Migrations
{
    /// <inheritdoc />
    public partial class postUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTag_MainTag_MainTagId",
                table: "SubTag");

            migrationBuilder.DropIndex(
                name: "IX_SubTag_MainTagId",
                table: "SubTag");

            migrationBuilder.DropColumn(
                name: "MainTagId",
                table: "SubTag");

            migrationBuilder.CreateTable(
                name: "MainTagEntitySubTagEntity",
                columns: table => new
                {
                    MainTagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubTagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainTagEntitySubTagEntity", x => new { x.MainTagsId, x.SubTagsId });
                    table.ForeignKey(
                        name: "FK_MainTagEntitySubTagEntity_MainTag_MainTagsId",
                        column: x => x.MainTagsId,
                        principalTable: "MainTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainTagEntitySubTagEntity_SubTag_SubTagsId",
                        column: x => x.SubTagsId,
                        principalTable: "SubTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainTagEntitySubTagEntity_SubTagsId",
                table: "MainTagEntitySubTagEntity",
                column: "SubTagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainTagEntitySubTagEntity");

            migrationBuilder.AddColumn<Guid>(
                name: "MainTagId",
                table: "SubTag",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubTag_MainTagId",
                table: "SubTag",
                column: "MainTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTag_MainTag_MainTagId",
                table: "SubTag",
                column: "MainTagId",
                principalTable: "MainTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
