using AutoMapper;
using GymManagement.Api.Dtos;
using GymManagement.Api.Dtos.Trainers;
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
    }
}