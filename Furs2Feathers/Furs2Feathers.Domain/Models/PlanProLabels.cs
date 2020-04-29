using System;
using System.Collections.Generic;
using System.Text;

namespace Furs2Feathers.Domain.Models
{
    public class PlanProLabels
    {
        public int PlanProLabelsId { get; set; }
        public string Labels { get; set; }
        public int? PlanId { get; set; }

        public Plan Plan { get; set; }
    }
}
