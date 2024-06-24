using WeNerds.Models.Types;

namespace WeNerds.Models.Requests;

public class WeGetPagedRequest(string Filter, int Page = 1, int PageSize = 25, ICollection<WeSortingInfo> OrderBy = default)
{
    public bool HasFilter() 
        => !string.IsNullOrEmpty(Filter);

    public bool HasOrderBy()
        => OrderBy is not null && OrderBy.Count > 0;

    public WePageInfo PageInfo()
        => new (Page, PageSize);
}
