using AutoMapper;
using GymManagement.Api.Dtos;
using GymManagement.Api.Dtos.Trainers;
using GymManagement.Api.Dtos.WorkoutSessions;
using GymManagement.Api.Models;

namespace GymManagement.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //из DTO в энтити
        CreateMap<CreateClientDto, Client>();
        //Из энтити в DTO
        CreateMap<Client, ClientResponseDto>();
        CreateMap<UpdateClientDto, Client>();
        CreateMap<CreateTrainerDto, Trainer>();
        CreateMap<Trainer, TrainerResponseDto>();
        // --- Workout Sessions ---
        // Из базы -> пользователю
        CreateMap<WorkoutSession, WorkoutSessionResponseDto>();

        // От пользователя -> в базу (Создание)
        CreateMap<CreateWorkoutSessionDto, WorkoutSession>();

        // От пользователя -> в базу (Обновление)
        CreateMap<UpdateWorkoutSessionDto, WorkoutSession>();
    }
}