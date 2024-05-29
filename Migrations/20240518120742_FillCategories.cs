using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinhaApi.Migrations
{
    /// <inheritdoc />
    public partial class FillCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("insert into 'categories' (Name, ImageUrl) values ('Bebidas', 'bebidas.jpg')");
            mb.Sql("insert into 'categories' (Name, ImageUrl) values ('Lanches', 'lanches.jpg')");
            mb.Sql("insert into 'categories' (Name, ImageUrl) values ('Sobremesas', 'sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("delete from categories");
        }
    }
}
