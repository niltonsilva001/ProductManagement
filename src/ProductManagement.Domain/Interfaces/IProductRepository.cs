using ProductManagement.Domain.Entities;
using ProductManagement.Domain.DTOs;

namespace ProductManagement.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product);
        Task<PagedResult<Product>> GetAllAsync(int page, int pageSize);
        Task<Product> GetByIdAsync(Guid id);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
