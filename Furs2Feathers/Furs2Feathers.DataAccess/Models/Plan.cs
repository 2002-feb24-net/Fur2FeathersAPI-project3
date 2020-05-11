using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furs2Feathers.DataAccess.Models
{
    public partial class Plan
    {
        public Plan()
        {
            PlanProLabels = new HashSet<PlanProLabels>();
        }

        [Key]
        public int PlanId { get; set; }
        public decimal? EstCost { get; set; }
        public short? PositivesMax { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PlanProLabels> PlanProLabels { get; set; }
    }
}
