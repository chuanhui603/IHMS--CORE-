namespace IHMS.DTO
{
    public class DietDetailDTO
    {
        public int DietDetailId { get; set; }

        public int DietId { get; set; }
        public string Dname { get; set; } = null!;

        public string Type { get; set; } = null!;

        public int Calories { get; set; }

        public string? Decription { get; set; }

        public string? Img { get; set; }

        public DateTime? Registerdate { get; set; }
    }
}
