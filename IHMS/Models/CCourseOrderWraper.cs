using System.ComponentModel;

namespace IHMS.Models
{
    public class CCourseOrderWraper
    {
        private Order _Order;
        public CCourseOrderWraper()
        {
            if (_Order == null)
            {
                _Order = new Order();
            }
        }

        public Order CourseOrder
        {
            get { return _Order; }
            set { _Order = value; }
        }
        public int CoCourseorderId
        {
            get { return _Order.OrderId; }
            set { _Order.OrderId = value; }
        }
        [DisplayName("會員ID")]
        public int CoMemberId
        {
            get { return _Order.MemberId; }
            set { _Order.MemberId = value; }
        }
        [DisplayName("總點數")]
        public int CoPointstotal
        {
            get { return _Order.Pointstotal; }
            set { _Order.Pointstotal = value; }
        }
        [DisplayName("狀態")]
        public string CoState { get; set; } = null!;

        [DisplayName("原因")]
        public string? CoReason
        {
            get { return _Order.Reason; }
            set { _Order.Reason = value; }
        }
        [DisplayName("下單時間")]
        public DateTime? CoCreatetime { get; set; }
        [DisplayName("更新時間")]
        public DateTime? CoUpdatetime { get; set; }
    }
}
