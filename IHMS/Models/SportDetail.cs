using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class SportDetail
{
    public int SportDetailId { get; set; }

    public int SportId { get; set; }

    public string Sname { get; set; } = null!;

    public int? Frequency { get; set; }

    public string? Type { get; set; }

    public DateTime Registerdate { get; set; }

    public string? Time { get; set; }

    public int? Timelong { get; set; }

    public bool? Isdone { get; set; }

    public int? Sets { get; set; }

    public DateTime? Sportdate { get; set; }
}
