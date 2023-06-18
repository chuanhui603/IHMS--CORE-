using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Diet
{
    public int DDietId { get; set; }

    public int DPlanId { get; set; }

    public DateTime? DDate { get; set; }

    public string? DImage { get; set; }

    public DateTime DRegisterdate { get; set; }
}
