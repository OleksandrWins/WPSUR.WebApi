using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WPSUR.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Messages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
 name: "Messages",
 columns: table => new
 {
     Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
     UserFromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
     UserToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
     Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
     DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
     DeletedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
     CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
     CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
     UpdatedData = table.Column<DateTime>(type: "datetime2", nullable: false),
     UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
 },
 constraints: table =>
 {
     table.PrimaryKey("PK_Messages", x => x.Id);
     table.ForeignKey(
         name: "FK_Messages_Users_CreatedById",
         column: x => x.CreatedById,
         principalTable: "Users",
         principalColumn: "Id");
     table.ForeignKey(
         name: "FK_Messages_Users_DeletedById",
         column: x => x.DeletedById,
         principalTable: "Users",
         principalColumn: "Id");
     table.ForeignKey(
         name: "FK_Messages_Users_UpdatedById",
         column: x => x.UpdatedById,
         principalTable: "Users",
         principalColumn: "Id");
     table.ForeignKey(
         name: "FK_Messages_Users_UserFromId",
         column: x => x.UserFromId,
         principalTable: "Users",
         principalColumn: "Id");
     table.ForeignKey(
         name: "FK_Messages_Users_UserToId",
         column: x => x.UserToId,
         principalTable: "Users",
         principalColumn: "Id");
 });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CreatedById",
                table: "Messages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_DeletedById",
                table: "Messages",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UpdatedById",
                table: "Messages",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserFromId",
                table: "Messages",
                column: "UserFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserToId",
                table: "Messages",
                column: "UserToId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
