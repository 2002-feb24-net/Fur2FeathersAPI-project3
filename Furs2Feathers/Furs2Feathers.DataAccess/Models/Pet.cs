using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furs2Feathers.DataAccess.Models
{
    public partial class Pet
    {
        public Pet()
        {
            Invoice = new HashSet<Invoice>();
            Policies = new HashSet<Policies>();
        }

        [Key]
        public int PetId { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<Policies> Policies { get; set; }
    }
}
