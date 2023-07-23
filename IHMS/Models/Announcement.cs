using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Announcement
{
    public int AnnouncemetId { get; set; }

    public string Title { get; set; } = null!;

    public string Contents { get; set; } = null!;

    public DateTime Time { get; set; }

    public string? Image { get; set; }
}
