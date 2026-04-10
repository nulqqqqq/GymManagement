using System;

namespace GymManagement.Api.Dtos.Trainers;

public class TrainerResponseDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Specialization { get; set; }
    public string? PhoneNumber { get; set; }
}