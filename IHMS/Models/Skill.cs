using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Skill
{
    public int SkillId { get; set; }

    public string? SkillName { get; set; }

    public string? SkillDescription { get; set; }

    public string? SkillImage { get; set; }

    public virtual ICollection<CoachSkill> CoachSkills { get; set; } = new List<CoachSkill>();
}
