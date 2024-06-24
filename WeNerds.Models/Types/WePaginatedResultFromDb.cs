namespace WeNerds.Models.Types;

public record struct WePaginatedResultFromDb<TResult>(ICollection<TResult> Records, int TotalRecords, int TotalPages);
