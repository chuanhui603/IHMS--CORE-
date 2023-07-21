using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class DietDetail
{
    public int DietDetailId { get; set; }

    public int DietId { get; set; }

    public string? Fname { get; set; }

    public string? FoodType { get; set; }

    public string? Type { get; set; }

    public int? Calories { get; set; }

    public string? Decription { get; set; }

    public string? Img { get; set; }

    public virtual Diet Diet { get; set; } = null!;
}
