using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Article
{
    public int ArticleId { get; set; }

    public string? Title { get; set; }

    public string? Contents { get; set; }

    public DateTime? Time { get; set; }
}
