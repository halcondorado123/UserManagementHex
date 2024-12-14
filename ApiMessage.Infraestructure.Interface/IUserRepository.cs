using ApiMessage.Models;

namespace ApiMessage.Data
{
    public interface IUserRepository
    {
        IEnumerable<UserInfoME> GetClients(int? userId = null, string? fullName = null, string? email = null);
        UserInfoME GetClientById(int id);
        ApiResponse CreateClient(UserInfoME client);
        ApiResponse ModifyClient(UserInfoME client);
        ApiResponse DeleteClient(int id);
    }
}
