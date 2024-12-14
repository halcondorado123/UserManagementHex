using ApiMessage.Models;

namespace ApiMessage.Data
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserInfoME>> GetClientsAsync();
        Task<UserInfoME> GetClientByIdAsync(int id);
        Task<UserInfoME> CreateAsync(UserInfoME client);
        Task<UserInfoME> UpdateClientAsync(UserInfoME client);
        Task<bool> DeleteClientAsync(int id);
    }
}

