using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IHMS.Models;

public partial class Teacher
{
    [DisplayName("老師編號")]
    public int TTeacherId { get; set; }
    [DisplayName("會員編號")]
    public int? TMemberId { get; set; }
    [DisplayName("個人簡介")]
    public string? TIntro { get; set; }
    [DisplayName("個人圖片")]
    public string? TImage { get; set; }
    [DisplayName("Level")]
    public int? TLevel { get; set; }
    [DisplayName("抽成比例")]
    public double? TCommission { get; set; }
    [DisplayName("個人履歷")]
    public string? TResume { get; set; }
    [DisplayName("授課影片")]
    public string? TVideo { get; set; }
    [DisplayName("申請時間")]
    public DateTime? TApplytime { get; set; }
    [DisplayName("審核時間")]
    public DateTime? TConfirmtime { get; set; }
    [DisplayName("審核狀態")]
    public int? TCondition { get; set; }
    [DisplayName("授課評分")]
    public int? TScore { get; set; }

    public string? TReason { get; set; }
}
