using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furs2Feathers.DataAccess.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Invoice = new HashSet<Invoice>();
            PlanReviews = new HashSet<PlanReviews>();
        }

        [Key]
        public int CustomerId { get; set; }
        public int? Policies { get; set; }
        public int? Address { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual Address AddressNavigation { get; set; }
        public virtual Policies PoliciesNavigation { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<PlanReviews> PlanReviews { get; set; }
    }
}
