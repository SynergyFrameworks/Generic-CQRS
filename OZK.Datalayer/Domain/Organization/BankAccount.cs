
using OZK.Datalayer.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OZK.Datalayer.Domain
{
    [Table("BankAccount")]
    public class BankAccount : Auditable, IEntity
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(BankUser))]
        public Guid BankUserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string AccountType { get; set; }
        [Required]
        public Decimal Amount { get; set; }
        [Required]
        public bool Enbled { get; set; }


    }
}
