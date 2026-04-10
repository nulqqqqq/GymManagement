using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GymManagement.Api.Dtos.Shared;
using GymManagement.Api.Dtos.WorkoutSessions;

namespace GymManagement.Api.Interfaces;

public interface IWorkoutSessionService
{
    Task<IEnumerable<WorkoutSessionResponseDto>> GetAllWorkoutSessionsAsync(WorkoutSessionQueryDto query);
    Task<WorkoutSessionResponseDto?> GetWorkoutSessionByIdAsync(Guid id);
    Task<WorkoutSessionResponseDto> CreateWorkoutSessionAsync(CreateWorkoutSessionDto workoutSessiontDto);
    Task<bool> UpdateWorkoutSessionAsync(Guid id, UpdateWorkoutSessionDto workoutDto);
    Task<bool> DeleteWorkoutSessionAsync(Guid id);
}