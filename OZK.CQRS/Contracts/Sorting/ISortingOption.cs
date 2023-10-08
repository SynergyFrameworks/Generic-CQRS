namespace OZK.Infrastructure.Sorting
{
    public interface ISortingOption
    {
        string ColumnName { get; set; }
        bool IsColumnOrderDesc { get; set; }
    }
}
