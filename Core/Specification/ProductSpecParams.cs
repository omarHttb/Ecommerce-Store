
namespace Core.Specification;

public class ProductSpecParams
{
    private const int maxPageSize = 50;

    public int PageIndex { get; set; } = 1;

    private int _pageSize = 6;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }
    


private List<string> _Brands = [];
public List<string> Brands
{
    get =>_Brands;//types = boards,gloves,etc 
    set { _Brands = value.SelectMany(x => x.Split(',',StringSplitOptions.RemoveEmptyEntries)).ToList(); }
}
//
private List<String> _Types = [];
public List<String> types
    {
    get =>_Types;//types = boards,gloves,etc 
    set { _Types = value.SelectMany(x => x.Split(',',StringSplitOptions.RemoveEmptyEntries)).ToList(); }
    }
    public string? Sort { get; set; }


    private string? _search= "";
    public string Search
    {
        get =>  _search ?? "";
        set => _search = value.ToLower(); 
    }
    
}
