using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Sport
{
    public int SportId { get; set; }

    public int PlanId { get; set; }

    public string Type { get; set; } = null!;

    public string Sname { get; set; } = null!;

    public TimeSpan? Time { get; set; }

    public int? Number { get; set; }

    public DateTime Date { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public DateTime Registerdate { get; set; }

    public virtual Plan Plan { get; set; } = null!;
}
