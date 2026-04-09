using System;

namespace GymManagement.Api.Dtos.WorkoutSessions;

public class WorkoutSessionResponseDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int DurationInMinutes { get; set; }

    public string Status { get; set; } = string.Empty;
    public Guid ClientId { get; set; }
    public Guid TrainerId { get; set; }
}