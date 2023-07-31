using Microsoft.AspNetCore.Http;
using IHMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prjiHealth.ViewModel
{
    public class CLoginViewModel
    {
        private Member _tmember;
        public CLoginViewModel() { _tmember = new Member(); }
        public Member Member { get { return _tmember; } set { _tmember = value; } }
        public int fMemberId { get { return _tmember.MemberId; } set { _tmember.MemberId = value; } }
        public string fAccount { get { return _tmember.Account; } set { _tmember.Account = value; } }
        [DataType(DataType.Password)]
        public string fPassword { get { return _tmember.Password; } set { _tmember.Password = value; } }
        public string fEmail { get { return _tmember.Email; } set { _tmember.Email = value; } }
        public string fPhone { get { return _tmember.Phone; } set { _tmember.Phone = value; } }
        [DisplayName("姓名")]
        public string fName { get { return _tmember.Name; } set { _tmember.Name = value; } }
        public DateTime? fBirthday { get { return _tmember.Birthday; } set { _tmember.Birthday = value; } }
        public string fNickname { get { return _tmember.Nickname; } set { _tmember.Nickname = value; } }
        public string fResidentialCity { get { return _tmember.ResidentialCity; } set { _tmember.ResidentialCity = value; } }
        public int? FPermission { get { return _tmember.Permission; } set { _tmember.Permission = value; } }
        public bool? FMaritalStatus { get { return _tmember.MaritalStatus; } set { _tmember.MaritalStatus = value; } }
        public bool? FGender { get { return _tmember.Gender; } set { _tmember.Gender = value; } }
        public string fDiseaseDescription { get { return _tmember.DiseaseDescription; } set { _tmember.DiseaseDescription = value; } }
        public string fAllergyDescription { get { return _tmember.AllergyDescription; } set { _tmember.AllergyDescription = value; } }


        public IFormFile photo { get; set; }

        public int fAuthorityId { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
        public string firstPassword { get; set; }
        public string confirmPassword { get; set; }

    }
}
