using OZK.Domain.CQRS.Models;
using OZK.Infrastructure.Paging;
using System.Collections.Generic;

namespace OZKService.Contracts
{
    public interface IDefaultSearch<T> : IPaging where T : class
    {
        SearchRange CreatedDateSearchRange { get; set; }
        string DescriptionSearch { get; set; }
        string NameSearch { get; set; }
        IList<T> Results { get; set; }
        IList<SortOption> SortOptions { get; set; }
    }
}