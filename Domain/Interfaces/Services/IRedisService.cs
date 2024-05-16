namespace Domain.Interfaces.Services;

public interface IRedisService
{
    void SetValue(string key, string value);
    string GetValue(string key);
}
