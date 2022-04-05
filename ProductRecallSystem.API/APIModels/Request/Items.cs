using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRecallSystem.API.APIModels.Request
{
    public class Items
    {
        [Key]
        public int ItemId { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }

        public string Category { get; set; }
    }
}
