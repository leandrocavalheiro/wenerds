namespace WeNerds.Models.Types;

public record struct WePageInfo(int CurrentPage = 1, int PageSize = 25)
{
    public int Skip => CalcSkip(CurrentPage);
    public int Take => PageSize;

    private int CalcSkip(int page)
    {
        page--;
        if (page <= 0)
            page = 1;
        return page * PageSize;
    }
}
