using GymManagement.Api.Dtos.Trainers;
using GymManagement.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainersController : ControllerBase
{
    public readonly ITrainerService _trainerService;

    public TrainersController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }
    
    /// <summary>
    /// Returns a list of all registered gym trainers.
    /// </summary>
    /// <returns>A collection of trainer response objects.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TrainerResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTrainers()
    {
        var trainers = await _trainerService.GetAllTrainersAsync();
        return Ok(trainers);
    }
    
    /// <summary>
    /// Creates a new trainer profile in the system.
    /// </summary>
    /// <param name="trainerDto">The trainer data (name, specialization).</param>
    /// <response code="201">Returns the newly created trainer profile.</response>
    /// <response code="400">If the provided data is invalid.</response>
    [HttpPost]
    [ProducesResponseType(typeof(TrainerResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTrainer([FromBody]CreateTrainerDto trainerDto)
    {
        var result = await _trainerService.CreateTrainerAsync(trainerDto);
        return Ok(new { Message = $"Trainer created successfully.", Data = result });
    }

    /// <summary>
    /// Updates an existing trainer's information.
    /// </summary>
    /// <param name="id">The unique identifier (GUID) of the trainer.</param>
    /// <param name="trainerDto">The updated data for the trainer.</param>
    /// <returns>No content if successful, or Not Found if the ID doesn't exist.</returns>
    /// <response code="204">Trainer successfully updated.</response>
    /// <response code="404">Trainer with the given ID was not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTrainer(Guid id, UpdateTrainerDto trainerDto)
    {
        var result = await _trainerService.UpdateTrainerAsync(id, trainerDto);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    /// <summary>
    /// Deletes a trainer from the system.
    /// </summary>
    /// <param name="id">The unique identifier (GUID) of the trainer to delete.</param>
    /// <response code="204">Trainer successfully deleted.</response>
    /// <response code="404">Trainer not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTrainer(Guid id)
    {
        var result = await _trainerService.DeleteTrainerAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}