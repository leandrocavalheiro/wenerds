using WeNerds.Models.Interfaces;
using WeNerds.Models.Types;

namespace WeNerds.Models.BusModels;

public class PaginatedQuery<T> : IQuery<T>
{
    private string _filter;
    public string Filter { get => _filter; set => _filter = (value ?? string.Empty).Trim(); }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public ICollection<WeSort> SortBy { get; set; }    
    public PaginatedQuery(string filter, int page, int pageSize, ICollection<WeSort> sortBy = null)
    {

        Filter = filter;
        Page = page;
        PageSize = pageSize;
        sortBy ??= new List<WeSort>() { new() };
        SortBy = sortBy;
    }       
}
