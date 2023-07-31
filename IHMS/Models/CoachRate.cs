using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class CoachRate
{
    public int RateId { get; set; }

    public int? MemberId { get; set; }

    public int? CoachId { get; set; }

    public int? RateStar { get; set; }

    public string? FRateText { get; set; }

    public string? FRateDate { get; set; }

    public bool? Visible { get; set; }

    public virtual Coach? Coach { get; set; }
}
