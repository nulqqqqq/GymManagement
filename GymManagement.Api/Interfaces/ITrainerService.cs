using GymManagement.Api.Dtos.Trainers;

namespace GymManagement.Api.Interfaces;

public interface ITrainerService
{
   Task<IEnumerable<TrainerResponseDto>> GetAllTrainersAsync();
   Task<TrainerResponseDto> CreateTrainerAsync(CreateTrainerDto trainerDto);
   Task<bool> UpdateTrainerAsync(Guid id, UpdateTrainerDto trainerDto);
   Task<bool> DeleteTrainerAsync(Guid id);
}