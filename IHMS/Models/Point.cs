using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Point
{
    public int PPointId { get; set; }

    public int PCount { get; set; }

    public int PMemberId { get; set; }

    public DateTime? PDatetime { get; set; }

    public string? PNote1 { get; set; }

    public string? PNote2 { get; set; }

    public string? PNote3 { get; set; }
}
