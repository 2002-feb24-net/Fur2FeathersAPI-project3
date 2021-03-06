﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furs2Feathers.DataAccess.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Invoice = new HashSet<Invoice>();
            Policies = new HashSet<Policies>();
        }

        [Key]
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<Policies> Policies { get; set; }
    }
}
