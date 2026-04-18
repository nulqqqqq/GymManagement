namespace GymManagement.Api.Dtos.Dashboard;

public class DashboardStatsDto
{
    public int TotalActiveClients { get; set; }
    public int TotalDeletedClients { get; set; }
    public int TotalTrainers { get; set; }
    
    public string? TopTrainerName { get; set; }
    public int TopTrainerClientCount { get; set; }
}