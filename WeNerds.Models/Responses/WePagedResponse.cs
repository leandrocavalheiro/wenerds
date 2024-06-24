using System.Text.Json;

namespace WeNerds.Models.Responses;

public class WePagedResponse<TTYpe>(bool success = false, ICollection<TTYpe> data = default, int page = 1, int pageSize = 25, int totalRecords = 0, int totalPages = 1)
{
    public ICollection<TTYpe> Data { get; set; } = data;
    public bool Success { get; set; } = success;
    public int Page { get; } = page;

    public int PageSize { get; } = pageSize;

    public int TotalRecords { get; set; } = totalRecords;

    public int TotalPages { get; set; } = totalPages;

    public bool IsSuccess()
        => Success;
    public override string ToString()
        => JsonSerializer.Serialize(this);

}
