using GymManagement.Api.Dtos.Shared;

namespace GymManagement.Api.Dtos;

public class ClientQueryDto:PaginationQueryDto
{
    public string? Plan { get; set; }
}