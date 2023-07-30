using IHMS.Models;

namespace IHMS.DTO
{
    public class HealthInfoDTO : AllModelDTO
    {
        public int MemberId { get; set; }

        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        public string? BloodPressure { get; set; }

        public decimal? BodyFatPercentage { get; set; }

        public DateTime? DateEntered { get; set; }        

        public MembersDTO Member { get; set; }
    }
}
