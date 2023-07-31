using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class CoachExperience
{
    public int ExperienceId { get; set; }

    public int? CoachId { get; set; }

    public string? Experience { get; set; }

    public virtual Coach? Coach { get; set; }
}
