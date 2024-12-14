using ApiMessage.Models;
using ApiMessage.Transversal.Common;
using ApiResponse.Domain.Interface;
using APIUserValidation.Helpers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMessage.Api.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class UserController : ControllerBase
    {
        private readonly IUserDomain _userDomain;

        // Simulamos una base de datos con una lista en memoria
        private static List<UserInfoME> users = new List<UserInfoME>();

        private readonly IAppLogger<UserController> _logger;

        public UserController(IUserDomain userDomain, IAppLogger<UserController> logger)
        {
            _userDomain = userDomain;
            _logger = logger;
        }

        [HttpGet("GetUsers")]
        [SwaggerOperation(
            Summary = SwaggerComments.Clients.GetUsersHeader,
            Description = SwaggerComments.Clients.GetUsersDescription)]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var users = await _userDomain.GetClientsAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = SwaggerComments.Clients.GetUsersByIdHeader,
            Description = SwaggerComments.Clients.GetUsersByIdDescription)]
        [ProducesResponseType(typeof(UserInfoME), 200)] 
        [ProducesResponseType(404)] 
        [ProducesResponseType(500)] 
        public async Task<IActionResult> GetClientById(int userId)
        {
            try
            {
                var client = await _userDomain.GetClientByIdAsync(userId);
                if (client == null)
                {
                    return NotFound("Cliente no encontrado.");
                }
                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPost("CreateClient")]
        [SwaggerOperation(
                Summary = SwaggerComments.Clients.CreateUserHeader,
                Description = SwaggerComments.Clients.CreateUserDescription)]
        [ProducesResponseType(typeof(UserInfoME), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateClient([FromBody] UserInfoME client)
        {
            if (client == null)
            {
                return BadRequest("Los datos del cliente son inválidos.");
            }

            try
            {
                var createdClient = await _userDomain.CreateAsync(client);
                Console.WriteLine($"Created client with ID: {createdClient.UserId}");
                return CreatedAtAction(nameof(GetClientById), new { id = createdClient.UserId }, createdClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating client: {ex.Message}");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPut("UpdateClient")]
        [SwaggerOperation(
     Summary = SwaggerComments.Clients.UpdateUserHeader,
     Description = SwaggerComments.Clients.UpdateUserDescription)]
        [ProducesResponseType(typeof(UserInfoME), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateClient([FromBody] UserInfoME client)
        {
            // Validación de que el cliente no sea nulo
            if (client == null)
            {
                return BadRequest("Los datos del cliente son inválidos (el cliente está vacío).");
            }

            // Validación de que el UserId del cliente es válido (mayor a 0)
            if (client.UserId <= 0)
            {
                return BadRequest("El ID del cliente no es válido.");
            }

            try
            {
                // Llamar al dominio para actualizar el cliente
                var updatedClient = await _userDomain.UpdateClientAsync(client);
                if (updatedClient == null)
                {
                    return NotFound("Cliente no encontrado para actualizar.");
                }

                // Retornar el cliente actualizado
                return Ok(updatedClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }



        [HttpDelete("DeleteClient")]
        [SwaggerOperation(
            Summary = SwaggerComments.Clients.DeleteUserHeader,
            Description = SwaggerComments.Clients.DeleteUserDescription)]
        [ProducesResponseType(typeof(UserInfoME), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteClient(int userId)
        {
            try
            {
                var deletedClient = await _userDomain.DeleteClientAsync(userId);
                if (deletedClient == null)
                {
                    return NotFound("Cliente no encontrado para eliminar.");
                }
                return Ok(deletedClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPost("CreateMessage")]
        [SwaggerOperation(
            Summary = SwaggerComments.Clients.ReceiveUserDataHeader,
            Description = SwaggerComments.Clients.ReceiveUserDataDescription)]
        [ProducesResponseType(typeof(UserInfoME), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult CreateMessage([FromBody] UserInfoME userRequest)
        {
            if (userRequest == null || string.IsNullOrEmpty(userRequest.FullName) || string.IsNullOrEmpty(userRequest.PhoneNumber))
            {
                _logger.LogWarning("Intento de creación de usuario fallido: Datos inválidos para el usuario con teléfono {PhoneNumber}", userRequest?.PhoneNumber);
                return BadRequest("Datos del usuario inválidos.");
            }

            // Guardamos los datos en la "base de datos" simulada
            users.Add(userRequest);

            // Simulamos el envío de un mensaje - se registra en los logs del sistema, y en traza temporal en un archivo txt
            _logger.LogInformation("Mensaje enviado a {FullName} con el número {PhoneNumber}: Bienvenido/a!", userRequest.FullName, userRequest.PhoneNumber);

            // Retornamos una respuesta exitosa con los datos recibidos
            return Ok(new { Message = "Usuario creado con éxito", User = userRequest });
        }
    }
}
