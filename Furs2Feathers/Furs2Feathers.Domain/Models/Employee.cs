using System;
using System.Collections.Generic;
using System.Text;

namespace Furs2Feathers.Domain.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Invoice> Invoice { get; set; }
        public List<Policies> Policies { get; set; }
    }
}
