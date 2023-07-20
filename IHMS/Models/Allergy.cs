using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Allergy
{
    public int AMemberId { get; set; }

    public string? AAllergyDescription { get; set; }

    public virtual Member AMember { get; set; } = null!;
}
