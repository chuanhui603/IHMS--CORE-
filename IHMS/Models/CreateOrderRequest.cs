using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class OrderRequest
{
    public string Ordernumber { get; set; } = null!;

    public int MemberId { get; set; }

    public int Pointstotal { get; set; }
}
