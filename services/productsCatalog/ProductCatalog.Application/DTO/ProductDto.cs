namespace ProductCatalog.Application.DTO;
public record ProductDto(
    Guid Id,
    string Name,
    decimal Price,
    string Category,
    bool IsActive,
    string? AdditionalData
);