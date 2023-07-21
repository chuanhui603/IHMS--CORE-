using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Diet
{
    public int DietId { get; set; }

    public int PlanId { get; set; }

    public DateTime? Date { get; set; }

    public string? Image { get; set; }

    public DateTime Registerdate { get; set; }

    public virtual ICollection<DietDetail> DietDetails { get; set; } = new List<DietDetail>();

    public virtual Plan Plan { get; set; } = null!;
}
