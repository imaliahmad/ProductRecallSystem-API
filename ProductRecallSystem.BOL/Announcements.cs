using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductRecallSystem.BOL
{
    public class Announcements
    {
        [Key]
        public long AnnouncementId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }


        [ForeignKey("Recalls")]
        public long RecallID { get; set; }
        public virtual Recalls Recalls { get; set; }
    }
}
