using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace JF.OrdemServico.Infra.Data.Context;

public class MongoContext
{
    private readonly IMongoDatabase _database;

    public MongoContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoSettings:ConnectionString"]);
        _database = client.GetDatabase(configuration["MongoSettings:Database"]);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}