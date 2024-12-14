﻿using ApiMessage.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiResponse.Domain.Interface
{
    public interface IUserDomain
    {
        Task<IEnumerable<UserInfoME>> GetClientsAsync();
        Task<UserInfoME> GetClientByIdAsync(int id);
        Task<UserInfoME> CreateAsync(UserInfoME client);
        Task<UserInfoME> UpdateClientAsync(UserInfoME client);
        Task<bool> DeleteClientAsync(int id);
    }
}
