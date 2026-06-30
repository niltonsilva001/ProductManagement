using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.DTOs;
using ProductManagement.Domain.Entities;
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

    public async Task<PagedResult<Product>> GetAllAsync(string searchTerm, int page, int pageSize)
    {
        var query = context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower().Trim();
            query = query.Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%"));
        }

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
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