using ApiMessage.Models;
using ApiMessage.Data;
using ApiResponse.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMessage.Domain.Core
{
    public class UserDomain : IUserDomain
    {
        private readonly IUserRepository _userRepository;

        public UserDomain(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserInfoME>> GetClientsAsync()
        {
            return await _userRepository.GetClientsAsync(); // Ahora es asincrónico
        }

        public async Task<UserInfoME> GetClientByIdAsync(int id)
        {
            return await _userRepository.GetClientByIdAsync(id); // Ahora es asincrónico
        }

        public async Task<UserInfoME> CreateAsync(UserInfoME client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client), "El objeto 'client' no puede ser nulo.");
            }

            try
            {
                var createdClient = await _userRepository.CreateAsync(client); // Llamada asincrónica

                if (createdClient == null)
                {
                    throw new Exception($"Error al crear el cliente. Los datos enviados fueron: {client.FullName}, {client.PhoneNumber}, {client.Email}");
                }

                return createdClient;  // Retorna el cliente creado
            }
            catch (Exception ex)
            {
                // Aquí puedes loguear o manejar el error detalladamente
                throw new ApplicationException("Error en la creación del cliente.", ex);
            }
        }



        public async Task<UserInfoME> UpdateClientAsync(UserInfoME client)
        {
            var updatedClient = await _userRepository.UpdateClientAsync(client); // Asincrónico y recibe el objeto completo

            // Si el cliente no fue actualizado, lanzamos una excepción
            if (updatedClient == null)
            {
                throw new Exception("Error al actualizar el cliente.");
            }

            // Devuelve el cliente actualizado
            return updatedClient;
        }

        public async Task<bool> DeleteClientAsync(int id)  // Cambié el tipo de retorno a bool
        {
            var isDeleted = await _userRepository.DeleteClientAsync(id); // Esperamos un bool
            if (!isDeleted)
            {
                throw new Exception("Error al eliminar el cliente.");
            }
            return isDeleted;  // Devuelves true si fue eliminado correctamente, false de lo contrario
        }
    }
}
