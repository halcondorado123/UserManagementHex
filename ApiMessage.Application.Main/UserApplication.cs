using ApiMessage.Application.DTO.UserDTO;
using ApiMessage.Application.Interface;
using ApiMessage.Models;
using ApiMessage.Transversal.Common;
using ApiResponse.Domain.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMessage.Application.Main
{
    public class UserApplication : IUserApplication
    {
        public readonly IUserDomain _userDomain;
        public readonly IMapper _mapper;
        public readonly IAppLogger<UserApplication> _Logger;

        public UserApplication(IUserDomain userDomain, IMapper mapper, IAppLogger<UserApplication> logger)
        {
            _userDomain = userDomain;
            _mapper = mapper;
            _Logger = logger;
        }


        public async Task<Response<IEnumerable<UserInfoDTO>>> GetClients()
        {
            var response = new Response<IEnumerable<UserInfoDTO>>();
            try
            {
                var user = await _userDomain.GetClientsAsync(); 
                response.Data = _mapper.Map<IEnumerable<UserInfoDTO>>(user); 

                if (response.Data != null && response.Data.Any())
                {
                    response.IsSuccess = true;
                    response.Message = "Successful Search";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Unsuccessful Search";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}"; 
            }
            return response;
        }

        public async Task<Response<UserInfoDTO>> GetClientById(int id)
        {
            var response = new Response<UserInfoDTO>();
            try
            {
                var user = _userDomain.GetClientByIdAsync(id);
                response.Data = _mapper.Map<UserInfoDTO>(user); 

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Successful Search";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Unsuccessful Search";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}"; 
            }
            return response;
        }

        public async Task<Response<UserInfoDTO>> CreateClient(UserInfoDTO client)
        {
            var response = new Response<UserInfoDTO>();
            try
            {
                var userEntity = _mapper.Map<UserInfoME>(client);

                var createdUser = await _userDomain.CreateAsync(userEntity); 

                response.Data = _mapper.Map<UserInfoDTO>(createdUser);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Client created successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Failed to create client.";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}"; 
            }

            return await Task.FromResult(response);  
        }
        public async Task<Response<UserInfoDTO>> ModifyClient(int id, UserInfoDTO client)
        {
            var response = new Response<UserInfoDTO>();
            try
            {
                var userEntity = _mapper.Map<UserInfoME>(client);

                userEntity.UserId = id;

                var updatedUser = await _userDomain.UpdateClientAsync(userEntity);  

                if (updatedUser != null)
                {
                    response.Data = _mapper.Map<UserInfoDTO>(updatedUser);
                    response.IsSuccess = true;
                    response.Message = "Client updated successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Client not found or update failed.";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}";  
            }

            return await Task.FromResult(response);  
        }

        public async Task<Response<bool>> DeleteClient(int id)
        {
            var response = new Response<bool>();
            try
            {
                var user = await _userDomain.DeleteClientAsync(id); 

                if (user == null)
                {
                    response.Data = false;
                    response.IsSuccess = false;
                    response.Message = "Client not found.";
                    return response;
                }

                var deletedUser = await _userDomain.DeleteClientAsync(id); 

                if (deletedUser != null)
                {
                    response.Data = true;
                    response.IsSuccess = true;
                    response.Message = "Client deleted successfully.";
                }
                else
                {
                    response.Data = false;
                    response.IsSuccess = false;
                    response.Message = "Failed to delete client.";
                }
            }
            catch (Exception e)
            {
                response.Data = false;
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}"; 
            }

            return response;
        }
    }
}
