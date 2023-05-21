using System;
using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using MyWebAPITest.Data;

#nullable disable

namespace MyWebAPITest.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookStore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prices = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStore", x => x.Id);
                });
            Randomizer.Seed = new Random(988098);
            var bookFaker = new Faker<Book>()
                            .RuleFor(b => b.Name, f => f.Lorem.Sentence(5, 5))
                            .RuleFor(b => b.Title, f => f.Lorem.Sentence(5, 5))
                            .RuleFor(b => b.Description, f => f.Lorem.Paragraphs(1, 2))
                            .RuleFor(b => b.Author, f => f.Lorem.Sentence(1, 4))
                            .RuleFor(b => b.Prices, f => f.Random.Float(1000, 5000))
                            .RuleFor(b => b.Discount, f => (byte)f.Random.Number(1, 100));

            for (int i = 0; i < 100; i++)
            {
                var book = bookFaker.Generate();
                migrationBuilder.InsertData(
                    table: "BookStore",
                    columns: new[] {"Id", "Name", "Title", "Description", "Author", "Prices", "Discount" },
                    values: new object[] {Guid.NewGuid(), book.Name, book.Title, book.Description, book.Author, book.Prices, (int)book.Discount }
                    );

            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookStore");
        }
    }
}
