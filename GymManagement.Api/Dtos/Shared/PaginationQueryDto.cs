namespace GymManagement.Api.Dtos.Shared;

public class PaginationQueryDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    
}