using System.ComponentModel;

namespace IHMS.Models
{
    public class CCoachWrapper
    {
        private Coach _coach;
        public CCoachWrapper()
        {
            if (_coach == null)
                _coach = new Coach();
        }
        public Coach coach
        {
            get { return _coach; }
            set { _coach = value; }
        }
        public int CoachId
        {
            get { return _coach.CoachId; }
            set { _coach.CoachId = value; }
        }
        [DisplayName("品名")]
        public int MemberId
        {
            get { return _coach.MemberId; }
            set { _coach.MemberId = value; }
        }

        public string? Intro
        {
            get { return _coach.Intro; }
            set { _coach.Intro = value; }
        }

        public string? Image
        {
            get { return _coach.Image; }
            set { _coach.Image = value; }
        }

        public int? Rank
        {
            get { return _coach.Rank; }
            set { _coach.Rank = value; }
        }

        public double? Commission
        {
            get { return _coach.Commission; }
            set { _coach.Commission = value; }
        }
        public string? Video
        {
            get { return _coach.Video; }
            set { _coach.Video = value; }
        }
        public int Condition
        {
            get { return _coach.Condition; }
            set { _coach.Condition = value; }
        }
        public string? Resume
        {
            get { return _coach.Resume; }
            set { _coach.Resume = value; }
        }
        public DateTime Applytime
        {
            get { return _coach.Applytime; }
            set { _coach.Applytime = value; }
        }
        public DateTime Confirmtime
        {
            get { return _coach.Confirmtime; }
            set { _coach.Confirmtime = value; }
        }

        public IFormFile photo { get; set; }
    }
}

