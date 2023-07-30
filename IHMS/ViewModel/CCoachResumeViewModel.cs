using IHMS.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace IHMS.ViewModel
{
    internal class CCoachResumeViewModel
    {
        private IhmsContext db;
        
        public CCoachResumeViewModel(IhmsContext context)
        {
            db = context;
        }
        public int CoachId { get; set; }
        public string CoachName { get; set; }
        public int? MemberId { get; set; }
        public int? CityId { get; set; }
        public string CoachImage { get; set; }
        public int? CoachFee { get; set; }
        public string CoachDescription { get; set; }
        public string ApplyDate { get; set; }
        public int? StatusNumber { get; set; }
        public bool? Visible { get; set; }
        public int? CourseCount { get; set; }
        public string Slogan { get; set; }
        public string CityName
        {
            get
            {
                if (CityId != null)
                    return db.Cities.FirstOrDefault(c => c.CityId == CityId).CityName;
                else
                    return null;

            }
        }
        public string ApplyDate1
        {
            get
            {
                if (!String.IsNullOrEmpty(ApplyDate))
                {
                    string fApplyDate = ApplyDate;
                    string yyyy = fApplyDate.Substring(0, 4);
                    string MM = fApplyDate.Substring(4, 2);
                    string dd = fApplyDate.Substring(6, 2);
                    string hh = fApplyDate.Substring(8, 2);
                    string mm = fApplyDate.Substring(10, 2);
                    return $"{yyyy}-{MM}-{dd} {hh}:{mm}";
                }
                else
                    return null;
            }
        }
        public string Status
        {
            get
            {
                if (StatusNumber != null)
                    return db.Statuses.FirstOrDefault(s => s.StatusNumber == StatusNumber).Status1;
                else
                    return null;
            }
        }
        public string Visible1
        {
            get
            {
                if (Visible != null)
                {
                    if ((bool)(Visible))
                        return "公開";
                    else
                        return "未公開";
                }
                else
                    return null;
            }
        }

        public IEnumerable<string> Skills
        {
            get
            {
                if (CoachId != null && CoachId != 0)
                    return db.CoachSkills.Where(cs => cs.CoachId == CoachId).Include(cs => cs.Skill).Select(cs => cs.Skill.SkillName);
                else
                    return null;
            }
        }
        public IEnumerable<string> Experiences
        {
            get
            {
                if (CoachId != null && CoachId != 0)
                    return db.CoachExperiences.Where(e => e.CoachId == CoachId).Select(e => e.Experience);
                else
                    return null;
            }
        }
        public IEnumerable<string> Licenses
        {
            get
            {
                if (CoachId != null && CoachId != 0)
                    return db.CoachLicenses.Where(l => l.CoachId == CoachId).Select(l => l.License);
                else
                    return null;
            }
        }
    }
}