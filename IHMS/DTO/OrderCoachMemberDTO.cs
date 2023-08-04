namespace IHMS.DTO
{
    public class OrderCoachMemberDTO
    {

        public string MemberName { get; set; } = null!;
        public string Ordernumber { get; set; } = null!;

        public int Pointstotal { get; set; }

        public string State { get; set; } = null!;

        public string? CourseTime { get; set; }

        public DateTime? Createtime { get; set; }

        public string? CourseName { get; set; }

        public int? CoachFee { get; set; }

        public string? CoachName { get; set; }

    }
}
