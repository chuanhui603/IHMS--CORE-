using IHMS.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
namespace IHMS.ViewModel
{
    public class CCoachViewModel
    {
        public Coach Coach;
        public Member Member;
        public City City;
        public CCoachViewModel()
        {
            Coach = new Coach();
        }
        public CCoachViewModel(Coach c)
        {
            Coach = c;
        }
        static public List<CCoachViewModel> CoachList(List<Coach> coachList)
        {
            List<CCoachViewModel> list = new List<CCoachViewModel>();
            foreach (var c in coachList)
            {
                CCoachViewModel vModel = new CCoachViewModel(c);
                list.Add(vModel);
            }
            return list;
        }
        public int CoachId
        {
            get { return Coach.CoachId; }
        }
        public int? MemberId
        {
            get { return Coach.CoachId; }
            set { Coach.CoachId = (int)value; }
        }
        public bool? Gender
        {
            get { return Coach.Member.Gender; }
            set { Coach.Member.Gender = value; }
        }
        public string CoachName
        {
            get { return Coach.CoachName; }
            set { Coach.CoachName = value; }
        }
        public int? CityId
        {
            get { return Coach.CityId; }
            set { Coach.CityId = value; }
        }
        //public string CityName
        //{
        //    get { return Coach.City.CityName; }
        //    set { Coach.City.CityName = value; }
        //}
        public string CoachImage
        {
            get { return Coach.CoachImage; }
            set { Coach.CoachImage = value; }
        }
        public int? CoachFee
        {
            get { return Coach.CoachFee; }
            set { Coach.CoachFee = value; }
        }
        public string ApplyDate
        {
            get { return Coach.ApplyDate; }
            set { Coach.ApplyDate = value; }
        }
        public int? StatusNumber
        {
            get { return Coach.StatusNumber; }
            set { Coach.StatusNumber = value; }
        }
        public bool? Visible
        {
            get { return Coach.Visible; }
            set { Coach.Visible = value; }
        }
        public int? CourseCount
        {
            get { return Coach.CourseCount; }
            set { Coach.CourseCount = value; }
        }
        public string CoachDescription
        {
            get { return Coach.CoachDescription; }
            set { Coach.CoachDescription = value; }
        }
        public string Slogan
        {
            get { return Coach.Slogan; }
            set { Coach.Slogan = value; }
        }
        public int RateCount
        {
            get { return Coach.CoachRates.Count(); }
        }
    }
}
