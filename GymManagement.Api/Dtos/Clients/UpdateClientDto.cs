using System;

namespace GymManagement.Api.Dtos;

public class UpdateClientDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Plan { get; set; } = string.Empty;
    public Guid? TrainerId { get; set; }
}