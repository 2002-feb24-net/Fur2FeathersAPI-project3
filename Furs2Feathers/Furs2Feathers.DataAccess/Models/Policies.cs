﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furs2Feathers.DataAccess.Models
{
    public partial class Policies
    {
        public Policies()
        {
            Claims = new HashSet<Claims>();
            Customer = new HashSet<Customer>();
        }

        [Key]
        public int PolicyId { get; set; }
        public decimal Deductible { get; set; }
        public decimal Premium { get; set; }
        public int? PetId { get; set; }
        public DateTime? RenewalDate { get; set; }
        public int EmpId { get; set; }
        public bool Active { get; set; }

        public virtual Employee Emp { get; set; }
        public virtual Pet Pet { get; set; }
        public virtual ICollection<Claims> Claims { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
    }
}
