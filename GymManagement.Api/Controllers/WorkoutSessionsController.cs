using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GymManagement.Api.Dtos.Shared;
using GymManagement.Api.Dtos.WorkoutSessions;
using GymManagement.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[Route("api/[controller]")]
public class WorkoutSessionsController:ControllerBase
{
    private readonly IWorkoutSessionService _workoutSessionService;

    public WorkoutSessionsController(IWorkoutSessionService workoutSession)
    {
        _workoutSessionService = workoutSession;
    }
    
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WorkoutSessionResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllWorkoutSessions([FromQuery] PaginationQueryDto query)
    {
        var workoutSessions = await _workoutSessionService.GetAllWorkoutSessionsAsync(query);
        return Ok(workoutSessions);
    }

    [HttpPost]
    [ProducesResponseType(typeof(WorkoutSessionResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateWorkoutSession([FromBody]CreateWorkoutSessionDto workoutSessionDto)
    {
        var result = await _workoutSessionService.CreateWorkoutSessionAsync(workoutSessionDto);
        return CreatedAtAction(nameof(GetWorkoutSession),
            new {id = result.Id},
            new {Message = "Workout session created successfully", Data = result});
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(WorkoutSessionResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWorkoutSession(Guid id)
    {
        var workoutSession = await _workoutSessionService.GetWorkoutSessionByIdAsync(id);
        if (workoutSession == null)
        {
            return NotFound(new { Message = $"Workout session with ID {id} not found" });
        }

        return Ok(workoutSession);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateWorkoutSession(Guid id, [FromBody]UpdateWorkoutSessionDto workoutSessionDto)
    {
        var result = await _workoutSessionService.UpdateWorkoutSessionAsync(id, workoutSessionDto);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWorkoutSession(Guid id)
    {
        var result = await _workoutSessionService.DeleteWorkoutSessionAsync(id);
        if (!result) return NotFound();
        
        return NoContent();
    }
}