using OZK.Datalayer.Domain;
using System;
using System.Linq.Expressions;

namespace OZK.Domain.CQRS.Projections
{
    public class OZKBankAccountProjection
    {
        public static Expression<Func<BankAccount, BankAccount>> OZKBankAccountInformation
        {
            get =>
                o => new BankAccount
                {
                    Id = o.Id,
                    BankUserID = o.BankUserID,  
                    AccountType = o.AccountType, 
                    Amount = o.Amount, 
                    Enbled = o.Enbled,
                    CreatedBy = o.CreatedBy,
                    CreatedDate = o.CreatedDate,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedDate = o.ModifiedDate,

                };
        }
    }
}
