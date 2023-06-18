using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Plan
{
    public int PPlanId { get; set; }

    public int PMemberId { get; set; }

    public int PWeight { get; set; }

    public int PBodyPercentage { get; set; }

    public DateTime? PRegisterdate { get; set; }

    public DateTime? PEndDate { get; set; }
}
