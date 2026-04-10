using GymManagement.Api.Dtos.Shared;

namespace GymManagement.Api.Dtos.WorkoutSessions;

public class WorkoutSessionQueryDto:PaginationQueryDto
{
    public string? Status { get; set; }
}