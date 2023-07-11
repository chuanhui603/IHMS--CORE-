using System.ComponentModel.DataAnnotations;

namespace IHMS.ViewModel
{
    public class PointRecordViewModel
    {
        [Display(Name = "點數ID")]
        public int PointrecordId { get; set; }

        [Display(Name = "數量")]
        public int Count { get; set; }

        [Display(Name = "會員ID")]
        public int MemberId { get; set; }

        [Display(Name = "銀行亂碼")]
        public int BankNumber { get; set; }

        [Display(Name = "購買時間")]
        public DateTime? Createtime { get; set; }

        [Display(Name = "更新時間")]
        public DateTime? Updatetime { get; set; }
    }
}
