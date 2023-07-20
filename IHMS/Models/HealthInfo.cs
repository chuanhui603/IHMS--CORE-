using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class HealthInfo
{
    public int MemberId { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public string? BloodPressure { get; set; }

    public decimal? BodyFatPercentage { get; set; }

    public DateTime? DateEntered { get; set; }

    public virtual Member Member { get; set; } = null!;
}
