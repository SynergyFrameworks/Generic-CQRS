﻿using OZK.Datalayer.Contracts;
using OZK.Datalayer.Domain;
using System;
using System.Collections.Generic;

namespace OZK.Domain.CQRS.Models
{
    public class OZKBankUserSearchResult : IEntity, IAuditable
    {
        public Guid Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Address_Address1 { get; set; }

        public string Address_Address2 { get; set; }

        public string Address_City { get; set; }

        public string Address_State { get; set; }

        public string Address_Zip { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();

        public DateTimeOffset CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public ModelState State { get; set; } = ModelState.None;
    }
}
