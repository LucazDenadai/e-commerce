using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Infrastructure.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ProductCatalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        BsonSerializer.RegisterSerializer(
            new GuidSerializer(GuidRepresentation.Standard)
        );

        var mongoSection = configuration.GetSection("Mongo");

        services.AddSingleton<IMongoClient>(_ =>
            new MongoClient(mongoSection["ConnectionString"])
        );

        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(mongoSection["Database"]);
        });

        services.AddScoped<IProductRepository, MongoProductRepository>();

        return services;
    }
}