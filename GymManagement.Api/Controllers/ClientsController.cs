using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GymManagement.Api.Dtos;
using GymManagement.Api.Dtos.Shared;
using GymManagement.Api.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }
    
    /// <summary>
    /// Returns a complete list of all registered gym clients.
    /// </summary>
    /// <returns>A list of clients with their IDs, names, and current plans.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClientResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClients([FromQuery]ClientQueryDto query)
    {
        var clients = await _clientService.GetAllClientsAsync(query);
        return Ok(clients);
    }
    
    /// <summary>
    /// Registers a new client in the gym management system.
    /// </summary>
    /// <param name="clientDto">The data required to create a new client profile.</param>
    /// <response code="201">Returns the newly created client with its assigned ID.</response>
    /// <response code="400">If the provided data is invalid (e.g., empty name).</response>
    [HttpPost] 
    [ProducesResponseType(typeof(ClientResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateClient([FromBody] CreateClientDto clientDto)
    {
        var result = await _clientService.CreateClientAsync(clientDto);
        return CreatedAtAction(
            nameof(GetClient),
            new { id = result.Id },
            new { message = "Client created succussfully.", Data = result });
    }
    
    /// <summary>
    /// Retrieves details for a specific client by their unique ID.
    /// </summary>
    /// <param name="id">The unique integer identifier of the client.</param>
    /// <response code="200">Returns the requested client data.</response>
    /// <response code="404">If a client with the specified ID was not found in the database.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ClientResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClient(Guid id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        if (client == null)
        {
            return NotFound(new { Message = $"Client with ID {id} not found." });
        }
        
        return Ok(client);
    }
    
    /// <summary>
    /// Permanently removes a client from the system.
    /// </summary>
    /// <param name="id">The ID of the client to be deleted.</param>
    /// <response code="204">The client was successfully deleted. No content is returned.</response>
    /// <response code="404">If the client does not exist.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteClient(Guid id)
    {
        var deleted = await _clientService.DeleteClientAsync(id);
        if (!deleted)
        {
            return NotFound(new{Message = $"Client with ID{id} not fount."});
        }
        
        return NoContent();
    }

    /// <summary>
    /// Updates an existing client's information.
    /// </summary>
    /// <response code="200">Returns the updated client.</response>
    /// <response code="404">If the client was not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateClient(Guid id, [FromBody] UpdateClientDto updateDto)
    {
        var result = await _clientService.UpdateClientAsync(id, updateDto);
        if (!result)
        {
            return NotFound($"Client with ID{id} not found.");
        }

        return Ok(new { Message = "Client updated successfully", result });
    }
}