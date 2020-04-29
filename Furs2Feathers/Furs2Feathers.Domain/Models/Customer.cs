using System;
using System.Collections.Generic;
using System.Text;

namespace Furs2Feathers.Domain.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public int? Policies { get; set; }
        public int? Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public Address AddressNavigation { get; set; }
        public Policies PoliciesNavigation { get; set; }
        public List<Invoice> Invoice { get; set; }
        public List<PlanReviews> PlanReviews { get; set; }
    }
}
