using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VolunteerDataWebApi.Models
{
    public enum Intent : int
    {
        None = 0,
        Uninterested = 1,
        Interested = 2,
        Confirmed = 3,
        Cancelled = 4
    }

    public class VolunteerIntent
    {
        public int Id { get; set; }
        public DateTime? StartDateTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public Intent Intent { get; set; }

        public int VolunteerId { get; set; }
        public int EventId { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual Event Event { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual Volunteer Volunteer { get; set; }
    }
}