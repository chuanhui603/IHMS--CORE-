using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class CoachLicense
{
    public int LicenseId { get; set; }

    public int? CoachId { get; set; }

    public string? License { get; set; }

    public virtual Coach? Coach { get; set; }
}
