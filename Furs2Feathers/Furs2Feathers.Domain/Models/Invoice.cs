using System;
using System.Collections.Generic;
using System.Text;

namespace Furs2Feathers.Domain.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public decimal Cost { get; set; }
        public int CustomerId { get; set; }
        public int PetId { get; set; }
        public string Notes { get; set; }
        public int EmpId { get; set; }

        public Customer Customer { get; set; }
        public Employee Emp { get; set; }
        public Pet Pet { get; set; }
    }
}
