using IHMS.DTO;
using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? Birthday { get; set; }

    public bool? Gender { get; set; }

    public bool? MaritalStatus { get; set; }

    public string? Nickname { get; set; }

    public string? AvatarImage { get; set; }

    public string? ResidentialCity { get; set; }

    public int? Permission { get; set; }

    public string? Occupation { get; set; }

    public string? DiseaseDescription { get; set; }

    public string? AllergyDescription { get; set; }

    public DateTime? LoginTime { get; set; }    

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();

    public virtual HealthInfo? HealthInfo { get; set; }

    public virtual ICollection<MessageBoard> MessageBoards { get; set; } = new List<MessageBoard>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<PointRecord> PointRecords { get; set; } = new List<PointRecord>();

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
