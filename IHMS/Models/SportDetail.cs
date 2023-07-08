using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class SportDetail
{
    public int SportDetailId { get; set; }

    public int SportId { get; set; }

    public string Sname { get; set; } = null!;

    public TimeSpan Sporttime { get; set; }

    public int? Frequency { get; set; }

    public string? Image { get; set; }

    public string? Type { get; set; }

    public string? Description { get; set; }
}
