namespace GymManagement.Api.Models;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; } = String.Empty;
    public string Role { get; set; } = "User";
}