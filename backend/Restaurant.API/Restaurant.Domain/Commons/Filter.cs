namespace Restaurant.Domain.Commons;

public class Filter
{
    public string Filters { get; set; }

    public int First { get; set; }

    public int Rows { get; set; }

    public string SortField { get; set; }

    public int SortOrder { get; set; }
}
