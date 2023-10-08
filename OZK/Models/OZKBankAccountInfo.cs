using OZK.Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace OZKService.Models
{
    public class OZKBankAccountInfo : IEntity
    {

        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid BankUserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string AccountType { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public bool Enbled { get; set; }
    }
}
