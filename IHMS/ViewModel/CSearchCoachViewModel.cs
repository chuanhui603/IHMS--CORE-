namespace IHMS.ViewModel
{
    public class CSearchCoachViewModel
    {
        public int? CityId { get; set; } //0
        public int? SkillId { get; set; } //0
        public string KeyWord { get; set; } //null
        public int? Sort { get; set; } //1:DESC 0:ORDERBY
        public int? StatusNum { get; set; } //0
    }
}