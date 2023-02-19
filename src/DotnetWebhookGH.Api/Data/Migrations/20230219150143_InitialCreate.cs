using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetWebhookGH.Api.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "label",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    color = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    is_default = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_label", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "repository",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false),
                    owner_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    language = table.Column<string>(type: "text", nullable: true),
                    default_branch = table.Column<string>(type: "text", nullable: true),
                    topics = table.Column<string>(type: "text", nullable: true),
                    is_private = table.Column<bool>(type: "boolean", nullable: false),
                    is_fork = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_repository", x => x.id);
                    table.ForeignKey(
                        name: "FK_repository_user_owner_id",
                        column: x => x.owner_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "issue",
                schema: "public",
                columns: table => new
                {
                    delivery = table.Column<string>(type: "text", nullable: false),
                    @event = table.Column<string>(name: "event", type: "text", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    repository_id = table.Column<long>(type: "bigint", nullable: false),
                    sender_id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    body = table.Column<string>(type: "text", nullable: true),
                    state = table.Column<string>(type: "text", nullable: false),
                    locked = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    closed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issue", x => x.delivery);
                    table.ForeignKey(
                        name: "FK_issue_repository_repository_id",
                        column: x => x.repository_id,
                        principalSchema: "public",
                        principalTable: "repository",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_issue_user_sender_id",
                        column: x => x.sender_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "issue_assignee",
                schema: "public",
                columns: table => new
                {
                    issue_delivery = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issue_assignee", x => new { x.issue_delivery, x.user_id });
                    table.ForeignKey(
                        name: "FK_issue_assignee_issue_issue_delivery",
                        column: x => x.issue_delivery,
                        principalSchema: "public",
                        principalTable: "issue",
                        principalColumn: "delivery",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_issue_assignee_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "issue_label",
                schema: "public",
                columns: table => new
                {
                    issue_delivery = table.Column<string>(type: "text", nullable: false),
                    label_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issue_label", x => new { x.issue_delivery, x.label_id });
                    table.ForeignKey(
                        name: "FK_issue_label_issue_issue_delivery",
                        column: x => x.issue_delivery,
                        principalSchema: "public",
                        principalTable: "issue",
                        principalColumn: "delivery",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_issue_label_label_label_id",
                        column: x => x.label_id,
                        principalSchema: "public",
                        principalTable: "label",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reaction",
                schema: "public",
                columns: table => new
                {
                    delivery = table.Column<string>(type: "text", nullable: false),
                    total_count = table.Column<int>(type: "integer", nullable: false),
                    one_plus = table.Column<int>(type: "integer", nullable: false),
                    one_minus = table.Column<int>(type: "integer", nullable: false),
                    laugh = table.Column<int>(type: "integer", nullable: false),
                    hooray = table.Column<int>(type: "integer", nullable: false),
                    confused = table.Column<int>(type: "integer", nullable: false),
                    heart = table.Column<int>(type: "integer", nullable: false),
                    rocket = table.Column<int>(type: "integer", nullable: false),
                    eyes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reaction", x => x.delivery);
                    table.ForeignKey(
                        name: "FK_reaction_issue_delivery",
                        column: x => x.delivery,
                        principalSchema: "public",
                        principalTable: "issue",
                        principalColumn: "delivery",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_issue_number",
                schema: "public",
                table: "issue",
                column: "number");

            migrationBuilder.CreateIndex(
                name: "IX_issue_repository_id",
                schema: "public",
                table: "issue",
                column: "repository_id");

            migrationBuilder.CreateIndex(
                name: "IX_issue_sender_id",
                schema: "public",
                table: "issue",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_issue_updated_at",
                schema: "public",
                table: "issue",
                column: "updated_at");

            migrationBuilder.CreateIndex(
                name: "IX_issue_assignee_user_id",
                schema: "public",
                table: "issue_assignee",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_issue_label_label_id",
                schema: "public",
                table: "issue_label",
                column: "label_id");

            migrationBuilder.CreateIndex(
                name: "IX_repository_name",
                schema: "public",
                table: "repository",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_repository_owner_id",
                schema: "public",
                table: "repository",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_login",
                schema: "public",
                table: "user",
                column: "login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "issue_assignee",
                schema: "public");

            migrationBuilder.DropTable(
                name: "issue_label",
                schema: "public");

            migrationBuilder.DropTable(
                name: "reaction",
                schema: "public");

            migrationBuilder.DropTable(
                name: "label",
                schema: "public");

            migrationBuilder.DropTable(
                name: "issue",
                schema: "public");

            migrationBuilder.DropTable(
                name: "repository",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user",
                schema: "public");
        }
    }
}
