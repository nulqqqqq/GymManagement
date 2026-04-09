using System;

namespace GymManagement.Api.Dtos.WorkoutSessions;

public class UpdateWorkoutSessionDto
{
    public DateTime Date { get; set; }
    public int DurationInMinutes { get; set; }
    public string Status { get; set; } = string.Empty;
}