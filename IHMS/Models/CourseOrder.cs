using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IHMS.Models;

public partial class CourseOrder
{
    public int CoCourseorderId { get; set; }

    [DisplayName("會員ID")]
    public int CoMemberId { get; set; }

    [DisplayName("總點數")]
    public int CoPointstotal { get; set; }

    [DisplayName("狀態")]
    public string CoState { get; set; } = null!;

    [DisplayName("已取消的原因")]

    public string? CoReason { get; set; }

    [DisplayName("下單時間")]
    public DateTime? CoCreatetime { get; set; }

    [DisplayName("更新時間")]
    public DateTime? CoUpdatetime { get; set; }

    
}

internal class RequiredIfAttribute : Attribute
{
}