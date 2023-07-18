using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class MessageBoardDetail
{
    public int MessageId { get; set; }

    public string Contents { get; set; } = null!;

    public int MemberId { get; set; }

    public DateTime Time { get; set; }

    public string? Image { get; set; }
}
