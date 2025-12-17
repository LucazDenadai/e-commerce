public record ProductResponse(
    Guid Id,
    string Name,
    decimal Price,
    string Category,
    bool IsActive,
    Dictionary<string, object> AdditionalData
);