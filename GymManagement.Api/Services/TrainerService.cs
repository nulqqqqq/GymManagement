using AutoMapper;
using GymManagement.Api.Data;
using GymManagement.Api.Dtos.Shared;
using GymManagement.Api.Dtos.Trainers;
using GymManagement.Api.Interfaces;
using GymManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Api.Services;

public class TrainerService : ITrainerService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public TrainerService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TrainerResponseDto>> GetAllTrainersAsync(PaginationQueryDto queryDto)
    {
        var query = _context.Trainers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(queryDto.SortColumn))
        {
            if (queryDto.SortColumn.Equals("lastName", StringComparison.OrdinalIgnoreCase))
            {
                query = queryDto.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(t => t.LastName)
                    : query.OrderBy(t => t.LastName);
            }
            else if (queryDto.SortColumn.Equals("firstName", StringComparison.OrdinalIgnoreCase))
            {
                query = queryDto.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(t => t.FirstName)
                    : query.OrderBy(t => t.FirstName);
            }
        }

        var skipAmount = (queryDto.PageNumber - 1) * queryDto.PageSize;
        var trainers = await query
            .Skip(skipAmount)
            .Take(queryDto.PageSize)
            .ToListAsync();
        return _mapper.Map<IEnumerable<TrainerResponseDto>>(trainers);
    }

    public async Task<TrainerResponseDto> CreateTrainerAsync(CreateTrainerDto trainerDto)
    {
        var trainer = _mapper.Map<Trainer>(trainerDto);
        _context.Trainers.Add(trainer);
        await _context.SaveChangesAsync();
        return _mapper.Map<TrainerResponseDto>(trainer);
    }

    public async Task<bool> UpdateTrainerAsync(Guid id, UpdateTrainerDto trainerDto)
    {
        var trainer = await _context.Trainers.FindAsync(id);
        if (trainer == null) return false;
        _mapper.Map(trainerDto, trainer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTrainerAsync(Guid id)
    {
        var trainer = await _context.Trainers.FindAsync(id);
        if (trainer == null) return false;
        trainer.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}