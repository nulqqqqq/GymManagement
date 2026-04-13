using GymManagement.Api.Dtos;

namespace GymManagement.Api.Interfaces;

public interface IClientService
{
    Task<IEnumerable<ClientResponseDto>> GetAllClientsAsync(ClientQueryDto query);
    Task<ClientResponseDto?> GetClientByIdAsync(Guid id);
    Task<ClientResponseDto> CreateClientAsync(CreateClientDto createClientDto);
    Task<bool> DeleteClientAsync(Guid id);
    Task<bool> UpdateClientAsync(Guid id, UpdateClientDto updateDto);
    Task<bool>  AssignTrainerAsync(Guid clientId, Guid trainerId);
    public Task UpdateClientPhotoAsync(Guid id, string photoUrl, string publicId);
}