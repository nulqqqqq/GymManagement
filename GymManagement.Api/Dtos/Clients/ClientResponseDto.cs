using GymManagement.Api.Dtos.Trainers;

namespace GymManagement.Api.Dtos;

public class ClientResponseDto
{
    public Guid Id { get; set; }
    public string FistName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Plan { get; set; } = string.Empty;
    public TrainerResponseDto? Trainer { get; set; }
}