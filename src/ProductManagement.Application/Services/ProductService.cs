using FluentValidation;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Events;
using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Application.Services;

public class ProductService(
    IProductRepository productRepository, 
    IValidator<CreateProductDto> createProductValidator, 
    IValidator<UpdateProductDto> updateProductValidator,
    IMessagePublisher messagePublisher) : IProductService
{
    public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto createProductDto)
    {
        await createProductValidator.ValidateAndThrowAsync(createProductDto);
        
        var product = new Product()
        {
            Name = createProductDto.Name,
            Description = createProductDto.Description,
            Price = createProductDto.Price,
            Stock = createProductDto.Stock,
            Active = true,
            Id = Guid.NewGuid()
        };
        
        await productRepository.AddAsync(product);
        
        await messagePublisher.PublishAsync(new ProductCreatedEvent
        {
            ProductId = product.Id,
            ProductName = product.Name,
            Price = product.Price,
            Stock = product.Stock
        });
        
        return new ProductResponseDto()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
        };
        
        
    }

    public async Task<ProductResponseDto> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
    {
        await updateProductValidator.ValidateAndThrowAsync(updateProductDto);
        
        var product = await productRepository.GetByIdAsync(id);

        product.Name = updateProductDto.Name;
        product.Description = updateProductDto.Description;
        product.Price = updateProductDto.Price;
        product.Stock = updateProductDto.Stock;
        
        await productRepository.UpdateAsync(product);
        
        if (product.Stock == 0)
        {
            await messagePublisher.PublishAsync(new ProductOutOfStockEvent()
            {
                ProductId = product.Id,
                ProductName = product.Name
            });
        }

        return new ProductResponseDto()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
        };
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        await productRepository.DeleteAsync(product.Id);
    }

    public async Task<ProductResponseDto?> GetProductByIdAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        return new ProductResponseDto()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
        };
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await productRepository.GetAllAsync();
        return products.Select(product => new ProductResponseDto()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
        });
    }
}