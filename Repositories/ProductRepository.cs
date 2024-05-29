using MinhaApi.Context;
using MinhaApi.Models;
using MinhaApi.Repositories.Interfaces;

namespace MinhaApi.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Product> GetProductsByCatetogy(int id)
        {
            return GetAll().Where(p => p.CategoryId == id);
        }
    }
}
