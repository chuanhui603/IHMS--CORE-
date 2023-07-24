using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class DietDetail
{
    public int DietDetailId { get; set; }

    public int DietId { get; set; }

    public string Dname { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int Calories { get; set; }

    public string? Decription { get; set; }

    public DateTime? Registerdate { get; set; }

    public virtual Diet Diet { get; set; } = null!;

    public virtual ICollection<DietImg> DietImgs { get; set; } = new List<DietImg>();

    public virtual ICollection<Table1> Table1s { get; set; } = new List<Table1>();
}
