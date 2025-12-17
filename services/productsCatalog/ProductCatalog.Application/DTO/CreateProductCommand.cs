namespace ProductCatalog.Application.DTO;
public record CreateProductCommand(
    string Name,
    decimal Price,
    string Category,
    string? AdditionalData
);