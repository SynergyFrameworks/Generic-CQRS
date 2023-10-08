using OZK.Infrastructure.Sorting;

namespace OZK.Domain.CQRS.Models
{
    public class SortOption : ISortingOption
    {
        public string ColumnName { get; set; }
        public bool IsColumnOrderDesc { get; set; }
    }
}