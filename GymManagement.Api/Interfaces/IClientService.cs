using GymManagement.Api.Dtos;

namespace GymManagement.Api.Interfaces;

public interface IClientService
{
    Task<IEnumerable<ClientResponseDto>> GetAllClientsAsync();
    Task<ClientResponseDto?> GetClientByIdAsync(Guid id);
    Task<ClientResponseDto> CreateClientAsync(CreateClientDto createClientDto);
    Task<bool> DeleteClientAsync(Guid id);
    Task<bool> UpdateClientAsync(Guid id, UpdateClientDto updateDto);
}