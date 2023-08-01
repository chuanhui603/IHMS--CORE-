namespace IHMS.ViewModel
{
    public class GoogleLoginCallbackViewModel
    {
        public string Email { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string PictureUrl { get; set; }
        public string Gender { get; set; } // 新增性別屬性
        public DateTime? BirthDate { get; set; } // 新增生日屬性，使用 DateTime? 來表示可為空值
    }


}