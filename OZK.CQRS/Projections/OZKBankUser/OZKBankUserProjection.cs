using OZK.Datalayer.Domain;
using System;
using System.Linq.Expressions;


namespace OZK.Domain.CQRS.Projections
{
    public static class OZKBankUserProjection
    {
        public static Expression<Func<BankUser, BankUser>> OZKBankAccountInformation
        {
            get =>
                user => new BankUser
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Address_Address1 = user.Address_Address1,
                    Address_Address2 = user.Address_Address2,
                    Address_City = user.Address_City,
                    Address_State = user.Address_State,
                    Address_Zip = user.Address_Zip,
                    CreatedBy = user.CreatedBy,
                    CreatedDate = user.CreatedDate,
                    ModifiedBy = user.ModifiedBy,
                    ModifiedDate = user.ModifiedDate,


                };
        }

    }
}
