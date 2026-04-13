using AutoMapper;
using GymManagement.Api.Data;
using GymManagement.Api.Dtos;
using GymManagement.Api.Interfaces;
using GymManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Api.Services;

public class ClientService : IClientService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ClientService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClientResponseDto>> GetAllClientsAsync(ClientQueryDto queryDto)
    {
        var query = _context.Clients.AsQueryable();
        if (!string.IsNullOrWhiteSpace(queryDto.Plan))
        {
            query = query.Where(c => c.Plan == queryDto.Plan);
        }

        if (!string.IsNullOrWhiteSpace(queryDto.SortColumn))
        {
            if (queryDto.SortColumn.Equals("lastName", StringComparison.OrdinalIgnoreCase))
            {
                query = queryDto.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(c => c.LastName)
                    : query.OrderBy(c => c.LastName);
            }

            else if (queryDto.SortColumn.Equals("firstName", StringComparison.OrdinalIgnoreCase))
            {
                query = queryDto.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(c => c.FirstName)
                    : query.OrderBy(c => c.FirstName);
            }
        }

        var skipAmount = (queryDto.PageNumber - 1) * queryDto.PageSize;
        var clients = await query
            .Skip(skipAmount)
            .Take(queryDto.PageSize)
            .Include(c => c.Trainer)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ClientResponseDto>>(clients);
    }

    public async Task<ClientResponseDto?> GetClientByIdAsync(Guid id)
    {
        var client = await _context.Clients
            .Include(c => c.Trainer)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (client == null) return null;
        return _mapper.Map<ClientResponseDto>(client);
    }

    public async Task<ClientResponseDto> CreateClientAsync(CreateClientDto createDto)
    {
        var client = _mapper.Map<Client>(createDto);

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return _mapper.Map<ClientResponseDto>(client);
    }

    public async Task<bool> DeleteClientAsync(Guid id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null) return false;

        client.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateClientAsync(Guid id, UpdateClientDto updateDto)
    {
        if (updateDto.TrainerId != null && !await _context.Trainers.AnyAsync(t => t.Id == updateDto.TrainerId))
            return false;
        var client = await _context.Clients.FindAsync(id);

        if (client == null) return false;
        _mapper.Map(updateDto, client);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AssignTrainerAsync(Guid clientId, Guid trainerId)
    {
        var client = await _context.Clients.FindAsync(clientId);
        if (client == null) return false;

        var trainer = await _context.Trainers.FindAsync(trainerId);
        if (trainer == null) return false;

        client.TrainerId = trainerId;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task UpdateClientPhotoAsync(Guid id, string photoUrl, string publicId)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client != null)
        {
            client.PhotoUrl = photoUrl;
            client.PublicId = publicId;
            await _context.SaveChangesAsync();
        }
    }
}