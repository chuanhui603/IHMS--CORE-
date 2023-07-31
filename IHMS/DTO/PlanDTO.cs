using System.Runtime.Intrinsics.X86;

namespace IHMS.DTO
{
    public class PlanDTO
    {
        public int PlanId { get; set; }
        public int MemberId { get; set; }

        public double? Bmr { get; set; }
        public double? Tdee { get; set; }
        public string? Type { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public string? Times { get; set; }
        public int? Height { get; set; }
        public DateTime RegisterDate { get; set; }
        public double BodyPercentage { get; set; }
    }
}
