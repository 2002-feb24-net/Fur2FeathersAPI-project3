﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furs2Feathers.DataAccess.Models
{
    public partial class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        public decimal Cost { get; set; }
        public int CustomerId { get; set; }
        public int PetId { get; set; }
        public string Notes { get; set; }
        public int EmpId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Emp { get; set; }
        public virtual Pet Pet { get; set; }
    }
}
