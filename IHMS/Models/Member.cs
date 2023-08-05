using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IHMS.Models;

public partial class Member
{
    public int MemberId { get; set; }
    [Required(ErrorMessage = "請填寫姓名")]
    [DisplayName("姓名")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "請填寫信箱")]
    [DisplayName("信箱")]
    public string Email { get; set; } = null!;   
    [DisplayName("電話")]
    public string? Phone { get; set; }

    [Required(ErrorMessage = "請填寫帳號")]
    [DisplayName("帳號")]
    public string Account { get; set; } = null!;

    [Required(ErrorMessage = "請填寫密碼")]
    [DisplayName("密碼")]
    public string Password { get; set; } = null!;

    [DisplayName("生日")]
    public DateTime? Birthday { get; set; }

    [DisplayName("性別")]
    public bool? Gender { get; set; }

    [DisplayName("婚姻狀況")]
    public bool? MaritalStatus { get; set; }

    [DisplayName("暱稱")]
    public string? Nickname { get; set; }

    [DisplayName("頭像圖片")]
    public string? AvatarImage { get; set; }

    [DisplayName("居住城市")]
    public string? ResidentialCity { get; set; }

    [DisplayName("權限")]
    public int? Permission { get; set; }

    [DisplayName("職業")]
    public string? Occupation { get; set; }

    [DisplayName("疾病描述")]
    public string? DiseaseDescription { get; set; }

    [DisplayName("過敏描述")]
    public string? AllergyDescription { get; set; }

    [DisplayName("登入時間")]
    public DateTime? LoginTime { get; set; }


    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<CoachContact> CoachContacts { get; set; } = new List<CoachContact>();

    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();

    public virtual ICollection<CustomerService> CustomerServices { get; set; } = new List<CustomerService>();

    public virtual HealthInfo? HealthInfo { get; set; }

    public virtual ICollection<MessageBoard> MessageBoards { get; set; } = new List<MessageBoard>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();

    public virtual ICollection<PointRecord> PointRecords { get; set; } = new List<PointRecord>();

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();

    public class DoPwdIn
    {
        public string Password { get; set; }
        public string CheckUserPwd { get; set; }
    }

    // 註冊參數    
    public class DoRegisterIn
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

   
    // 註冊回傳   
    public class DoRegisterOut
    {
        public string ErrMsg { get; set; }
        public string ResultMsg { get; set; }
    }

    
    // 登入參數
    public class DoLoginIn
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string KeepLogin { get; set; }
    }

    
    // 登入回傳
    public class DoLoginOut
    {
        public string ErrMsg { get; set; }
        public string ResultMsg { get; set; }
    }


    // 取得個人資料回傳
    public class GetUserProfileOut
    {
        public string ErrMsg { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }


    // 修改個人資料參數
    public class DoEditProfileIn
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    // 修改個人資料回傳
    public class DoEditProfileOut
    {
        public string ErrMsg { get; set; }
        public string ResultMsg { get; set; }
    }
    // 修改密碼參數
    


    // 修改密碼參數
    public class DoEditPwdIn
    {
        public string NewUserPwd { get; set; }
        public string CheckUserPwd { get; set; }
    }


    // 修改密碼回傳
    public class DoEditPwdOut
    {
        public string ErrMsg { get; set; }
        public string ResultMsg { get; set; }
    }


    // 寄送驗證碼
    public class SendMailTokenIn
    {
        public string Account { get; set; }
    }


    // 寄送驗證碼回傳
    public class SendMailTokenOut
    {
        public string ErrMsg { get; set; }
        public string ResultMsg { get; set; }
    }


    // 重設密碼
    public class DoResetPwdIn
    {
        public string NewUserPwd { get; set; }
        public string CheckUserPwd { get; set; }
    }


    // 重設密碼回傳
    public class DoResetPwdOut
    {
        public string ErrMsg { get; set; }
        public string ResultMsg { get; set; }
    }

}

