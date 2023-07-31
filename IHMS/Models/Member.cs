using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IHMS.Models;

public partial class Member
{
    public int MemberId { get; set; }
    [DisplayName("姓名")]
    public string Name { get; set; } = null!;
    [Required]
    [DisplayName("電子信箱")]
    public string Email { get; set; } = null!;
    [DisplayName("電話")]
    public string? Phone { get; set; }
    [Required]
    [DisplayName("使用者名稱")]
    public string Account { get; set; } = null!;
    [Required][DisplayName("密碼")]
    public string Password { get; set; } = null!;
    [DisplayName("生日")]
    public DateTime? Birthday { get; set; }
    [DisplayName("生理性別")]
    public bool? Gender { get; set; }
    [DisplayName("婚姻狀態")]
    public bool? MaritalStatus { get; set; }
    [DisplayName("暱稱")]
    public string? Nickname { get; set; }
    [DisplayName("照片")]
    public string? AvatarImage { get; set; }
    [DisplayName("居住城市")]
    public string? ResidentialCity { get; set; }
    [DisplayName("權限名稱")]
    public int? Permission { get; set; }
    [DisplayName("職業")]
    public string? Occupation { get; set; }
    [DisplayName("疾病史描述")]
    public string? DiseaseDescription { get; set; }
    [DisplayName("過敏史描述")]
    public string? AllergyDescription { get; set; }
    [DisplayName("最後登入時間")]
    public DateTime? LoginTime { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();

    public virtual ICollection<CustomerService> CustomerServices { get; set; } = new List<CustomerService>();

    public virtual HealthInfo? HealthInfo { get; set; }

    public virtual ICollection<MessageBoard> MessageBoards { get; set; } = new List<MessageBoard>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();

    public virtual ICollection<PointRecord> PointRecords { get; set; } = new List<PointRecord>();

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
