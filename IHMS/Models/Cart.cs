using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Cart
{
    public int CCartId { get; set; }

    public int CMemberId { get; set; }

    public int CScheduleId { get; set; }

    public DateTime? CCreatetime { get; set; }

    public DateTime? CUpdatetime { get; set; }
}
