using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Course
{
    public int CCourseId { get; set; }

    public int CTeacherId { get; set; }

    public string CType { get; set; } = null!;

    public string CName { get; set; } = null!;

    public string CIntro { get; set; } = null!;

    public string? CVideo { get; set; }
}
