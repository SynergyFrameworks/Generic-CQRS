using OZK.Datalayer.Contracts;
using OZK.Datalayer.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OZKService.Models
{
    public class OZKBankUserInfo: IEntity
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address_Address1 { get; set; }

        [MaxLength(200)]
        public string Address_Address2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address_City { get; set; }

        [Required]
        [MaxLength(80)]
        public string Address_State { get; set; }

        [Required]
        [MaxLength(10)]
        public string Address_Zip { get; set; }

        public ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();



    }
}