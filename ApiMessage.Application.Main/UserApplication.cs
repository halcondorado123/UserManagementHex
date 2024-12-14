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


        /// <summary>
        /// Retrieves all clients as a collection of DTOs.
        /// </summary>
        /// <returns>A response containing a collection of UserInfoDTO objects.</returns>
        public async Task<Response<IEnumerable<UserInfoDTO>>> GetClients()
        {
            var response = new Response<IEnumerable<UserInfoDTO>>();
            try
            {
                // Aquí, asumo que _userDomain.GetClients() es asincrónico
                var user = await _userDomain.GetClients();  // Usar await para llamar a la versión asincrónica
                response.Data = _mapper.Map<IEnumerable<UserInfoDTO>>(user); // Mapear como colección

                if (response.Data != null && response.Data.Any()) // Verificar que haya datos
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
                response.Message = $"Error: {e.Message}"; // Incluir más información en el error
            }
            return response;
        }

        /// <summary>
        /// Retrieves a specific client by their ID.
        /// </summary>
        /// <param name="id">The unique identifier of the client.</param>
        /// <returns>A response containing the UserInfoDTO object if found.</returns>
        public async Task<Response<UserInfoDTO>> GetClientById(int id)
        {
            var response = new Response<UserInfoDTO>();
            try
            {
                var user = _userDomain.GetClientById(id);
                response.Data = _mapper.Map<UserInfoDTO>(user); // Mapear como colección

                if (response.Data != null) // Verificar que haya datos
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
                response.Message = $"Error: {e.Message}"; // Incluir más información en el error
            }
            return response;
        }

        /// <summary>
        /// Creates a new client.
        /// </summary>
        /// <param name="client">The DTO containing the client information to create.</param>
        /// <returns>A response containing the created UserInfoDTO object.</returns>
        public async Task<Response<UserInfoDTO>> CreateClient(UserInfoDTO client)
        {
            var response = new Response<UserInfoDTO>();
            try
            {
                // Mapea el DTO a la entidad de dominio
                var userEntity = _mapper.Map<UserInfoME>(client);

                // Llama al método de dominio para guardar el nuevo cliente
                var createdUser = await _userDomain.CreateClient(userEntity);  // Asegúrate de que CreateClient en _userDomain sea asincrónico

                // Mapea el resultado al DTO
                response.Data = _mapper.Map<UserInfoDTO>(createdUser);

                if (response.Data != null) // Verifica si se creó el usuario correctamente
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
                response.Message = $"Error: {e.Message}";  // Incluir más información en el error
            }

            return await Task.FromResult(response);  // Devuelve el resultado como un Task
        }

        /// <summary>
        /// Updates an existing client.
        /// </summary>
        /// <param name="client">The DTO containing the updated client information.</param>
        /// <returns>A response containing the updated UserInfoDTO object.</returns>
        public async Task<Response<UserInfoDTO>> ModifyClient(int id, UserInfoDTO client)
        {
            var response = new Response<UserInfoDTO>();
            try
            {
                // Mapea el DTO a la entidad de dominio
                var userEntity = _mapper.Map<UserInfoME>(client);

                // Asegúrate de que el id del DTO coincide con el id pasado
                userEntity.UserId = id;

                // Llama al método de dominio para actualizar el cliente
                var updatedUser = await _userDomain.UpdateClient(userEntity);  // Asegúrate de que UpdateClient en _userDomain sea asincrónico

                // Verifica si se actualizó el usuario correctamente
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
                response.Message = $"Error: {e.Message}";  // Incluir más información en el error
            }

            return await Task.FromResult(response);  // Devuelve el resultado como un Task
        }


        public async Task<Response<bool>> DeleteClient(int id)
        {
            var response = new Response<bool>();
            try
            {
                // Llama al método de dominio para obtener el cliente
                var user = await _userDomain.GetClientById(id); // Asegúrate de que GetClientById sea asincrónico

                if (user != null)
                {
                    // Si el usuario existe, llama al método para eliminarlo
                    var isDeleted = await _userDomain.DeleteClient(id);  // Asegúrate de que DeleteClient sea asincrónico

                    if (isDeleted)
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
                else
                {
                    response.Data = false;
                    response.IsSuccess = false;
                    response.Message = "Client not found.";
                }
            }
            catch (Exception e)
            {
                response.Data = false;
                response.IsSuccess = false;
                response.Message = $"Error: {e.Message}"; // Incluir más información en el error
            }

            return response;
        }
    }
}
