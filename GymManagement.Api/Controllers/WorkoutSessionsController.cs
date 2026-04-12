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
    
    /// <summary>
    /// Retrieves a list of all workout sessions based on filter criteria.
    /// </summary>
    /// <param name="query">Filter and pagination parameters for workout sessions.</param>
    /// <returns>A collection of workout session data transfer objects.</returns>
    /// <response code="200">Returns the requested list of sessions.</response> 
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<WorkoutSessionResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllWorkoutSessions([FromQuery] WorkoutSessionQueryDto query)
    {
        var workoutSessions = await _workoutSessionService.GetAllWorkoutSessionsAsync(query);
        return Ok(workoutSessions);
    }
    
    /// <summary>
    /// Creates a new workout session.
    /// </summary>
    /// <param name="workoutSessionDto">The data required to create a session.</param>
    /// <returns>The newly created workout session details.</returns>
    /// <response code="201">Session created successfully.</response>
    /// <response code="400">Invalid input data provided.</response>
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
    
    /// <summary>
    /// Gets a specific workout session by its unique identifier.
    /// </summary>
    /// <param name="id">The unique GUID of the workout session.</param>
    /// <returns>The details of the requested session.</returns>
    /// <response code="200">Session found and returned.</response>
    /// <response code="404">Session with the specified ID not found.</response>
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
    /// <summary>
    /// Updates an existing workout session with new information.
    /// </summary>
    /// <param name="id">The unique GUID of the session to update.</param>
    /// <param name="workoutSessionDto">The updated data for the session.</param>
    /// <returns>No content if the update was successful.</returns>
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
    /// <summary>
    /// Removes a workout session from the system.
    /// </summary>
    /// <param name="id">The unique GUID of the session to remove.</param>
    /// <returns>No content if the deletion was successful.</returns>
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