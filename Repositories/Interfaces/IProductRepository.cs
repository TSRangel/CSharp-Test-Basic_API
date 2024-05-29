using MinhaApi.Models;

namespace MinhaApi.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsByCatetogy(int id);
    }
}
