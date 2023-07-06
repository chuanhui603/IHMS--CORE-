using IHMS.Models;

namespace IHMS.ViewModel
{
    public class PPlanViewModel
    {
        public int PlanId { get; set; }

        public int MemberId { get; set; }

        public string Pname { get; set; } = null!;

        public int Weight { get; set; }

        public double BodyPercentage { get; set; }

        public DateTime RegisterDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
