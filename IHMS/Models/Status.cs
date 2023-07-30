using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public int StatusNumber { get; set; }

    public string Status1 { get; set; } = null!;
}
