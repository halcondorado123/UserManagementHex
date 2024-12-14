using ApiMessage.Models;
using Dapper;
using System.Data.SqlClient;
using ApiMessage.Data;
using System.Data;

namespace ApiMessage.Infraestructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserInfoME>> GetClientsAsync()
        {
            using (var connection = _context.CreateSqlConnection())
            {
                var query = "SELECT * FROM USU.vwUserInfoME";
                return await connection.QueryAsync<UserInfoME>(query);
            }
        }

        public async Task<UserInfoME> GetClientByIdAsync(int id)
        {
            using (var connection = _context.CreateSqlConnection())
            {
                var query = "SELECT * FROM USU.fnGetUserById(@UserId)";
                var client = await connection.QueryFirstOrDefaultAsync<UserInfoME>(query, new { UserId = id });

                if (client == null)
                {
                    throw new KeyNotFoundException($"Usuario con ID {id} no encontrado.");
                }

                return client;
            }
        }

        public async Task<UserInfoME> CreateAsync(UserInfoME client)
        {
            try
            {
                using (var connection = _context.CreateSqlConnection())
                {
                    var query = "USU.CreateUserInfoME";

                    var parameters = new
                    {
                        FullName = client.FullName,
                        PhoneNumber = client.PhoneNumber,
                        Email = client.Email
                    };

                    var result = await connection.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        return client;
                    }

                    return client;
                }
            }
            catch (SqlException sqlEx)
            {
                throw new ApplicationException("Error en la base de datos al crear el cliente.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al crear el cliente.", ex);
            }
        }

        public async Task<UserInfoME> UpdateClientAsync(UserInfoME client)
        {
            try
            {
                using (var connection = _context.CreateSqlConnection())
                {
                    var query = "USU.UpdateUserInfoME"; 

                    var parameters = new
                    {
                        UserId = client.UserId,
                        FullName = client.FullName,
                        PhoneNumber = client.PhoneNumber,
                        Email = client.Email
                    };

                    var result = await connection.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);

                    if (result > 0)
                    {
                        return client;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al actualizar el cliente", ex);
            }
        }
        public async Task<bool> DeleteClientAsync(int id)
        {
            try
            {
                using (var connection = _context.CreateSqlConnection())
                {
                    var query = "USU.DeleteUserInfoME";
                    var parameters = new { UserId = id };

                    var result = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la eliminación: {ex.Message}");
                return false;
            }
        }
    }
}
