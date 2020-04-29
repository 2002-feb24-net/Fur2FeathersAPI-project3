using System;
using System.Collections.Generic;

namespace Furs2Feathers.DataAccess.Models
{
    public partial class Address
    {
        public Address()
        {
            Customer = new HashSet<Customer>();
        }

        public int AddressId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
    }
}
