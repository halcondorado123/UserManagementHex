using ApiMessage.Models;
using ApiMessage.Data;
using ApiResponse.Domain.Interface;

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
            return await _userRepository.GetClientsAsync();
        }

        public async Task<UserInfoME> GetClientByIdAsync(int id)
        {
            return await _userRepository.GetClientByIdAsync(id);
        }

        public async Task<UserInfoME> CreateAsync(UserInfoME client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client), "El objeto 'client' no puede ser nulo.");
            }

            try
            {
                var createdClient = await _userRepository.CreateAsync(client);

                if (createdClient == null)
                {
                    throw new Exception($"Error al crear el cliente. Los datos enviados fueron: {client.FullName}, {client.PhoneNumber}, {client.Email}");
                }

                return createdClient;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en la creación del cliente.", ex);
            }
        }

        public async Task<UserInfoME> UpdateClientAsync(UserInfoME client)
        {
            var updatedClient = await _userRepository.UpdateClientAsync(client); 

            if (updatedClient == null)
            {
                throw new Exception("Error al actualizar el cliente.");
            }

            return updatedClient;
        }

        public async Task<bool> DeleteClientAsync(int id) 
        {
            var isDeleted = await _userRepository.DeleteClientAsync(id); 
            if (!isDeleted)
            {
                throw new Exception("Error al eliminar el cliente.");
            }
            return isDeleted;
        }
    }
}
