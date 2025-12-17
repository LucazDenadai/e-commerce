using ProductCatalog.Application.DTO;


namespace ProductCatalog.Application.Services;
public interface IProductService
{
    Task<ProductDto> CreateAsync(CreateProductCommand command);
    Task<ProductDto?> GetByIdAsync(Guid id);
}
