using System.Text.Json;

namespace WeNerds.Models.ViewModels;

public record WePaginatedViewModel<TResult>
{
    public ICollection<TResult> Items { get; set; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }

    public WePaginatedViewModel(ICollection<TResult> items, int page, int pageSize, int totalRecords, int totalPages)
    {
        Items = items ?? new List<TResult>();
        Page = page;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = totalPages;
    }
    public override string ToString()
        => JsonSerializer.Serialize(this);
}
