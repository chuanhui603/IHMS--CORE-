using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IHMS.Models;

public partial class Member
{
    public int MMemberId { get; set; }
    [DisplayName ("姓名")]
    [Required(ErrorMessage ="姓名必須填寫")]
    public string? MName { get; set; } = null!;
    [DisplayName("電子郵件")]
    [Required(ErrorMessage = "信箱必須填寫")]
    public string? MEmail { get; set; } = null!;    
    [DisplayName("帳號")]
    [Required(ErrorMessage = "帳號必須填寫")]
    public string? MAccount { get; set; } = null!;
    [DisplayName("密碼")]
    [Required(ErrorMessage = "密碼必須填寫")]
    public string? MPassword { get; set; } = null!;
    [DisplayName("電話")]
    public string? MPhone { get; set; }
    [DisplayName("生日")]
    public DateTime? MBirthday { get; set; }
    [DisplayName("性別")]
    public bool? MGender { get; set; }
    [DisplayName("婚姻狀況")]
    public bool? MMaritalStatus { get; set; }
    [DisplayName("暱稱")]
    public string? MNickname { get; set; }
    [DisplayName("會員")]
    public string? MAvatarImage { get; set; }
    [DisplayName("居住城市")]
    public string? MResidentialCity { get; set; }
    [DisplayName("權限")]
    public int? MPermission { get; set; }
    [DisplayName("職業")]
    public string? MOccupation { get; set; }
    [DisplayName("疾病史")]
    public string? MDiseaseDescription { get; set; }
    [DisplayName("過敏史")]
    public string? MAllergyDescription { get; set; }
    [DisplayName("最後登入時間")]
    public DateTime? MLoginTime { get; set; }
    
}
