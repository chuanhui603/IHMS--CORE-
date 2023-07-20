using System.ComponentModel;

namespace IHMS.Models
{
    public class CCourseOrderWraper
    {
        private CourseOrder _CourseOrder;
        public CCourseOrderWraper()
        {
            if (_CourseOrder == null)
            {
                _CourseOrder = new CourseOrder();
            }
        }

        public CourseOrder CourseOrder
        {
            get { return _CourseOrder; }
            set { _CourseOrder = value; }
        }
        public int CoCourseorderId
        {
            get { return _CourseOrder.CoCourseorderId; }
            set { _CourseOrder.CoCourseorderId = value; }
        }
        [DisplayName("會員ID")]
        public int CoMemberId
        {
            get { return _CourseOrder.CoMemberId; }
            set { _CourseOrder.CoMemberId = value; }
        }
        [DisplayName("總點數")]
        public int CoPointstotal
        {
            get { return _CourseOrder.CoPointstotal; }
            set { _CourseOrder.CoPointstotal = value; }
        }
        [DisplayName("狀態")]
        public string CoState { get; set; } = null!;

        [DisplayName("原因")]
        public string? CoReason
        {
            get { return _CourseOrder.CoReason; }
            set { _CourseOrder.CoReason = value; }
        }
        [DisplayName("下單時間")]
        public DateTime? CoCreatetime { get; set; }
        [DisplayName("更新時間")]
        public DateTime? CoUpdatetime { get; set; }
    }
}
