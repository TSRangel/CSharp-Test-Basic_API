using MinhaApi.Context;
using MinhaApi.Models;
using MinhaApi.Repositories.Interfaces;

namespace MinhaApi.Repositories
{
    public class CategoryRepository : Repository<Category> , ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
    }
}
