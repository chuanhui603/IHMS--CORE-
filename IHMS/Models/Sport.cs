using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Sport
{
    public int SportId { get; set; }

    public int PlanId { get; set; }

    public DateTime Createdate { get; set; }

    public virtual Plan Plan { get; set; } = null!;
}
