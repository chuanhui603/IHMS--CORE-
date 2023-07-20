using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class PointRecord
{
    public int PointrecordId { get; set; }

    public int Count { get; set; }

    public int MemberId { get; set; }

    public int BankNumber { get; set; }

    public DateTime? Createtime { get; set; }

    public DateTime? Updatetime { get; set; }

    public virtual Member Member { get; set; } = null!;
}
