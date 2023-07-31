using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class City
{
    public int CityId { get; set; }

    public string? CityName { get; set; }

    public int? CityOrder { get; set; }

    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();
}
