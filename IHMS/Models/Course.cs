using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public int CoachId { get; set; }

    public string Coursename { get; set; } = null!;

    public string? Intro { get; set; }

    public string? Video { get; set; }

    public string? Image1 { get; set; }

    public string? Image2 { get; set; }

    public string? Image3 { get; set; }

    public virtual Coach Coach { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
