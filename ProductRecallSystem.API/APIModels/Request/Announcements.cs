using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRecallSystem.API.APIModels.Request
{
    public class Announcements
    {
        public long AnnouncementId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }


        public long RecallID { get; set; }
        public virtual Recalls Recalls { get; set; }
    }
}
