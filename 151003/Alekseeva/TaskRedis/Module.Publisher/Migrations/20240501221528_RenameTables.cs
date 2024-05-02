using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Publisher.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagTweet_tbl_tags_TagsId",
                table: "TagTweet");

            migrationBuilder.DropForeignKey(
                name: "FK_TagTweet_tbl_tweets_TweetsId",
                table: "TagTweet");

            migrationBuilder.DropTable(
                name: "tbl_posts");

            migrationBuilder.DropTable(
                name: "tbl_tags");

            migrationBuilder.DropTable(
                name: "tbl_tweets");

            migrationBuilder.DropTable(
                name: "tbl_creators");

            migrationBuilder.CreateTable(
                name: "tbl_creator",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    password = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    firstname = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    lastname = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_creator", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_tag",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_tag", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_tweet",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creator_id = table.Column<long>(type: "bigint", nullable: true),
                    title = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    content = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_tweet", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_tweet_tbl_creator_creator_id",
                        column: x => x.creator_id,
                        principalTable: "tbl_creator",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_creator_login",
                table: "tbl_creator",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_tag_name",
                table: "tbl_tag",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_tweet_creator_id",
                table: "tbl_tweet",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_tweet_title",
                table: "tbl_tweet",
                column: "title",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TagTweet_tbl_tag_TagsId",
                table: "TagTweet",
                column: "TagsId",
                principalTable: "tbl_tag",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagTweet_tbl_tweet_TweetsId",
                table: "TagTweet",
                column: "TweetsId",
                principalTable: "tbl_tweet",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagTweet_tbl_tag_TagsId",
                table: "TagTweet");

            migrationBuilder.DropForeignKey(
                name: "FK_TagTweet_tbl_tweet_TweetsId",
                table: "TagTweet");

            migrationBuilder.DropTable(
                name: "tbl_tag");

            migrationBuilder.DropTable(
                name: "tbl_tweet");

            migrationBuilder.DropTable(
                name: "tbl_creator");

            migrationBuilder.CreateTable(
                name: "tbl_creators",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    LastName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Login = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Password = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_creators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_tags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_tweets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatorId = table.Column<long>(type: "bigint", nullable: true),
                    Content = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Title = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_tweets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_tweets_tbl_creators_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "tbl_creators",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_posts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TweetId = table.Column<long>(type: "bigint", nullable: true),
                    Content = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_posts_tbl_tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "tbl_tweets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_creators_Login",
                table: "tbl_creators",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_posts_TweetId",
                table: "tbl_posts",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_tags_Name",
                table: "tbl_tags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_tweets_CreatorId",
                table: "tbl_tweets",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_tweets_Title",
                table: "tbl_tweets",
                column: "Title",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TagTweet_tbl_tags_TagsId",
                table: "TagTweet",
                column: "TagsId",
                principalTable: "tbl_tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagTweet_tbl_tweets_TweetsId",
                table: "TagTweet",
                column: "TweetsId",
                principalTable: "tbl_tweets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
