using ApiMessage.Models;
using Dapper;
using System.Data;

namespace ApiMessage.Data
{

    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _sqlContext;
    
        public UserRepository(DapperContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public IEnumerable<UserInfoME> GetClients(int? userId = null, string? fullName = null, string? email = null)
        {
            try
            {
                using (var connection = _sqlContext.CreateSqlConnection())
                {
                    var parameters = new
                    {
                        UserId = userId, // Si no se pasa, será null
                        FullName = fullName, // Si no se pasa, será null
                        Email = email // Si no se pasa, será null
                    };

                    // Ejecuta el procedimiento almacenado y pasa los parámetros
                    var users = connection.Query<UserInfoME>(
                        "// Inserte el parametro //",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    return users;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetClients: {ex.Message}");
                return null; // O manejarlo de otra forma si es necesario
            }
        }


        public UserInfoME GetClientById(int id)
        {
            throw new NotImplementedException();
        }


        public ApiResponse CreateClient(UserInfoME client)
        {
            throw new NotImplementedException();
        }

        public ApiResponse ModifyClient(UserInfoME client)
        {
            throw new NotImplementedException();
        }

        public ApiResponse DeleteClient(int id)
        {
            throw new NotImplementedException();
        }
    }
}
