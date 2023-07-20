using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class MedicalHistory
{
    public int MhMemberId { get; set; }

    public string? MhDiseaseDescription { get; set; }

    public virtual Member MhMember { get; set; } = null!;
}
