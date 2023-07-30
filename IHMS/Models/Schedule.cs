using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int? CourseId { get; set; }

    public string? CourseTime { get; set; }

    public int? StatusNumber { get; set; }

    public virtual Course? Course { get; set; }
}
