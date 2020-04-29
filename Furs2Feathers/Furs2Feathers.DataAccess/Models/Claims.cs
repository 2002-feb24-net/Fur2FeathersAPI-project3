using System;
using System.Collections.Generic;

namespace Furs2Feathers.DataAccess.Models
{
    public partial class Claims
    {
        public int ClaimId { get; set; }
        public string Description { get; set; }
        public int PolicyId { get; set; }
        public DateTime FilingDate { get; set; }

        public virtual Policies Policy { get; set; }
    }
}
