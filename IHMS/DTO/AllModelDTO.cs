using IHMS.Models;

namespace IHMS.DTO
{
    public class AllModelDTO
    {
        public class Members
        {
            public int MemberId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Account { get; set; }
            public string Password { get; set; }
            public DateTime? Birthday { get; set; }
            public bool? Gender { get; set; }
            public bool? MaritalStatus { get; set; }
            public string Nickname { get; set; }
            public string AvatarImage { get; set; }
            public string ResidentialCity { get; set; }
            public int? Permission { get; set; }
            public string Occupation { get; set; }
            public string DiseaseDescription { get; set; }
            public string AllergyDescription { get; set; }
            public DateTime? LoginTime { get; set; }
        }

        public class HealthInfo
        {
            public int MemberId { get; set; }

            public decimal? Height { get; set; }

            public decimal? Weight { get; set; }

            public string? BloodPressure { get; set; }

            public decimal? BodyFatPercentage { get; set; }

            public DateTime? DateEntered { get; set; }

            public virtual Member Member { get; set; } = null!;

        }

    }
}