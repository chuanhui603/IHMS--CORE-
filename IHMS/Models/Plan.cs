using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Plan
{
    public int PlanId { get; set; }

    public int MemberId { get; set; }

    public int Weight { get; set; }

    public int BodyPercentage { get; set; }

    public DateTime? Registerdate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<Diet> Diets { get; set; } = new List<Diet>();

    public virtual Member Member { get; set; } = null!;

    public virtual ICollection<Sport> Sports { get; set; } = new List<Sport>();

    public virtual ICollection<Water> Water { get; set; } = new List<Water>();
}
