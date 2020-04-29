using System;
using System.Collections.Generic;
using System.Text;

namespace Furs2Feathers.Domain.Models
{
    public class Policies
    {

        public int PolicyId { get; set; }
        public decimal Deductible { get; set; }
        public decimal Premium { get; set; }
        public int? PetId { get; set; }
        public DateTime? RenewalDate { get; set; }
        public int EmpId { get; set; }
        public bool Active { get; set; }

        public Employee Emp { get; set; }
        public Pet Pet { get; set; }
        public List<Claims> Claims { get; set; } = new List<Claims>();
        public List<Customer> Customer { get; set; } = new List<Customer>();
    }
}
