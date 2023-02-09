using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPSUR.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Post : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_CreatedById",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_DeletedById",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_UpdatedById",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_UserFromId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_UserToId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_CreatedById",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_DeletedById",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_UpdatedById",
                table: "Post");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Post",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MainTagId",
                table: "Post",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Post",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MainTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedData = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainTag_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainTag_User_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainTag_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MainTagId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedData = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubTag_MainTag_MainTagId",
                        column: x => x.MainTagId,
                        principalTable: "MainTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubTag_User_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubTag_User_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubTag_User_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostEntitySubTagEntity",
                columns: table => new
                {
                    PostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubTagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntitySubTagEntity", x => new { x.PostsId, x.SubTagsId });
                    table.ForeignKey(
                        name: "FK_PostEntitySubTagEntity_Post_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostEntitySubTagEntity_SubTag_SubTagsId",
                        column: x => x.SubTagsId,
                        principalTable: "SubTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_MainTagId",
                table: "Post",
                column: "MainTagId");

            migrationBuilder.CreateIndex(
                name: "IX_MainTag_CreatedById",
                table: "MainTag",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MainTag_DeletedById",
                table: "MainTag",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_MainTag_UpdatedById",
                table: "MainTag",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PostEntitySubTagEntity_SubTagsId",
                table: "PostEntitySubTagEntity",
                column: "SubTagsId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTag_CreatedById",
                table: "SubTag",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SubTag_DeletedById",
                table: "SubTag",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_SubTag_MainTagId",
                table: "SubTag",
                column: "MainTagId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTag_UpdatedById",
                table: "SubTag",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_CreatedById",
                table: "Message",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_DeletedById",
                table: "Message",
                column: "DeletedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_UpdatedById",
                table: "Message",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_UserFromId",
                table: "Message",
                column: "UserFromId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_UserToId",
                table: "Message",
                column: "UserToId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_MainTag_MainTagId",
                table: "Post",
                column: "MainTagId",
                principalTable: "MainTag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_CreatedById",
                table: "Post",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_DeletedById",
                table: "Post",
                column: "DeletedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_UpdatedById",
                table: "Post",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_CreatedById",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_DeletedById",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_UpdatedById",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_UserFromId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_UserToId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_MainTag_MainTagId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_CreatedById",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_DeletedById",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_User_UpdatedById",
                table: "Post");

            migrationBuilder.DropTable(
                name: "PostEntitySubTagEntity");

            migrationBuilder.DropTable(
                name: "SubTag");

            migrationBuilder.DropTable(
                name: "MainTag");

            migrationBuilder.DropIndex(
                name: "IX_Post_MainTagId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "MainTagId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Post");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_CreatedById",
                table: "Message",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_DeletedById",
                table: "Message",
                column: "DeletedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_UpdatedById",
                table: "Message",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_UserFromId",
                table: "Message",
                column: "UserFromId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_UserToId",
                table: "Message",
                column: "UserToId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_CreatedById",
                table: "Post",
                column: "CreatedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_DeletedById",
                table: "Post",
                column: "DeletedById",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_User_UpdatedById",
                table: "Post",
                column: "UpdatedById",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
