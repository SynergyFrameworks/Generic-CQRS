using OZK.Datalayer.Domain;
using OZK.Domain.CQRS.Models;
using System;
using System.Linq.Expressions;


namespace OZK.Domain.CQRS.Projections
{
    public class OZKBankUserProjectionSearchDetails
    {
        public static Expression<Func<BankUser, OZKBankUserSearchResult>> OZKBankUserSearch
        {
            get =>
                o => new OZKBankUserSearchResult
                {
                    Id = o.Id,
                    Firstname = o.Firstname,
                    Lastname = o.Firstname,
                    Address_Address1 = o.Address_Address1,
                    Address_Address2 = o.Address_Address2,
                    Address_City = o.Address_City,
                    Address_State = o.Address_State,
                    Address_Zip = o.Address_Zip,


                };
        }
    }
}
