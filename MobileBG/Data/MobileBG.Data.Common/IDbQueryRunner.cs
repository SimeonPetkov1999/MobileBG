namespace MobileBG.Data.Common;

public interface IDbQueryRunner : IDisposable
{
    Task RunQueryAsync(string query, params object[] parameters);
}