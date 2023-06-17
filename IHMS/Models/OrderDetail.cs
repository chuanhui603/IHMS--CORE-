using System;
using System.Collections.Generic;

namespace Final.Models;

public partial class OrderDetail
{
    public int OOrderdetailId { get; set; }

    public int OScheduleId { get; set; }

    public int OCourseorderId { get; set; }

    public DateTime? OCreatetime { get; set; }

    public DateTime? OUpdatetime { get; set; }
}
