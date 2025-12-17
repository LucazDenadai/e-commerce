namespace ProductCatalog.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Category { get; private set; }
    public bool IsActive { get; private set; }

    public Dictionary<string, object> AdditionalData { get; private set; }

    protected Product() { } // ORM / Mongo

    public Product(
        string name,
        decimal price,
        string category,
        Dictionary<string, object>? additionalData = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Category = category;
        IsActive = true;
        AdditionalData = additionalData ?? new();
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("Price must be greater than zero");

        Price = newPrice;
    }
}