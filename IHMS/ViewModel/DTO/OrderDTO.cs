namespace IHMS.ViewModel.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public string Ordernumber { get; set; } = null!;
        

        public int Pointstotal { get; set; }

        public string State { get; set; } = null!;

        public string? Reason { get; set; }

        public DateTime? Createtime { get; set; }
    }
}
