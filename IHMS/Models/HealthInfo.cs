using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class HealthInfo
{
    public int HMemberId { get; set; }

    public decimal? HHeight { get; set; }

    public decimal? HWeight { get; set; }

    public string? HBloodPressure { get; set; }

    public decimal? HBmi { get; set; }

    public decimal? HBodyFatPercentage { get; set; }

    public DateTime? HDateEntered { get; set; }

    public virtual Member HMember { get; set; } = null!;
}
