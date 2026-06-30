using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.DTOs;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Infrastructure.Data;

namespace ProductManagement.Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public async Task<Product> AddAsync(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<PagedResult<Product>> GetAllAsync(string searchTerm, int page, int pageSize, SortBy sortBy, bool ascending)
    {
        var query = context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower().Trim();
            query = query.Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%"));
        }

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        query = sortBy switch
        {
            SortBy.Name => ascending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
            SortBy.Price => ascending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price),
            SortBy.Stock => ascending ? query.OrderBy(p => p.Stock) : query.OrderByDescending(p => p.Stock),
            SortBy.CreatedAt => ascending ? query.OrderBy(p => p.CreatedAt) : query.OrderByDescending(p => p.CreatedAt),
            _ => query
        };

        var products = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Product>()   
        {
            Data = products,
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = page,
            PageSize = pageSize
        };
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        if(product != null)
        {
            return product;
        }
        throw new KeyNotFoundException($"Product with ID {id} not found.");
    }

    public async Task UpdateAsync(Product product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        if (product != null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return;
        }
        throw new KeyNotFoundException($"Product with ID {id} not found.");
    }
}