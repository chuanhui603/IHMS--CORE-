namespace IHMS.DTO
{
    public class SportDetailDTO
    {
        
        public int SportDetailId { get; set; }

        public int SportId { get; set; }

        public string Sname { get; set; } = null!;

        public TimeSpan Sporttime { get; set; }

        public int? Frequency { get; set; }

        public string Type { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime Registerdate { get; set; }

    }
}