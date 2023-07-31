using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class CoachAvailableTime
{
    public int CoachTimeId { get; set; }

    public int? CoachId { get; set; }

    public int? AvailableTimeId { get; set; }

    public virtual AvailableTime? AvailableTime { get; set; }

    public virtual Coach? Coach { get; set; }
}
