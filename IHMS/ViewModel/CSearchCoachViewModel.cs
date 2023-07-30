namespace IHMS.ViewModel
{
    public class CSearchCoachViewModel
    {
        public int? MemberId { get; set; }
        public string KeyWord { get; set; } //null
        public int? Sort { get; set; } //1:DESC 0:ORDERBY
        public int? Condition{ get; set; } //0
    }
}