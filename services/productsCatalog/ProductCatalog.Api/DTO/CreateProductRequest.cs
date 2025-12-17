public record CreateProductRequest(
    string Name,
    decimal Price,
    string Category,
    Dictionary<string, object>? AdditionalData
);