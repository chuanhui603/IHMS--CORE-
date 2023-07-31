using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class CoachSkill
{
    public int CoachSkillId { get; set; }

    public int? CoachId { get; set; }

    public int? SkillId { get; set; }

    public virtual Coach? Coach { get; set; }

    public virtual Skill? Skill { get; set; }
}
