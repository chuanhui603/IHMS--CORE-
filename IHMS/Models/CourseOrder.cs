using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class CourseOrder
{
    public int CoCourseorderId { get; set; }

    public int CoMemberId { get; set; }

    public int CoPointstotal { get; set; }

    public string CoState { get; set; } = null!;

    public string? CoReason { get; set; }

    public DateTime? CoCreatetime { get; set; }

    public DateTime? CoUpdatetime { get; set; }
}
