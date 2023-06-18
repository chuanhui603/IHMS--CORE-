using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class DietDetail
{
    public int DdDietDetailId { get; set; }

    public int DdDietId { get; set; }

    public string? DName { get; set; }

    public string? DFoodType { get; set; }

    public string? DType { get; set; }

    public int? DCalories { get; set; }

    public string? DDecription { get; set; }
}
