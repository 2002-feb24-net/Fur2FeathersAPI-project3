using System;
using System.Collections.Generic;

namespace Furs2Feathers.DataAccess.Models
{
    public partial class PlanProLabels
    {
        public int PlanProLabelsId { get; set; }
        public string Labels { get; set; }
        public int? PlanId { get; set; }

        public virtual Plan Plan { get; set; }
    }
}
