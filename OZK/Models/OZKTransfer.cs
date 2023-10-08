using OZK.Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace OZK.Models
{
    public class OZKTransfer : IEntity
    {

        public Guid Id { get; set; }
        [Required]
        public Guid BankUserId { get; set; }
        [Required]
        public Guid SourceAccountId { get; set; }
        [Required]
        public Guid DestinationAccountId { get; set; }
        [Required]
        public decimal TransferAmount { get; set; }


    }
}
