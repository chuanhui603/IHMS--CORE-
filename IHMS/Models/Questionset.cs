using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Questionset
{
    public int QQuestionsetId { get; set; }

    public string? QQuestion { get; set; }

    public string? QCategory { get; set; }
}
