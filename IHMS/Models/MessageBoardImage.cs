using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class MessageBoardImage
{
    public int MessageId { get; set; }

    public string? Image { get; set; }

    public virtual MessageBoard Message { get; set; } = null!;
}
