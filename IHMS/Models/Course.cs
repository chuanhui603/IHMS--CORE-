using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Course
{
    public int CourseId { get; set; }



    public int? CoachContactId { get; set; }

    public string? CourseName { get; set; }

    public int CourseTotal { get; set; }

    public int? StatusNumber { get; set; }

    public bool? Visible { get; set; }

    public int? AvailableTimeNum { get; set; }

    public virtual CoachContact? CoachContact { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
