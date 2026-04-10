using GymManagement.Api.Dtos.Shared;

namespace GymManagement.Api.Dtos.Trainers;

public class TrainerQueryDto:PaginationQueryDto
{
    public string? Specialization { get; set; }
}