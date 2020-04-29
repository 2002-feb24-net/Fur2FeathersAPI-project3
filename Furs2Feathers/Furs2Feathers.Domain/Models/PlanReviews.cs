using System;
using System.Collections.Generic;
using System.Text;

namespace Furs2Feathers.Domain.Models
{
    public class PlanReviews
    {
        public int PlanReviewId { get; set; }
        public string Review { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
