using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int CourseId { get; set; }

    public int Point { get; set; }

    public DateTime Month { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
