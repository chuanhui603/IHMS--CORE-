using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IHMS.Models;

public partial class Order
{
    public int OrderId { get; set; }

    [Display(Name = "訂單編號")]
    public string Ordernumber { get; set; } = null!;

    [Display(Name = "會員ID")]
    public int MemberId { get; set; }

    [Display(Name = "點數總計")]
    public int Pointstotal { get; set; }

    [Display(Name = "狀態")]
    public string State { get; set; } = null!;

    public string? Reason { get; set; }

    [Display(Name = "下單時間")]
    public DateTime? Createtime { get; set; }

    public DateTime? Updatetime { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
