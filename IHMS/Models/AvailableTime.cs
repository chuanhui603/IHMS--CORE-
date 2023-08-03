using System;
using System.Collections.Generic;

namespace IHMS.Models
{

    public partial class AvailableTime
    {
        public int AvailableTimeId { get; set; }

        public string? AvailableTime1 { get; set; }

        public int? AvailableTimeNum { get; set; }

        public virtual ICollection<CoachAvailableTime> CoachAvailableTimes { get; set; } = new List<CoachAvailableTime>();
    }
}
