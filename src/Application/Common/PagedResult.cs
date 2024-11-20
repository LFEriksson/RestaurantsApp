using System.Collections;

namespace Application.Common;

public class PagedResult<T> where T : class
{
    public PagedResult(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalItems = totalCount;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        ItemsFrom = pageSize * (pageNumber - 1) + 1;
        ItemsTo = ItemsFrom + pageSize - 1;
    }
    public IEnumerable<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
}
