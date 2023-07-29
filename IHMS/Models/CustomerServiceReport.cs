using System;
namespace IHMS.Models
{
    public class CustomerServiceReport
    {
        public int customer_service_id { get; set; }
        public int member_id { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string Reply { get; set; }
        public DateTime? created_time { get; set; }
        public DateTime? updated_time { get; set; }
    }
}
