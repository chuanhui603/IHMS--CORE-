using System.ComponentModel;

namespace IHMS.Models
{
    public class CTeacherWrapper
    {
        private Teacher _teacher;
        public CTeacherWrapper()
        {
            if (_teacher == null)
                _teacher = new Teacher();
        }
        public Teacher teacher
        {
            get { return _teacher; }
            set { _teacher = value; }
        }
        public int TeacherId
        {
            get { return _teacher.TTeacherId; }
            set { _teacher.TTeacherId = value; }
        }
        [DisplayName("品名")]
        public int? MemberId
        {
            get { return _teacher.TMemberId; }
            set { _teacher.TMemberId = value; }
        }

        public string? Intro
        {
            get { return _teacher.TIntro; }
            set { _teacher.TIntro = value; }
        }

        public string? Image
        {
            get { return _teacher.TImage; }
            set { _teacher.TImage = value; }
        }

        public int? Level
        {
            get { return _teacher.TLevel; }
            set { _teacher.TLevel = value; }
        }

        public double? Commission
        {
            get { return _teacher.TCommission; }
            set { _teacher.TCommission = value; }
        }
        public string? Video
        {
            get { return _teacher.TVideo; }
            set { _teacher.TVideo = value; }
        }
        public int? Condition
        {
            get { return _teacher.TCondition; }
            set { _teacher.TCondition = value; }
        }
        public string? Resume
        {
            get { return _teacher.TResume; }
            set { _teacher.TResume = value; }
        }
        public DateTime? Applytime
        {
            get { return _teacher.TApplytime; }
            set { _teacher.TApplytime = value; }
        }
        public DateTime? Confirmtime
        {
            get { return _teacher.TConfirmtime; }
            set { _teacher.TConfirmtime = value; }
        }
        public int? Score
        {
            get { return _teacher.TScore; }
            set { _teacher.TScore = value; }
        }
        public IFormFile photo { get; set; }
    }
}

