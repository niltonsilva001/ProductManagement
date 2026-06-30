using ProductManagement.Domain.Entities;
using ProductManagement.Domain.DTOs;
using ProductManagement.Domain.Enums;

namespace ProductManagement.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product);
        Task<PagedResult<Product>> GetAllAsync(string searchTerm, int page, int pageSize, SortBy sortBy, bool ascending);
        Task<Product> GetByIdAsync(Guid id);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
