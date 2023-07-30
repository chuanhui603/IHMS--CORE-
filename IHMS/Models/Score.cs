using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Score
{
    public int ScoreId { get; set; }

    public int CourseId { get; set; }

    public int MemberId { get; set; }

    public int? Score1 { get; set; }

    public DateTime? ScoredDate { get; set; }

    public virtual Member Member { get; set; } = null!;
}
