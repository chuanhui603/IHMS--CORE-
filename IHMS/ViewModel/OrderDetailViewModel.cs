using System.ComponentModel.DataAnnotations;

namespace IHMS.ViewModel
{
    public class OrderDetailViewModel
    {
        [Display(Name = "訂單明細ID")]

        public int OrderdetailId { get; set; }
        [Display(Name = "訂單編號")]
        public string Ordernumber { get; set; } = null!;

        [Display(Name = "課程ID")]

        public int ScheduleId { get; set; }


        [Display(Name = "訂單ID")]
        public int OrderId { get; set; }

        [Display(Name = "會員名字")]
        public string MemberName { get; set; }
        [Display(Name = "會員ID")]
        public int MemberId { get; set; }

       [Display(Name = "點數總計")]
        public int Pointstotal { get; set; }

        [Display(Name = "狀態")]

        public string State { get; set; } = null!;
        [Required(ErrorMessage = "選擇已取消時，請填寫原因")]

        [Display(Name = "取消原因")]
        public string? Reason { get; set; }

        [Display(Name = "下單時間")]
        public DateTime? Createtime { get; set; }

        [Display(Name = "更新時間")]
        public DateTime? Updatetime { get; set; }
    }
}
