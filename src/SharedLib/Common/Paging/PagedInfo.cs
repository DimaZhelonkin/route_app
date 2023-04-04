namespace Ark.SharedLib.Common.Paging;

public class PagedInfo
{
    public PagedInfo(long pageNumber, long pageSize, long totalRecords)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
    }

    public long PageNumber { get; private set; }
    public long PageSize { get; private set; }
    public long TotalPages => (long)Math.Ceiling((double)TotalRecords / PageSize);
    public long TotalRecords { get; private set; }

    public PagedInfo SetPageNumber(long pageNumber)
    {
        PageNumber = pageNumber;
        return this;
    }

    public PagedInfo SetPageSize(long pageSize)
    {
        PageSize = pageSize;
        return this;
    }

    public PagedInfo SetTotalRecords(long totalRecords)
    {
        TotalRecords = totalRecords;

        return this;
    }
}