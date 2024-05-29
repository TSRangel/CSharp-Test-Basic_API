namespace MinhaApi.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }

        void Commit();
        void Dispose();
    }
}
