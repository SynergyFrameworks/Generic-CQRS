using LinqKit;
using OZKService.Models;
using OZK.Datalayer.Context;
using OZK.Datalayer.Domain;
using OZK.Domain.CQRS.Contracts;
using OZK.Domain.CQRS.Models;
using OZK.Domain.CQRS.Projections;
using OZK.Infrastructure.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OZK.Services
{
    public class BankAccountService : CrudServiceBase<BankAccount>, IService<BankAccount, DefaultSearch<OZKBankAccountSearchResult>>
    {
        public BankAccountService(ICrudHandler<OZKDbContext> handler, IQueryHandler<OZKDbContext> queryHandler) : base(handler, queryHandler)
        {
        }

        public async Task<DefaultSearch<OZKBankAccountSearchResult>> Query(DefaultSearch<OZKBankAccountSearchResult> search, CancellationToken cancellationToken = default)
        {
            var whereClauseBuilder = PredicateBuilder.New<BankAccount>(x => true);

            if (search.NameSearch != null)
            {
                whereClauseBuilder.And(account => account.AccountType.Contains(search.NameSearch));
            }

            //if (search.DescriptionSearch != null)
            //{
            //    whereClauseBuilder.And(account => account.AccountType.Contains(search.DescriptionSearch));
            //}

            if (search.CreatedDateSearchRange != null)
            {
                whereClauseBuilder.And(row => row.CreatedDate.Date >= search.CreatedDateSearchRange.FromDate.Date);
                whereClauseBuilder.And(row => row.CreatedDate.Date <= search.CreatedDateSearchRange.ToDate.Date);
            }

            var results = await _queryHandler.SelectSortHandler(OZKBankAccountSearchDetails.OZKBankAccountSearch, whereClauseBuilder, search.SortOptions?.OfType<ISortingOption>().ToList(), search, cancellationToken);

            search.TotalOfRecords = await _queryHandler.CountHandler<BankAccount>(whereClauseBuilder, cancellationToken);

            search.Results = results.ToList();

            return search;
        }

        public async Task<BankAccount> Query(Expression<Func<BankAccount, BankAccount>> projection, Expression<Func<BankAccount, bool>> whereClause, CancellationToken cancellationToken = default)
        {
            return await _queryHandler.SelectHandler(projection, whereClause, cancellationToken);
        }
    }
}
