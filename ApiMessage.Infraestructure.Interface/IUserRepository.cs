using ApiMessage.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMessage.Data
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserInfoME>> GetClientsAsync(); // Método para obtener todos los clientes
        Task<UserInfoME> GetClientByIdAsync(int id); // Método para obtener un solo cliente por ID
        Task<UserInfoME> CreateAsync(UserInfoME client); // Método para crear un cliente
        Task<UserInfoME> UpdateClientAsync(UserInfoME client); // Método para actualizar un cliente
        Task<bool> DeleteClientAsync(int id); // Método para eliminar un cliente por ID (ahora devuelve un bool)
    }
}

