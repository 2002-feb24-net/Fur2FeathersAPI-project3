using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Furs2Feathers.Domain.Models
{
    public class Pet
    {

        public int PetId { get; set; }
        public string Name { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public string Species { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public List<Invoice> Invoice { get; set; } = new List<Invoice>();
        public List<Policies> Policies { get; set; } = new List<Policies>();
    }
}
