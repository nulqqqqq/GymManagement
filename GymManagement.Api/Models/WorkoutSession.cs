using System;

namespace GymManagement.Api.Models;

public class WorkoutSession
{
    public Guid Id { get; set; }
    public int DurationInMinutes { get; set; } = 60;
    public string Status { get; set; } = "Scheduled";
    public DateTime Date { get; set; }
    
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    
    public Guid TrainerId { get; set; }
    public Trainer? Trainer { get; set; }
}