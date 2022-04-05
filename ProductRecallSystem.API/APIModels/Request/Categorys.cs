﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRecallSystem.API.APIModels.Request
{
    public class Categorys
    {
        [Key]
        public int CategoryId { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}
