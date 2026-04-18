using GymManagement.Api.Data;
using GymManagement.Api.Dtos.Dashboard;
using GymManagement.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Api.Services;

public class StatsService : IStatsService
{
    private readonly AppDbContext _context;

    public StatsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardStatsDto> GetDashboardStatsAsync()
    {
        var activeClients = await _context.Clients.CountAsync();

        var deletedClients = await _context.Clients
            .IgnoreQueryFilters()
            .Where(c => c.IsDeleted)
            .CountAsync();
        var totalTrainers = await _context.Trainers.CountAsync();
        var topTrainerData = await _context.Trainers
            .Include(t => t.Clients)
            .OrderByDescending(t => t.Clients.Count)
            .Select(t => new
            {
                FullName = t.FirstName + " " + t.LastName,
                ClientCount = t.Clients.Count
            })
            .FirstOrDefaultAsync();
        return new DashboardStatsDto
        {
            TotalActiveClients = activeClients,
            TotalDeletedClients = deletedClients,
            TotalTrainers = totalTrainers,
            TopTrainerName = topTrainerData?.FullName ?? "No data",
            TopTrainerClientCount = topTrainerData?.ClientCount ?? 0
        };
    }
}