using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Schedule
{
    public int SScheduleId { get; set; }

    public int STeacherId { get; set; }

    public DateTime SStartTime { get; set; }

    public DateTime SEndTime { get; set; }

    public int SBooking { get; set; }
}
