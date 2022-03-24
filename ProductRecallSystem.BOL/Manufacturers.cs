using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductRecallSystem.BOL
{
    public class Manufacturers
    {
        [Key]
        public long ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Address{ get; set; }
        public string City { get; set; }
        public string State { get; set; }

        //list
        public virtual IEnumerable<Products> Products { get; set; }
    }
}
