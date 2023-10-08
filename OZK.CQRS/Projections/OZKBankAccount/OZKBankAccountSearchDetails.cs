using OZK.Datalayer.Domain;
using OZK.Domain.CQRS.Models;
using System;
using System.Linq.Expressions;

namespace OZK.Domain.CQRS.Projections
{
    public class OZKBankAccountSearchDetails
    {
        public static Expression<Func<BankAccount, OZKBankAccountSearchResult>> OZKBankAccountSearch
        {
            get =>
                o => new OZKBankAccountSearchResult
                {
                    Id = o.Id,
                    BankUserID = o.BankUserID, 
                    AccountType = o.AccountType,  
                    Amount = o.Amount,
                    State = o.State,
                    Enbled = o.Enbled,  
                  
                };
        }
    }

}
