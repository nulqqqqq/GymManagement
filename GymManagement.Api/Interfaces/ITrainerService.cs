using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GymManagement.Api.Dtos.Shared;
using GymManagement.Api.Dtos.Trainers;

namespace GymManagement.Api.Interfaces;

public interface ITrainerService
{
   Task<IEnumerable<TrainerResponseDto>> GetAllTrainersAsync(PaginationQueryDto query);
   Task<TrainerResponseDto> CreateTrainerAsync(CreateTrainerDto trainerDto);
   Task<bool> UpdateTrainerAsync(Guid id, UpdateTrainerDto trainerDto);
   Task<bool> DeleteTrainerAsync(Guid id);
}