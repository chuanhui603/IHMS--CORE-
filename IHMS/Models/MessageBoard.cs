using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class MessageBoard
{
    public int MessageId { get; set; }

    public string Title { get; set; } = null!;

    public string Contents { get; set; } = null!;

    public string Category { get; set; } = null!;

    public int MemberId { get; set; }

    public DateTime Time { get; set; }

    public virtual Member Member { get; set; } = null!;
}
