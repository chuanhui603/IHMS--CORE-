using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IHMS.Models;

public partial class PointRecord
{
    public int PointrecordId { get; set; }

    [Display(Name = "數量")]
    public int Count { get; set; }

    [Display(Name = "會員ID")]
    public int MemberId { get; set; }

    [Display(Name = "銀行碼")]
    public int BankNumber { get; set; }

    [Display(Name = "下單時間")]
    public DateTime? Createtime { get; set; }

    public DateTime? Updatetime { get; set; }

    public virtual Member Member { get; set; } = null!;
}
