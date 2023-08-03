using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Coach
{
    public Coach()
    {
        CoachAvailableTimes = new HashSet<CoachAvailableTime>();
        CoachContacts = new HashSet<CoachContact>();
        CoachExperiences = new HashSet<CoachExperience>();
        CoachLicenses = new HashSet<CoachLicense>();
        CoachSkills = new HashSet<CoachSkill>();
    }
    public int CoachId { get; set; }

    public string? CoachName { get; set; }

    public int? MemberId { get; set; }

    public int? CityId { get; set; }

    public string? CoachImage { get; set; }

    public int? CoachFee { get; set; }

    public string? CoachDescription { get; set; }

    public string? ApplyDate { get; set; }

    public int? StatusNumber { get; set; }

    public bool? Visible { get; set; }

    public int? CourseCount { get; set; }

    public string? Slogan { get; set; }

    public virtual City City { get; set; }
    public virtual Member Member { get; set; }

    public virtual ICollection<CoachAvailableTime> CoachAvailableTimes { get; set; } = new List<CoachAvailableTime>();

    public virtual ICollection<CoachContact> CoachContacts { get; set; } = new List<CoachContact>();

    public virtual ICollection<CoachExperience> CoachExperiences { get; set; } = new List<CoachExperience>();

    public virtual ICollection<CoachLicense> CoachLicenses { get; set; } = new List<CoachLicense>();

    public virtual ICollection<CoachRate> CoachRates { get; set; } = new List<CoachRate>();

    public virtual ICollection<CoachSkill> CoachSkills { get; set; } = new List<CoachSkill>();

    
}
