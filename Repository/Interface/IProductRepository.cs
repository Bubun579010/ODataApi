using Models.Entity;

namespace Repository.Interface
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAllQueryable();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Product product);
    }
}
