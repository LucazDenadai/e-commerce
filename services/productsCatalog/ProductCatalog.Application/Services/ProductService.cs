using ProductCatalog.Application.DTO;
using ProductCatalog.Domain.Entities;
using ProductCatalog.Application.Interfaces;

namespace ProductCatalog.Application.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> CreateAsync(CreateProductCommand command)
    {
        var product = new Product(
            command.Name,
            command.Price,
            command.Category,
            command.AdditionalData
        );

        await _productRepository.AddAsync(product);

        return new ProductDto(
            product.Id,
            product.Name,
            product.Price,
            product.Category,
            product.IsActive,
            product.AdditionalData
        );
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null) return null;

        return new ProductDto(
            product.Id,
            product.Name,
            product.Price,
            product.Category,
            product.IsActive,
            product.AdditionalData
        );
    }
}