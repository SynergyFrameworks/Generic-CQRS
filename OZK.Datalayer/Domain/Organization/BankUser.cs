
using OZK.Datalayer.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OZK.Datalayer.Domain
{
    [Table("BankUser")]
    public class BankUser : Auditable, IEntity
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address_Address1 { get; set; }


        [MaxLength(100)]
        public string Address_Address2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address_City { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address_State { get; set; }


        [Required]
        [MaxLength(50)]
        public string Address_Zip { get; set; }

     
        public ICollection<BankAccount> BankAccounts { get; set; } = new HashSet<BankAccount>();


    }
}
