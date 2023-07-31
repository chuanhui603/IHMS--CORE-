using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Water
{
    public int WaterId { get; set; }

    public int PlanId { get; set; }

    public int? Drink { get; set; }

    public DateTime? Createdate { get; set; }

    public virtual Plan Plan { get; set; } = null!;
}
