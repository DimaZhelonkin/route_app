using Microsoft.EntityFrameworkCore;

namespace Ark.SharedLib.Common.Paging;

public class PagedList<T> : List<T>
{
    public PagedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        PageNumber = pageNumber;
        CurrentPageSize = items.Count;
        CurrentStartIndex = count == 0 ? 0 : (pageNumber - 1) * pageSize + 1;
        CurrentEndIndex = count == 0 ? 0 : CurrentStartIndex + CurrentPageSize - 1;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public int PageNumber { get; }
    public int TotalPages { get; }
    public int PageSize { get; }
    public int CurrentPageSize { get; set; }
    public int CurrentStartIndex { get; set; }
    public int CurrentEndIndex { get; set; }
    public int TotalCount { get; }

    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;

    public static PagedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var count = source.Count();
        var items = await source
                          .Skip((pageNumber - 1) * pageSize)
                          .Take(pageSize)
                          .ToListAsync(cancellationToken);
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}