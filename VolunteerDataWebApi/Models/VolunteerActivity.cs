using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VolunteerDataWebApi.Models
{
    [Flags] public enum ReportingProperties : int
    {
        None = 0,
        Reviewed = 1,
        Amended = 2,
        Invalidated = 4,
        PostdatedStart = 8,
        PostdatedEnd = 16
    }

    public class VolunteerActivity
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public ReportingProperties ReportingProperties { get; set; }

        public int VolunteerId { get; set; }
        public int EventId { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual Event Event { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual Volunteer Volunteer { get; set; }
    }
}