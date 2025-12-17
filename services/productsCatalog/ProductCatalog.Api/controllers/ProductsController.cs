using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Services;
using ProductCatalog.Application.DTO;
using System.Text.Json;

namespace ProductCatalog.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        var additionalDataJson = request.AdditionalData is null
        ? null
        : JsonSerializer.Serialize(request.AdditionalData);

        var command = new CreateProductCommand(
            request.Name,
            request.Price,
            request.Category,
            additionalDataJson
        );

        var product = await _productService.CreateAsync(command);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);
        if (product is null)
            return NotFound();

        return Ok(product);
    }
}
