namespace GymManagement.Api.Models;

public class Trainer
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; } = string.Empty;

    public bool IsDeleted { get; set; } = false;

    //link to clients for relationship: one to many
    public ICollection<Client> Clients { get; set; } = new List<Client>();
    public ICollection<WorkoutSession> WorkoutSessions { get; set; } = new List<WorkoutSession>();
}