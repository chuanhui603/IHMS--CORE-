using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Schedule
{
    public int SScheduleId { get; set; }

    public int SCourseId { get; set; }

    public int SPoint { get; set; }

    public DateTime SMonth { get; set; }

    public DateTime SStartTime { get; set; }

    public DateTime SEndTime { get; set; }

    public int? SScore { get; set; }
}
