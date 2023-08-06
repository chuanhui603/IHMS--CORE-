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

        public int MemberId
        {
            get { return _member.MemberId; }
            set { _member.MemberId = value; }
        }
        
        public string? Name
        {
            get { return _member.Name; }
            set { _member.Name = value; }
        }

        public string? Email
        {
            get { return _member.Email; }
            set { _member.Email = value; }
        }

        public string? Phone
        {
            get { return _member.Phone; }
            set { _member.Phone = value; }
        }

        public string? Account
        {
            get { return _member.Account; }
            set { _member.Account = value; }
        }

        public string? Password
        {
            get { return _member.Password; }
            set { _member.Password = value; }
        }

        public DateTime? Birthday
        {
            get { return _member.Birthday; }
            set { _member.Birthday = value; }
        }

        public bool? Gender
        {
            get { return _member.Gender; }
            set { _member.Gender = value; }
        }

        public bool? MaritalStatus
        {
            get { return _member.MaritalStatus; }
            set { _member.MaritalStatus = value; }
        }

        public string? Nickname
        {
            get { return _member.Nickname; }
            set { _member.Nickname = value; }
        }

        public string? AvatarImage
        {
            get { return _member.AvatarImage; }
            set { _member.AvatarImage = value; }
        }
        public IFormFile photo { get; set; }

        public string? ResidentialCity
        {
            get { return _member.ResidentialCity; }
            set { _member.ResidentialCity = value; }
        }

        public int? Permission
        {
            get { return _member.Permission; }
            set { _member.Permission = value; }
        }

        public string? Occupation
        {
            get { return _member.Occupation; }
            set { _member.Occupation = value; }
        }

        public string? DiseaseDescription
        {
            get { return _member.DiseaseDescription; }
            set { _member.DiseaseDescription = value; }
        }

        public string? AllergyDescription
        {
            get { return _member.AllergyDescription; }
            set { _member.AllergyDescription = value; }
        }

        public DateTime? LoginTime
        {
            get { return _member.LoginTime; }
            set { _member.LoginTime = value; }
        }
        public string ErrMsg { get; set; }
        public string firstPassword { get; set; }
        public string confirmPassword { get; set; }
    }
}
