using MinhaApi.Context;
using MinhaApi.Repositories.Interfaces;

namespace MinhaApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICategoryRepository _categoryRepository;

        private IProductRepository _productRepository;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICategoryRepository CategoryRepository => 
            _categoryRepository ?? new CategoryRepository(_context);

        public IProductRepository ProductRepository =>
            _productRepository ?? new ProductRepository(_context);

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
