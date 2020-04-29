using System;
using System.Collections.Generic;
using System.Text;

namespace Furs2Feathers.Domain.Models
{
    public class Plan
    {

        public int PlanId { get; set; }
        public decimal? EstCost { get; set; }
        public short? PositivesMax { get; set; }
        public string Description { get; set; }

        public List<PlanProLabels> PlanProLabels { get; set; } = new List<PlanProLabels>();
    }
}
