using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRecallSystem.Web.Models
{
    public class Recalls
    {
        public long RecallID { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public long ProductId { get; set; }
        public virtual Products Products { get; set; }

        public virtual IEnumerable<Announcements> Announcements { get; set; }
    }
}
