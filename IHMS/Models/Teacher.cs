using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Teacher
{
    public int TTeacherId { get; set; }

    public int TMemberId { get; set; }

    public string TMajor { get; set; } = null!;

    public string TIntro { get; set; } = null!;

    public int TPrice { get; set; }

    public int TLevel { get; set; }

    public double TCommission { get; set; }

    public DateTime TApplytime { get; set; }
}
