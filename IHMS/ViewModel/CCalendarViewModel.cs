
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
        public int ScheduleId
        {
            get { return Schedule.ScheduleId; }
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
        //    get { return Schedule.Course.CoachContact.Member.Name; }
        //    set { Schedule.Course.CoachContact.Member.Name = value; }
        //}
        public int CoachId
        {
            get { return (int)Schedule.Course.CoachContact.CoachId; }
            set { Schedule.Course.CoachContact.Coach.CoachId = value; }
        }
        public string CoachName
        {
            get { return Schedule.Course.CoachContact.Coach.CoachName; }
            set { Schedule.Course.CoachContact.Coach.CoachName = value; }
        }
        public string CourseDate
        {
            get { return Schedule.CourseTime.Substring(0, 8); }
        }
        public string CourseTime1
        {
            get { return Schedule.CourseTime.Substring(8, 4); }
        }
        public int? StatusNumber
        {
            get { return Schedule.StatusNumber; }
            set { Schedule.StatusNumber = value; }
        }
        public string Status
        {
            get { return Schedule.StatusNumber == 60 ? "未完成" : "已完成"; }
        }
    }
}