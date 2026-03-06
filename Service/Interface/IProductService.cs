using DTO.Product;

namespace Service.Interface
{
    public interface IProductService
    {
        IQueryable<ProductDto> GetAllQueryable();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(ProductRequest request);
        Task<ProductDto> UpdateAsync(ProductRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> SoftDeleteAsync(int id);
    }
}
