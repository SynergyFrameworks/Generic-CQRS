using OZK.Datalayer.Contracts;
using OZK.Datalayer.Domain;
using System;
using System.Collections.Generic;

namespace OZK.Domain.CQRS.Models
{
    public class OZKBankAccountSearchResult : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        public Guid BankUserID { get; set; }

        public string AccountType { get; set; }

        public decimal  Amount { get; set; }

        public bool Enbled { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
        
        public ModelState State { get; set; } = ModelState.None;
    }
}
