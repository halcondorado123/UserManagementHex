using ApiMessage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiResponse.Domain.Interface
{ 
    public interface IUserDomain
    {
        Task<IEnumerable<UserInfoME>> GetClients();
        Task<UserInfoME?> GetClientById(int id);
        Task<UserInfoME?> CreateClient(UserInfoME client);
        Task<UserInfoME?> UpdateClient(UserInfoME client);
        Task<bool> DeleteClient(int id);
    }
}
