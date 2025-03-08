namespace Phonebook.Application.DTOs.Common;

public class ContactQueryParams
{
    private int _page = 1;
    public int Page
    {
        get => _page;
        set => _page = (value > 0) ? value : _page;
    }


    private const int MaxPageSize = 50;
    private int _pageSize = 5;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (MaxPageSize >= value && value > 0) ? value : MaxPageSize;
    }

    public string? Search { get; set; }
}
