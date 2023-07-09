using IHMS.Models;
using System.ComponentModel;

namespace IHMS.ViewModel
{
    public class PPlanListViewModel
    {

       
        public int? PlanId { get; set; }
        [DisplayName("開始日期")]
        public DateTime? Registerdate { get; set; }
        [DisplayName("結束日期")]
        public DateTime? EndDate { get; set; }
        [DisplayName("會員姓名")]
        public string? Name { get; set; }

    }
}
