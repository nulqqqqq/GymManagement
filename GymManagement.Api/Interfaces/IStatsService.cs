using GymManagement.Api.Dtos.Dashboard;

namespace GymManagement.Api.Interfaces;

public interface IStatsService
{
    Task<DashboardStatsDto> GetDashboardStatsAsync();
}