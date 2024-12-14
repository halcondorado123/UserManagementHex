using ApiMessage.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiMessage.Application.DTO.UserDTO;

namespace ApiMessage.Application.Interface
{
    public interface IUserApplication
    {
        Task<Response<IEnumerable<UserInfoDTO>>> GetClients();
        Task<Response<UserInfoDTO>> GetClientById(int id);
        Task<Response<UserInfoDTO>> CreateClient(UserInfoDTO client);
        Task<Response<UserInfoDTO>> ModifyClient(int id, UserInfoDTO client);
        Task<Response<bool>> DeleteClient(int id);
    }
}
