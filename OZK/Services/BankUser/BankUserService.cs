
using LinqKit;
using OZKService.Models;
using OZK.Datalayer.Context;
using OZK.Datalayer.Domain;
using OZK.Domain.CQRS.Contracts;
using OZK.Domain.CQRS.Models;
using OZK.Domain.CQRS.Projections;
using OZK.Infrastructure.Sorting;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OZK.Services
{
    public class BankUserService : CrudServiceBase<BankUser>,  IService<BankUser, DefaultSearch<OZKBankUserSearchResult>>
    {
        public BankUserService(ICrudHandler<OZKDbContext> handler, IQueryHandler<OZKDbContext> queryHandler): base(handler, queryHandler)
        {
        }

        public async Task<BankUser> Query(Expression<Func<BankUser, BankUser>> projection, Expression<Func<BankUser, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }
       
        public async Task<DefaultSearch<OZKBankUserSearchResult>> Query(DefaultSearch<OZKBankUserSearchResult> search, CancellationToken cancellationToken = default)
        {
            var whereClauseBuilder = PredicateBuilder.New<BankUser>(x => true);

            if (search.NameSearch != null)
            {
                whereClauseBuilder.And(org => org.Firstname.Contains(search.NameSearch));
            }

            if (search.DescriptionSearch != null)
            {
                whereClauseBuilder.And(org => org.Firstname.Contains(search.DescriptionSearch));
            }

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            var results = await _queryHandler.SelectSortHandler(OZKBankUserProjectionSearchDetails.OZKBankUserSearch, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<BankUser>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }
    }
}
