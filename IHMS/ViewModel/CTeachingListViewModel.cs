using IHMS.Models;
using System.ComponentModel;

namespace IHMS.ViewModel
{

    public class CTeachingListViewModel
    {
        public Course Course;
        public CTeachingListViewModel()
        {
            Course = new Course();
        }
        public CTeachingListViewModel(Course c)
        {
            Course = c;
        }
        static public List<CTeachingListViewModel> CourseList(List<Course> courseList)
        {
            List<CTeachingListViewModel> list = new List<CTeachingListViewModel>();
            foreach (var c in courseList)
            {
                CTeachingListViewModel vModel = new CTeachingListViewModel(c);
                list.Add(vModel);
            }
            return list;
        }
        public int CourseId
        {
            get { return Course.CourseId; }
        }
        public int? CoachContactId
        {
            get { return Course.CoachContactId; }
            set { Course.CoachContactId = value; }
        }
        public int? MemberId
        {
            get { return Course.CoachContact.MemberId; }
            set { Course.CoachContact.MemberId = (int)value; }
        }
        //[DisplayName("學員")]
        //public string MemberName
        //{
        //    get { return Course.CoachContact.Member; }
        //    set { Course.CoachContact.FMember.FMemberName = value; }
        //}
        //public string MemberPhone
        //{
        //    get { return Course.FCoachContact.FMember.FPhone; }
        //    set { Course.FCoachContact.FMember.FPhone = value; }
        //}
        [DisplayName("堂數")]
        public int CourseTotal
        {
            get { return Course.CourseTotal; }
            set { Course.CourseTotal = value; }
        }
        [DisplayName("課程名稱")]
        public string CourseName
        {
            get { return Course.CourseName; }
            set { Course.CourseName = value; }
        }
        [DisplayName("預設上課時段")]
        public int? AvailableTimeNum
        {
            get { return Course.AvailableTimeNum; }
            set { Course.AvailableTimeNum = value; }
        }
        public int? StatusNumber
        {
            get { return Course.StatusNumber; }
            set { Course.StatusNumber = value; }
        }
        [DisplayName("狀態")]
        public string Status
        {
            get { return Course.StatusNumber == 55 ? "進行中" : "已結束"; }
        }
        public bool? Visible
        {
            get { return Course.Visible; }
            set { Course.Visible = value; }
        }

        //排課紀錄
        public List<CCalendarViewModel> Reservations
        {
            get
            {
                var reservations = Course.Schedules.AsEnumerable();
                List<CCalendarViewModel> list = new List<CCalendarViewModel>();
                foreach (var s in reservations)
                {
                    CCalendarViewModel vModel = new CCalendarViewModel(s)
                    {
                        Schedule = s
                    };
                    list.Add(vModel);
                }
                return list;
            }
        }
    }
}