using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlueRaven.Data.Migrations
{
    public partial class addblogsandposts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ByLine = table.Column<string>(nullable: true),
                    Disclaimer = table.Column<string>(nullable: true),
                    LocalUrl = table.Column<string>(nullable: true),
                    Theme = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    BlogId = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    Excerpt = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    PubDate = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    Categories = table.Column<string>(nullable: true),
                    Keywords = table.Column<string>(nullable: true),
                    IsPublished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "ByLine", "Disclaimer", "LocalUrl", "Theme", "Title", "Url" },
                values: new object[] { "blog1", "Powered by Blue Raven", "Read at your own risk!", "https://localhost:5001", "Default", "Blue Raven", "https://example.com" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Author", "BlogId", "Categories", "Content", "Excerpt", "IsPublished", "Keywords", "LastModified", "PubDate", "Slug", "Title" },
                values: new object[] { 1, "Me", "blog1", "", @"Bacon ipsum dolor amet doner drumstick swine prosciutto. Swine boudin chuck tongue flank beef venison. Turducken strip steak beef ribs boudin. Pastrami jowl pork loin tri-tip. Ham hock biltong buffalo, pork short ribs spare ribs turducken strip steak prosciutto swine porchetta venison bresaola tongue doner. Porchetta bresaola pork chop venison. Boudin picanha burgdoggen drumstick meatball pork chop cow cupim shoulder tenderloin turducken filet mignon.
Fatback sausage chicken spare ribs, rump pork chop picanha strip steak buffalo. Burgdoggen beef frankfurter, spare ribs hamburger pork loin alcatra pork belly doner ground round biltong. Strip steak short loin leberkas ball tip salami brisket drumstick spare ribs. Ham bacon buffalo bresaola, short loin cupim flank rump pastrami tenderloin porchetta venison ball tip burgdoggen t-bone. Swine hamburger pork prosciutto pig pork belly jerky venison flank shoulder bacon pancetta. Salami pork chop rump, biltong kielbasa filet mignon pig.
Ball tip burgdoggen bresaola pancetta. Biltong pork loin buffalo, hamburger flank beef ribs shankle pig ground round ball tip. Pork loin kevin beef ribs turducken, flank ham hock short loin cupim beef t-bone ribeye ground round pig fatback. Kevin salami beef swine. Flank pig burgdoggen sirloin.", "Bacon ipsum dolor amet doner drumstick swine prosciutto. Swine boudin chuck tongue flank beef venison. Turducken strip steak beef ribs boudin. Pastrami jowl pork loin tri-tip. Ham hock biltong buffalo, pork short ribs spare ribs turducken strip...", true, "", new DateTime(2018, 8, 9, 2, 50, 24, 752, DateTimeKind.Utc), new DateTime(2018, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "First post" });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Author", "BlogId", "Categories", "Content", "Excerpt", "IsPublished", "Keywords", "LastModified", "PubDate", "Slug", "Title" },
                values: new object[] { 2, "Me", "blog1", "", @"Spicy jalapeno ground round short loin pork chop, beef ribs t-bone meatball beef. Beef ribs tail filet mignon, drumstick sirloin kevin fatback jerky flank swine buffalo prosciutto. Short loin chuck capicola pastrami turkey bacon filet mignon. Pork chop pancetta beef boudin. Ball tip swine ground round tongue short ribs tenderloin pig landjaeger flank fatback shankle drumstick cupim.
Bresaola t-bone tri-tip chicken ham hock shankle brisket. Landjaeger shank doner meatball, tenderloin rump prosciutto frankfurter jerky. Filet mignon salami beef fatback beef ribs, andouille strip steak meatloaf prosciutto t-bone pork belly brisket ground round tail. Tenderloin jowl ribeye swine venison, short loin spare ribs short ribs burgdoggen pork chop shank shoulder. Hamburger bacon burgdoggen, turducken boudin ground round filet mignon flank shoulder. Pig meatloaf ham, hamburger turkey porchetta pastrami rump. Salami chicken porchetta, shoulder ham landjaeger t-bone turkey pancetta.
Shoulder boudin pork brisket, picanha meatball tongue sausage ball tip spare ribs venison flank leberkas burgdoggen pork belly. Flank spare ribs rump leberkas, beef ribs andouille ham hock tri-tip alcatra doner t-bone. Tongue cupim jowl, jerky chuck prosciutto chicken ball tip shank fatback buffalo doner flank. Beef ribs sausage t-bone landjaeger flank salami pork kevin beef bacon short ribs pork loin ham ball tip. Sausage beef buffalo, cupim kielbasa kevin turkey. Pork loin bresaola tri-tip pork chop jerky salami corned beef ball tip tail. Ham cow pig turkey, t-bone beef leberkas boudin shoulder burgdoggen prosciutto meatloaf pastrami tail.", "Spicy jalapeno ground round short loin pork chop, beef ribs t-bone meatball beef. Beef ribs tail filet mignon, drumstick sirloin kevin fatback jerky flank swine buffalo prosciutto. Short loin chuck capicola pastrami turkey bacon filet mignon. Pork...", true, "", new DateTime(2018, 8, 9, 2, 50, 24, 752, DateTimeKind.Utc), new DateTime(2018, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Next post" });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
