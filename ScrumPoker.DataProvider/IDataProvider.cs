namespace ScrumPoker.DataProvider;

public interface IDataProvider
{
    Task<T> Get<T>(string spName);
    Task<T> Get<T>(string spName, object parameters);
    Task<List<T>> GetList<T>(string spName);
    Task<List<T>> GetList<T>(string spName, object parameters);
    Task<T> Insert<T>(string spName, object data);
    Task<bool> Insert(string spName, IEnumerable<object> data);
    Task<bool> Update(string spName, object data);
    Task<bool> Delete(string spName, object data);
}