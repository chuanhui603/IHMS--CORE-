using IHMS.Models;

namespace IHMS.DTO
{
    public class DeletePlanDTO
    {
        public string[] deleteDataSet = { "diet", "sport", "water" };
        public IQueryable<Diet> diet { get; set; }
        public IQueryable<Sport> sport { get; set; }
        public IQueryable<Water> water { get; set; }
    }
}
