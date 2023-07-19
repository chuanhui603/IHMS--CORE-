using IHMS.Models;

namespace IHMS.DTO
{
    public class DeletePlanDTO
    {
        public string[] deleteDataSet = { "diet", "sport", "water" };
        public List<Diet> diet { get; set; }
        public List<Sport> sport { get; set; }
        public List<Water> water { get; set; }
    }
}
