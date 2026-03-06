using DTO.Product;
using Models.Entity;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public IQueryable<ProductDto> GetAllQueryable()
        {
            return _repo.GetAllQueryable().Where(p => p.Status == true)
                .Select(p => new ProductDto
                {
                    Id = p.ProductId,
                    Name = p.Name,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    Category = p.Category,
                    Description = p.Description,
                    Status = p.Status,
                    CreatedOn = p.CreatedOn
                });
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) { return null; }
            return MapToDto(product);
        }

        public async Task<ProductDto> CreateAsync(ProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Quantity = request.Quantity,
                Price = request.Price,
                Category = request.Category,
                Description = request.Description,
                Status = true,
                CreatedOn = DateTime.UtcNow
            };
            var created = await _repo.AddAsync(product);
            return MapToDto(created);
        }

        public async Task<ProductDto> UpdateAsync(ProductRequest request)
        {
            var result = await _repo.GetByIdAsync(request.Id);
            if (result == null) throw new Exception("Product not found.");

            result.Name = request.Name;
            result.Quantity = request.Quantity;
            result.Price = request.Price;
            result.Category = request.Category;
            result.Description = request.Description;
            result.Status = true;

            var updated = await _repo.UpdateAsync(result);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) { return false; }
            return await _repo.DeleteAsync(product);
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) { return false; }
            ;
            product.Status = false;
            var updated = await _repo.UpdateAsync(product);
            return updated != null;
        }

        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.ProductId,
                Name = product.Name,
                Quantity = product.Quantity,
                Price = product.Price,
                Category = product.Category,
                Description = product.Description,
                Status = product.Status,
                CreatedOn = product.CreatedOn
            };
        }
    }
}
