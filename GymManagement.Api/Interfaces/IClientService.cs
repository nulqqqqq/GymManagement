using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GymManagement.Api.Dtos;
using GymManagement.Api.Dtos.Shared;

namespace GymManagement.Api.Interfaces;

public interface IClientService
{
    Task<IEnumerable<ClientResponseDto>> GetAllClientsAsync(PaginationQueryDto query);
    Task<ClientResponseDto?> GetClientByIdAsync(Guid id);
    Task<ClientResponseDto> CreateClientAsync(CreateClientDto createClientDto);
    Task<bool> DeleteClientAsync(Guid id);
    Task<bool> UpdateClientAsync(Guid id, UpdateClientDto updateDto);
}