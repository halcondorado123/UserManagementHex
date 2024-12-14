using ApiMessage.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                var query = "SELECT * FROM USU.vwUserInfoME";  // Aquí no filtras por ID, sino que obtienes todos los registros
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


        // Método para crear un cliente
        public async Task<UserInfoME> CreateAsync(UserInfoME client)
        {
            try
            {
                using (var connection = _context.CreateSqlConnection())
                {
                    var query = "USU.CreateUserInfoME"; // Nombre del procedimiento almacenado

                    // Parámetros que se enviarán al procedimiento almacenado
                    var parameters = new
                    {
                        FullName = client.FullName,
                        PhoneNumber = client.PhoneNumber,
                        Email = client.Email
                    };

                    var result = await connection.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        return client;  // Procedimiento ejecutado correctamente
                    }

                    return client; // Retorna el cliente creado si la inserción fue exitosa
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo de errores específicos de SQL
                throw new ApplicationException("Error en la base de datos al crear el cliente.", sqlEx);
            }
            catch (Exception ex)
            {
                // Manejo de errores generales
                throw new ApplicationException("Error al crear el cliente.", ex);
            }
        }

        // Método para actualizar un cliente
        public async Task<UserInfoME> UpdateClientAsync(UserInfoME client)
        {
            try
            {
                using (var connection = _context.CreateSqlConnection())
                {
                    var query = "USU.UpdateUserInfoME"; // Nombre del procedimiento almacenado

                    var parameters = new
                    {
                        UserId = client.UserId,
                        FullName = client.FullName,
                        PhoneNumber = client.PhoneNumber,
                        Email = client.Email
                    };

                    // Ejecutar el procedimiento almacenado
                    var result = await connection.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);

                    if (result > 0)
                    {
                        return client;  // Si la actualización fue exitosa, devolvemos el cliente actualizado
                    }
                    else
                    {
                        return null;  // Si no hubo ninguna fila afectada, retornamos null
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al actualizar el cliente", ex);
            }
        }



        // Método para eliminar un cliente por ID
        public async Task<bool> DeleteClientAsync(int id)
        {
            try
            {
                using (var connection = _context.CreateSqlConnection())
                {
                    var query = "USU.DeleteUserInfoME"; // Nombre del procedimiento almacenado
                    var parameters = new { UserId = id };

                    var result = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);

                    // Si el valor de result es mayor que 0, significa que se eliminó al menos un registro
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores, puedes loguear el error para saber qué pasa
                Console.WriteLine($"Error en la eliminación: {ex.Message}");
                return false;  // Devuelves false en caso de error
            }
        }
    }
}
