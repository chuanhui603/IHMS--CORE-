using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Water
{
    public int WWaterId { get; set; }

    public int WPlanId { get; set; }

    public int? WDrinkId { get; set; }

    public DateTime? WDate { get; set; }
}
