using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductRecallSystem.BOL
{
    public class Recalls
    {
        [Key]
        public long RecallID { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        [ForeignKey("Products")]
        public long ProductId { get; set; }
        public virtual Products Products { get; set; }

        public virtual IEnumerable<Announcements> Announcements { get; set; }
    }
}
