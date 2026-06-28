using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
    {
        var result = await productService.CreateProductAsync(productDto);
        return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
    }

    [HttpPut("UpdateProduct/{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto productDto)
    {
        var result = await productService.UpdateProductAsync(id, productDto);
        return Ok(result);
    }

    [HttpDelete("DeleteProduct/{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await productService.DeleteProductAsync(id);
        return NoContent();
    }
    
    [HttpGet("GetProductById/{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var result = await productService.GetProductByIdAsync(id);
        return Ok(result);
    }
    
    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        var result = await productService.GetAllProductsAsync();
        return Ok(result);
    }
}