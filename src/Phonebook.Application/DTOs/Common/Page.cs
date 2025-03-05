namespace Phonebook.Application.DTOs.Common;

public class Page<TData>
{
    public Page(IEnumerable<TData> data, int currentPage, int pageSize, int totalItems)
    {
        Data = data;
        Metadata = new PageMetadata
        {
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalItems = totalItems,
        };
    }

    public Page(IEnumerable<TData> data, PageMetadata metadata)
    {
        Data = data;
        Metadata = metadata;
    }

    public IEnumerable<TData> Data { get; init; }
    public PageMetadata Metadata { get; set; }
}

public class PageMetadata
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}
