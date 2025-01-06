using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ScrumPoker.DataProvider;

public class DapperDataProvider : IDataProvider
{
    private readonly IConfiguration _configuration;
    private string connectionString = "";
    private int timeout = 120;
    public DapperDataProvider(IConfiguration configuration)
    {
        _configuration = configuration;
        connectionString = configuration.GetConnectionString("DefaultConnectionString")!;
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }
    }
    private SqlConnection GetSqlConnection(string connectionString)
    {
        SqlConnection sqlConn = new(connectionString);
        if (sqlConn.State == ConnectionState.Closed)
        {
            sqlConn.Open();
        }
        return sqlConn;
    }

    public async Task<T> Get<T>(string spName)
    {
        using (var conn = GetSqlConnection(connectionString))
        {
            return await conn.QueryFirstOrDefaultAsync<T>(spName, null, commandTimeout: timeout, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<T> Get<T>(string spName, object parameters)
    {
        using (var conn = GetSqlConnection(connectionString))
        {
            return await conn.QueryFirstOrDefaultAsync<T>(spName, parameters, commandTimeout: timeout, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<List<T>> GetList<T>(string spName)
    {
        using (var conn = GetSqlConnection(connectionString))
        {
            return conn.QueryAsync<T>(spName, null, commandTimeout: timeout, commandType: CommandType.StoredProcedure).Result.ToList();
        }
    }

    public async Task<List<T>> GetList<T>(string spName, object parameters)
    {
        using (var conn = GetSqlConnection(connectionString))
        {
            return conn.QueryAsync<T>(spName, parameters, commandTimeout: timeout, commandType: CommandType.StoredProcedure).Result.ToList();
        }
    }

    public async Task<T> Insert<T>(string spName, object data)
    {
        using (var conn = GetSqlConnection(connectionString))
        {
            return conn.ExecuteScalarAsync<T>(spName, data, commandTimeout: timeout, commandType: CommandType.StoredProcedure).Result;
        }
    }

    public async Task<bool> Insert(string spName, IEnumerable<object> data)
    {
        using var conn = GetSqlConnection(connectionString);
        return conn.ExecuteAsync(spName, data, commandTimeout: timeout, commandType: CommandType.StoredProcedure).Result > 0;
    }

    public async Task<bool> Update(string spName, object data)
    {
        using (var conn = GetSqlConnection(connectionString))
        {
            return conn.ExecuteAsync(spName, data, commandTimeout: timeout, commandType: CommandType.StoredProcedure).Result > 0;
        }
    }

    public async Task<bool> Delete(string spName, object data)
    {
        using (var conn = GetSqlConnection(connectionString))
        {

            return conn.ExecuteAsync(spName, data, commandTimeout: timeout, commandType: CommandType.StoredProcedure).Result > 0;
        }
    }
}