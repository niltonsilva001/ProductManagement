using ProductManagement.Application.DTOs;
using ProductManagement.Domain.DTOs;

namespace ProductManagement.Application.Interfaces;

public interface IProductService
{
    Task<ProductResponseDto> CreateProductAsync(CreateProductDto createProductDto);
    Task<ProductResponseDto> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto);
    Task DeleteProductAsync(Guid id);
    Task<ProductResponseDto?> GetProductByIdAsync(Guid id);
    Task<PagedResult<ProductResponseDto>> GetAllAsync(string? searchTerm, int page, int pageSize, string? sortBy, string? sortOrder);
}