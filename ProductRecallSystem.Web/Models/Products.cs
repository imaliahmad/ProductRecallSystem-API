using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRecallSystem.Web.Models
{
    public class Products
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public long ManufacturerId { get; set; }
        public virtual Manufacturers Manufacturers { get; set; }

        public virtual IEnumerable<Recalls> Recalls { get; set; }
    }
}
