using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furs2Feathers.DataAccess.Models
{
    public partial class PlanReviews
    {
        [Key]
        public int PlanReviewId { get; set; }
        public string Review { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
