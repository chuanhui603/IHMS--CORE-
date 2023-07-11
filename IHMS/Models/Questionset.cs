using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Questionset
{
    public int QuestionsetId { get; set; }

    public string? Question { get; set; }

    public string? Category { get; set; }
}
