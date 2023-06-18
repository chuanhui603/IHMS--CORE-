using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Member
{
    public int MMemberId { get; set; }

    public string MName { get; set; } = null!;

    public string MEmail { get; set; } = null!;

    public string MAccount { get; set; } = null!;

    public string MPassword { get; set; } = null!;

    public DateTime? MBirthday { get; set; }

    public bool? MGender { get; set; }

    public bool? MMaritalStatus { get; set; }

    public string? MNickname { get; set; }

    public string? MAvatarImage { get; set; }

    public string? MResidentialCity { get; set; }

    public int? MPermission { get; set; }

    public string? MOccupation { get; set; }

    public int? MPoints { get; set; }

    public DateTime? MLoginTime { get; set; }

    public virtual Allergy? Allergy { get; set; }

    public virtual HealthInfo? HealthInfo { get; set; }

    public virtual MedicalHistory? MedicalHistory { get; set; }
}
