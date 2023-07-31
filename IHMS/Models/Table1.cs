using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Table1
{
    public int DietImgId { get; set; }

    public int DietDetailId { get; set; }

    public string Img { get; set; } = null!;

    public virtual DietDetail DietDetail { get; set; } = null!;
}
