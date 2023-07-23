using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Answer
{
    public int? AnswerId { get; set; }

    public int? QuestionsetId { get; set; }

    public string? Answer1 { get; set; }

    public DateTime? Time { get; set; }

    public virtual Questionset? Questionset { get; set; }
}
