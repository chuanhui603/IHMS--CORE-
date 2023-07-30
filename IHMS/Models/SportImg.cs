using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class SportImg
{
    public int SportImgId { get; set; }

    public int SportDetailId { get; set; }

    public string Img { get; set; } = null!;

    public virtual SportDetail SportDetail { get; set; } = null!;
}
