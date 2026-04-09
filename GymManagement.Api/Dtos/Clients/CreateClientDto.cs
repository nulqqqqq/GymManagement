using System;

namespace GymManagement.Api.Dtos;

public class CreateClientDto
{
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Plan { get; set; } = "Basic";
        public Guid? TrainerId { get; set; }
}