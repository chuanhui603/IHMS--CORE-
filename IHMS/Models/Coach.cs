using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IHMS.Models;

public partial class Coach
{
    [DisplayName("教練編號")]
    public int CoachId { get; set; }
    [DisplayName("會員編號")]
    public int MemberId { get; set; }
    [DisplayName("個人簡介")]
    public string? Intro { get; set; }
    [DisplayName("個人圖片")]
    public string Image { get; set; } = null!;

    public int? Rank { get; set; }
    [DisplayName("抽成比例")]
    public double? Commission { get; set; }
    [DisplayName("個人履歷")]
    public string Resume { get; set; } = null!;
    [DisplayName("授課影片")]
    public string? Video { get; set; }
    [DisplayName("審查狀態")]
    public int Condition { get; set; }
    [DisplayName("緣由")]
    public string? Reason { get; set; }
    [DisplayName("申請時間")]
    public DateTime Applytime { get; set; }
    [DisplayName("審查時間")]
    public DateTime Confirmtime { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Member Member { get; set; } = null!;
}
