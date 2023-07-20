namespace IHMS.DTO
{
    public class PlansSideBarDTO
    {
        public int PlanId { get; set; }

        public string Pname { get; set; } = null!;

        public DateTime RegisterDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
