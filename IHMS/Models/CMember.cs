using System.ComponentModel;

namespace IHMS.Models
{
    public class CMember
    {
        public Member _member;

        public CMember()
        {
            if (_member == null)
                _member = new Member();
        }
        public Member member
        {
            get { return _member; }
            set { _member = value; }
        }

        public int MMemberId
        {
            get { return _member.MMemberId; }
            set { _member.MMemberId = value; }
        }
        
        public string? MName
        {
            get { return _member.MName; }
            set { _member.MName = value; }
        }

        public string? MEmail
        {
            get { return _member.MEmail; }
            set { _member.MEmail = value; }
        }

        public string? MPhone
        {
            get { return _member.MPhone; }
            set { _member.MPhone = value; }
        }

        public string? MAccount
        {
            get { return _member.MAccount; }
            set { _member.MAccount = value; }
        }

        public string? MPassword
        {
            get { return _member.MPassword; }
            set { _member.MPassword = value; }
        }

        public DateTime? MBirthday
        {
            get { return _member.MBirthday; }
            set { _member.MBirthday = value; }
        }

        public bool? MGender
        {
            get { return _member.MGender; }
            set { _member.MGender = value; }
        }

        public bool? MMaritalStatus
        {
            get { return _member.MMaritalStatus; }
            set { _member.MMaritalStatus = value; }
        }

        public string? MNickname
        {
            get { return _member.MNickname; }
            set { _member.MNickname = value; }
        }

        public string? MAvatarImage
        {
            get { return _member.MAvatarImage; }
            set { _member.MAvatarImage = value; }
        }
        public IFormFile photo { get; set; }

        public string? MResidentialCity
        {
            get { return _member.MResidentialCity; }
            set { _member.MResidentialCity = value; }
        }

        public int? MPermission
        {
            get { return _member.MPermission; }
            set { _member.MPermission = value; }
        }

        public string? MOccupation
        {
            get { return _member.MOccupation; }
            set { _member.MOccupation = value; }
        }

        public string? MDiseaseDescription
        {
            get { return _member.MDiseaseDescription; }
            set { _member.MDiseaseDescription = value; }
        }

        public string? MAllergyDescription
        {
            get { return _member.MAllergyDescription; }
            set { _member.MAllergyDescription = value; }
        }

        public DateTime? MLoginTime
        {
            get { return _member.MLoginTime; }
            set { _member.MLoginTime = value; }
        }        
    }
}
