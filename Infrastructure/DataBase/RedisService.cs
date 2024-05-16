using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using Domain.Interfaces.Services;

namespace Infrastructure.DataBase;

public class RedisService:IRedisService
{
    private readonly ConnectionMultiplexer _redis;

    public RedisService(IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Redis");
        _redis = ConnectionMultiplexer.Connect(connectionString);
    }

    public void SetValue(string key, string value)
    {
        var db = _redis.GetDatabase();
        db.StringSet(key, value);
    }

    public string GetValue(string key)
    {
        var db = _redis.GetDatabase();
        return db.StringGet(key);
    }
}
