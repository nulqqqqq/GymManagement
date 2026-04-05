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

    public async Task<IEnumerable<ClientResponseDto>> GetAllClientsAsync()
    {
        var clients = await _context.Clients
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

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateClientAsync(Guid id, UpdateClientDto updateDto)
    {
        if (updateDto.TrainerId != null && !await _context.Trainers.AnyAsync(t => t.Id == updateDto.TrainerId)) return false;
        var client = await _context.Clients.FindAsync(id);
        
        if (client == null) return false;
        _mapper.Map(updateDto, client);
        
        await _context.SaveChangesAsync();
        
        return true;
    }
}