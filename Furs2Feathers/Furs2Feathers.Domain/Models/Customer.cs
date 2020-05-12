using System;
using System.Collections.Generic;
using System.Text;

namespace Furs2Feathers.Domain.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public int? Policies { get; set; }
        public string Name { get; set; }
        public string street { get; set; }
        public string addr2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address AddressNavigation { get; set; }
        public Policies PoliciesNavigation { get; set; }
        public List<Invoice> Invoice { get; set; }
        public List<PlanReviews> PlanReviews { get; set; }
    }
}
