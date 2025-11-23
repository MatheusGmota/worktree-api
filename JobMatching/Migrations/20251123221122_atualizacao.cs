using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobMatching.Migrations
{
    /// <inheritdoc />
    public partial class atualizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "job_location",
                table: "JOB",
                newName: "JOB_LOCATION");

            migrationBuilder.CreateTable(
                name: "USER_table",
                columns: table => new
                {
                    ID_USER = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USER_NAME = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    user_password = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    USER_ROLE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    user_description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_table", x => x.ID_USER);
                });

            migrationBuilder.CreateTable(
                name: "APPLICATION",
                columns: table => new
                {
                    ID_APPLICATION = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_JOB = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    candidate_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    APP_STATUS = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APPLICATION", x => x.ID_APPLICATION);
                    table.ForeignKey(
                        name: "FK_APPLICATION_JOB_ID_JOB",
                        column: x => x.ID_JOB,
                        principalTable: "JOB",
                        principalColumn: "ID_JOB",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_APPLICATION_USER_table_candidate_id",
                        column: x => x.candidate_id,
                        principalTable: "USER_table",
                        principalColumn: "ID_USER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_user_skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    skill = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ID_USER = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_user_skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_user_skills_USER_table_ID_USER",
                        column: x => x.ID_USER,
                        principalTable: "USER_table",
                        principalColumn: "ID_USER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APPLICATION_candidate_id",
                table: "APPLICATION",
                column: "candidate_id");

            migrationBuilder.CreateIndex(
                name: "IX_APPLICATION_ID_JOB",
                table: "APPLICATION",
                column: "ID_JOB");

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_skills_ID_USER",
                table: "tb_user_skills",
                column: "ID_USER");

            migrationBuilder.CreateIndex(
                name: "IX_USER_table_USER_NAME",
                table: "USER_table",
                column: "USER_NAME",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APPLICATION");

            migrationBuilder.DropTable(
                name: "tb_user_skills");

            migrationBuilder.DropTable(
                name: "USER_table");

            migrationBuilder.RenameColumn(
                name: "JOB_LOCATION",
                table: "JOB",
                newName: "job_location");
        }
    }
}
