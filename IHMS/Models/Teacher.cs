using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Teacher
{
    public int TTeacherId { get; set; }

    public int? TMemberId { get; set; }

    public string? TIntro { get; set; }

    public string? TImage { get; set; }

    public int? TLevel { get; set; }

    public double? TCommission { get; set; }

    public string? TResume { get; set; }

    public string? TVideo { get; set; }

    public DateTime? TApplytime { get; set; }

    public DateTime? TConfirmtime { get; set; }

    public int? TCondition { get; set; }

    public int? TScore { get; set; }

    public string? TReason { get; set; }
}
