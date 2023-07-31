using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int MemberId { get; set; }

    public int ScheduleId { get; set; }

    public virtual Member Member { get; set; } = null!;
}
