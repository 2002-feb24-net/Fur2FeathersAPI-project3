using System;
using System.Collections.Generic;

namespace Furs2FeathersAPI.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Invoice = new HashSet<Invoice>();
            PlanReviews = new HashSet<PlanReviews>();
        }

        public int CustomerId { get; set; }
        public int? Policies { get; set; }
        public int? Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public virtual Address AddressNavigation { get; set; }
        public virtual Policies PoliciesNavigation { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<PlanReviews> PlanReviews { get; set; }
    }
}
