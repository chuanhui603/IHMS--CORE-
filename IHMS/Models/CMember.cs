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
            get { return _member.MemberId; }
            set { _member.MemberId = value; }
        }
        
        public string? MName
        {
            get { return _member.Name; }
            set { _member.Name = value; }
        }

        public string? MEmail
        {
            get { return _member.Email; }
            set { _member.Email = value; }
        }

        public string? MPhone
        {
            get { return _member.Phone; }
            set { _member.Phone = value; }
        }

        public string? MAccount
        {
            get { return _member.Account; }
            set { _member.Account = value; }
        }

        public string? MPassword
        {
            get { return _member.Password; }
            set { _member.Password = value; }
        }

        public DateTime? MBirthday
        {
            get { return _member.Birthday; }
            set { _member.Birthday = value; }
        }

        public bool? MGender
        {
            get { return _member.Gender; }
            set { _member.Gender = value; }
        }

        public bool? MMaritalStatus
        {
            get { return _member.MaritalStatus; }
            set { _member.MaritalStatus = value; }
        }

        public string? MNickname
        {
            get { return _member.Nickname; }
            set { _member.Nickname = value; }
        }

        public string? MAvatarImage
        {
            get { return _member.AvatarImage; }
            set { _member.AvatarImage = value; }
        }
        public IFormFile photo { get; set; }

        public string? MResidentialCity
        {
            get { return _member.ResidentialCity; }
            set { _member.ResidentialCity = value; }
        }

        public int? MPermission
        {
            get { return _member.Permission; }
            set { _member.Permission = value; }
        }

        public string? MOccupation
        {
            get { return _member.Occupation; }
            set { _member.Occupation = value; }
        }

        public string? MDiseaseDescription
        {
            get { return _member.DiseaseDescription; }
            set { _member.DiseaseDescription = value; }
        }

        public string? MAllergyDescription
        {
            get { return _member.AllergyDescription; }
            set { _member.AllergyDescription = value; }
        }

        public DateTime? MLoginTime
        {
            get { return _member.LoginTime; }
            set { _member.LoginTime = value; }
        }        
    }
}
