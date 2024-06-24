using WeNerds.Models.Enumerators;

namespace WeNerds.Models.Types;

public readonly record struct WeSortingInfo(string Field = "created_at", SortDirectionEnum Direction = SortDirectionEnum.Descending)
{
    public bool IsDescending()
        => Direction == SortDirectionEnum.Descending;
    public bool IsAscending()
        => Direction == SortDirectionEnum.Ascending;

}
