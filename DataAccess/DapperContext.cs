using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

public class DapperContext
{
    private readonly IConfiguration _configuration;
    private readonly string _sqlConnection;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _sqlConnection = configuration.GetConnectionString("MyLocalConnection");
    }

    // Método para crear la conexión SQL de manera perezosa cuando se necesite
    public IDbConnection CreateSqlConnection()
    {
        return new SqlConnection(_sqlConnection);
    }
}