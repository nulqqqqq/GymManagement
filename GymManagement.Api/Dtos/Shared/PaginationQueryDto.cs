namespace GymManagement.Api.Dtos.Shared;

public class PaginationQueryDto
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
    public string? Status { get; set; }
}