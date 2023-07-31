using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class CustomerService
{
    public int CustomerServiceId { get; set; }

    public int MemberId { get; set; }

    public string? Title { get; set; }

    public string? Contents { get; set; }

    public string? Reply { get; set; }

    public DateTime? CreatedTime { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public virtual Member Member { get; set; } = null!;
}
