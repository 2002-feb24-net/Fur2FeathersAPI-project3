using System;
using System.Collections.Generic;

namespace Furs2Feathers.DataAccess.Models
{
    public partial class PlanReviews
    {
        public int PlanReviewId { get; set; }
        public string Review { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
