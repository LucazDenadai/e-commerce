public record CreateProductRequest(
    string Name,
    decimal Price,
    string Category,
    object? AdditionalData
);