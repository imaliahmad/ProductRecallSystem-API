using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductRecallSystem.BOL
{
    public class Products
    {
        [Key]
        public long ProductId { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("Manufacturers")]
        public long ManufacturerId { get; set; }
        public virtual Manufacturers Manufacturers { get; set; }

        public virtual IEnumerable<Recalls> Recalls { get; set; }
    }
}
