using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Plan
{
    public int PlanId { get; set; }

    public int MemberId { get; set; }

    public string? Type { get; set; }

    public int Weight { get; set; }

    public double BodyPercentage { get; set; }

    public DateTime RegisterDate { get; set; }

    public string? Times { get; set; }

    public int Age { get; set; }

    public double? Tdee { get; set; }

    public double? Bmr { get; set; }

    public int? Height { get; set; }

    public virtual ICollection<Diet> Diets { get; set; } = new List<Diet>();

    public virtual Member Member { get; set; } = null!;

    public virtual ICollection<Sport> Sports { get; set; } = new List<Sport>();

    public virtual ICollection<Water> Water { get; set; } = new List<Water>();
}
