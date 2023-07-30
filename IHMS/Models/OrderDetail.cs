using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class OrderDetail
{
    public int OrderdetailId { get; set; }

    public int ScheduleId { get; set; }

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;
}
