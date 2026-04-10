using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GymManagement.Api.Data;
using GymManagement.Api.Dtos.Shared;
using GymManagement.Api.Dtos.WorkoutSessions;
using GymManagement.Api.Interfaces;
using GymManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Api.Services;

public class WorkoutSessionService:IWorkoutSessionService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public WorkoutSessionService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WorkoutSessionResponseDto>> GetAllWorkoutSessionsAsync(WorkoutSessionQueryDto queryDto)
    {
        var query = _context.WorkoutSessions.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(queryDto.Status))
        {
            query = query.Where(w => w.Status == queryDto.Status);
        }
        
        if (!string.IsNullOrWhiteSpace(queryDto.SortColumn))
        {
            if (queryDto.SortColumn.Equals("date", StringComparison.OrdinalIgnoreCase))
            {
                query = queryDto.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(w => w.Date)
                    : query.OrderBy(w => w.Date);
            }
            else if (queryDto.SortColumn.Equals("duration", StringComparison.OrdinalIgnoreCase))
            {
                query = queryDto.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(w => w.DurationInMinutes)
                    : query.OrderBy(w => w.DurationInMinutes);
            }
        }
        var skipAmount = (queryDto.PageNumber - 1) * queryDto.PageSize;
        
        var workoutSessions = await query
            .Include(w=> w.Client)
            .Include(w => w.Trainer)
            .Skip(skipAmount)
            .Take(queryDto.PageSize)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<WorkoutSessionResponseDto>>(workoutSessions);
    }

    public async Task<WorkoutSessionResponseDto?> GetWorkoutSessionByIdAsync(Guid id)
    {
        var workoutSession = await _context.WorkoutSessions
            .Include(w => w.Client)
            .Include(w => w.Trainer)
            .FirstOrDefaultAsync(i => i.Id == id);
        if (workoutSession == null) return null;
        
        return _mapper.Map<WorkoutSessionResponseDto>(workoutSession);
    }

    public async Task<WorkoutSessionResponseDto> CreateWorkoutSessionAsync(CreateWorkoutSessionDto workoutSessionDto)
    {
        var clientExist = await _context.Clients.AnyAsync(c => c.Id == workoutSessionDto.ClientId);
        var trainerExist = await _context.Trainers.AnyAsync(t => t.Id == workoutSessionDto.TrainerId);
        
        if (!trainerExist||!clientExist)
        {
            throw new ArgumentException("Client or trainer does not exist.");
        }
        var workoutSession = _mapper.Map<WorkoutSession>(workoutSessionDto);
        _context.WorkoutSessions.Add(workoutSession);
        await _context.SaveChangesAsync();
        return _mapper.Map<WorkoutSessionResponseDto>(workoutSession);
    }

    public async Task<bool> UpdateWorkoutSessionAsync(Guid id, UpdateWorkoutSessionDto workoutDto)
    {
        var workoutSession = await _context.WorkoutSessions.FindAsync(id);
        if (workoutSession == null)return false;
        _mapper.Map(workoutDto, workoutSession);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteWorkoutSessionAsync(Guid id)
    {
        var workoutSession = await _context.WorkoutSessions.FindAsync(id);
        if (workoutSession == null) return false;
        _context.WorkoutSessions.Remove(workoutSession);
        await _context.SaveChangesAsync();
        return true;
    }
    
}