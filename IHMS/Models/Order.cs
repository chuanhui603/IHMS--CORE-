using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Order
{
    public int OOrderId { get; set; }

    public int OMemberId { get; set; }

    public int OTeacherId { get; set; }

    public int OScheduleId { get; set; }

    public string OCourse { get; set; } = null!;

    public int OPoints { get; set; }

    public DateTime? OOrderDate { get; set; }

    public string? ONote1 { get; set; }

    public string? ONote2 { get; set; }

    public string? ONote3 { get; set; }
}
