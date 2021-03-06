using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRecallSystem.API.APIModels.Request
{
    public class Manufacturers
    {
        public long ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        //list
        public virtual IEnumerable<Products> Products { get; set; }
    }
}
