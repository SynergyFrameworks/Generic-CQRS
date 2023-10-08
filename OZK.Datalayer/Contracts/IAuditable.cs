using OZK.Datalayer.Domain;
using System;

namespace OZK.Datalayer.Contracts
{
    public interface IAuditable
    {
        public DateTimeOffset CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public ModelState State { get; set; }
    }
}
