using System;
using System.Collections.Generic;

namespace Final.Models;

public partial class PointRecord
{
    public int PPointrecordId { get; set; }

    public int PCount { get; set; }

    public int PMemberId { get; set; }

    public int PBankNumber { get; set; }

    public DateTime? PCreatetime { get; set; }

    public DateTime? PUpdatetime { get; set; }
}
