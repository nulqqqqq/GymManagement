namespace GymManagement.Api.Models;

public class Client
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Plan { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid TrainerId { get; set; }
    public Trainer? Trainer { get; set; }
}