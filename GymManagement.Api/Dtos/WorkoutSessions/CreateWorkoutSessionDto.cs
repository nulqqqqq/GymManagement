using System;

namespace GymManagement.Api.Dtos.WorkoutSessions;

public class CreateWorkoutSessionDto
{
    public DateTime Date { get; set; }
    public int DurationOnMinutes { get; set; } = 60;
    public Guid ClientId { get; set; }
    public Guid TrainerId { get; set; }
}