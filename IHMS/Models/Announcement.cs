using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Announcement
{
    public int AnAnnouncemetId { get; set; }

    public string AnTitle { get; set; } = null!;

    public string AnContent { get; set; } = null!;

    public DateTime AnTime { get; set; }

    public string? AnImage { get; set; }
}
