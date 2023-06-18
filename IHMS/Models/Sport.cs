using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Sport
{
    public int SSportId { get; set; }

    public int SPlanId { get; set; }

    public string SType { get; set; } = null!;

    public string SName { get; set; } = null!;

    public TimeSpan? STime { get; set; }

    public int? SNumber { get; set; }

    public DateTime SDate { get; set; }

    public string? SImage { get; set; }

    public string? SDescription { get; set; }

    public DateTime SRegisterdate { get; set; }
}
