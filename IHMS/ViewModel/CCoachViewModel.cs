using IHMS.Models;

namespace IHMS.ViewModel
{
    public class CCoachViewModel
    {
        public Coach Coach;
        public Member Member;
        public Course Course;
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
            get { return Coach.MemberId; }
            set { Coach.MemberId = (int)value; }
        }
        public string Name
        {
            get { return Member.Name; }
            set { Member.Name = value; }
        }
        public bool? Gender
        {
            get { return Member.Gender; }
            set { Member.Gender = value; }
        }
        public string City
        {
            get { return Member.ResidentialCity; }
            set { Member.ResidentialCity = value; }
        }
        public string Intro
        {
            get { return Coach.Intro; }
            set { Coach.Intro = value; }
        }
        public string Image
        {
            get { return Coach.Image; }
            set { Coach.Image = value; }
        }
        
        public string Type
        {
            get { return Coach.Type; }
            set { Coach.Type = value; }
        }
        public double? Commission
        {
            get { return Coach.Commission; }
            set { Coach.Commission = value; }
        }
        public DateTime ApplyDate
        {
            get { return Coach.Applytime; }
            set { Coach.Applytime = value; }
        }
        public DateTime ConfirmDate
        {
            get { return Coach.Confirmtime; }
            set { Coach.Confirmtime = value; }
        }
        public int? Rank
        {
            get { return Coach.Rank; }
            set { Coach.Rank = value; }
        }
        public string Resume
        {
            get { return Coach.Resume; }
            set { Coach.Resume = value; }
        }
        public string Video
        {
            get { return Coach.Video; }
            set { Coach.Video = value; }
        }
        public int Condition
        {
            get { return Coach.Condition; }
            set { Coach.Condition= value; }
        }
        public string Reason
        {
            get { return Coach.Reason; }
            set { Coach.Reason = value; }
        }
        
    }
}
