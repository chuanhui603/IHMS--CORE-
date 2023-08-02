
using IHMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace IHMS.ViewModel
{
    public class CCalendarViewModel
    {
        public Schedule Schedule;
        public CCalendarViewModel(Schedule s)
        {
            Schedule = s;
        }
        static public List<CCalendarViewModel> ScheduleList(List<Schedule> Schedule)
        {
            List<CCalendarViewModel> list = new List<CCalendarViewModel>();

            foreach (var r in Schedule)
            {
                CCalendarViewModel vModel = new CCalendarViewModel(r);
                list.Add(vModel);
            }
            return list;
        }
        
        public int? CourseId
        {
            get { return Schedule.CourseId; }
        }
        public string CourseTime
        {
            get { return Schedule.CourseTime; }
            set { Schedule.CourseTime = value; }
        }
        public int MemberId
        {
            get { return (int)Schedule.Course.CoachContact.MemberId; }
            set { Schedule.Course.CoachContact.MemberId = value; }
        }
        //public string MemberName
        //{
        //    get { return Schedule.Course.CoachContact.MemberId. MemberName; }
        //    set { Schedule.Course.CoachContact.Member.FMemberName = value; }
        //}
        //public int CoachId
        //{
        //    get { return (int)Schedule.Course.CoachContact.CoachId; }
        //    set { Schedule.Course.CoachContact.Coach.CoachId = value; }
        //}
        //public string CoachName
        //{
        //    get { return Schedule.FCourse.FCoachContact.FCoach.FCoachName; }
        //    set { Schedule.FCourse.FCoachContact.FCoach.FCoachName = value; }
        //}
        //public string CourseDate
        //{
        //    get { return Schedule.FCourseTime.Substring(0, 8); }
        //}
        //public string CourseTime
        //{
        //    get { return Schedule.FCourseTime.Substring(8, 4); }
        //}
        //public int? FStatusNumber
        //{
        //    get { return Schedule.FStatusNumber; }
        //    set { Schedule.FStatusNumber = value; }
        //}
        //public string Status
        //{
        //    get { return Schedule.FStatusNumber == 60 ? "未完成" : "已完成"; }
        //}
    }
}