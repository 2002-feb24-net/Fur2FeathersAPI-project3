using System;
using System.Collections.Generic;
using System.Text;

namespace Furs2Feathers.Domain.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }

        public List<Customer> Customer { get; set; } = new List<Customer>();
    }
}
