using System;
using System.Collections.Generic;
using System.Text;

namespace Furs2Feathers.Domain.Models
{
    public class Claims
    {
        public int ClaimId { get; set; }
        public string Description { get; set; }
        public int PolicyId { get; set; }
        public DateTime FilingDate { get; set; }

        public Policies Policy { get; set; }
    }
}
