namespace Ark.SharedLib.Common.Paging;

public class PagedData<T>
{
    public PagedData(IEnumerable<T> items, PagedInfo pagedInfo)
    {
        Items = items;
        PagedInfo = pagedInfo;
    }

    public IEnumerable<T> Items { get; }
    public PagedInfo PagedInfo { get; }
}