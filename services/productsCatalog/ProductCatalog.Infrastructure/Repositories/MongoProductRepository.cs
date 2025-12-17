using MongoDB.Driver;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Repositories;

public class MongoProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _collection;

    public MongoProductRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Product>("products");
    }

    public async Task AddAsync(Product product)
    {
        await _collection.InsertOneAsync(product);
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _collection
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }
}