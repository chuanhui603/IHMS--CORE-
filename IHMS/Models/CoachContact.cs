using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class CoachContact
{
    public CoachContact()
    {
        
        Courses = new HashSet<Course>();
    }
    public int CoachContactId { get; set; }

    public int? MemberId { get; set; }

    public int? CoachId { get; set; }

    public string? ContactDate { get; set; }

    public int? StatusNumber { get; set; }

    public int? AvailableTimeNum { get; set; }

    public virtual Coach? Coach { get; set; }
    public virtual Member Member { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
