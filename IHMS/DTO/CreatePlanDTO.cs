using System.Runtime.Intrinsics.X86;

namespace IHMS.DTO
{
    public class CreatePlanDTO
    {
        public string? pname { get; set; }
        public int weight { get; set; }
        public double Bmi { get; set; }
        public DateTime endDate { get; set; }
        public string? description { get; set; }
    }
}
