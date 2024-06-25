using WeNerds.Models.Types;

namespace WeNerds.Models.Requests;

public class WeGetPagedRequest(string filter, int page = 1, int pageSize = 25, ICollection<WeSortingInfo> orderBy = default)
{
    public string Filter { get; set; } = filter;
    public int Page { get; set;} = page;
    public int PageSize { get; set;} = pageSize;
    public ICollection<WeSortingInfo> OrderBy {get; set;} = orderBy;

    public bool HasFilter() 
        => !string.IsNullOrEmpty(Filter);

    public bool HasOrderBy()
        => OrderBy is not null && OrderBy.Count > 0;

    public WePageInfo PageInfo()
        => new (Page, PageSize);
}
