using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Coach
{
    public int CoachId { get; set; }

    public int MemberId { get; set; }

    public string? Intro { get; set; }

    public string Image { get; set; } = null!;

    public int? Rank { get; set; }

    public double? Commission { get; set; }

    public string Resume { get; set; } = null!;

    public string? Video { get; set; }

    public int Condition { get; set; }

    public string? Reason { get; set; }

    public DateTime Applytime { get; set; }

    public DateTime Confirmtime { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Member Member { get; set; } = null!;
}
