using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string Ordernumber { get; set; } = null!;

    public int MemberId { get; set; }

    public int Pointstotal { get; set; }

    public string State { get; set; } = null!;

    public string? Reason { get; set; }
    public DateTime? Createtime { get; set; } = DateTime.Now;

    public DateTime? Updatetime { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
