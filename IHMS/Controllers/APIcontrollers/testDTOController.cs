using IHMS.Models;
using IHMS.ViewModel.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IHMS.Controllers.APIcontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testDTOController : ControllerBase
    {
        // GET: api/<testDTOController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<testDTOController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<testDTOController>
        [HttpPost]
        public string Post([FromBody] string value)
        {
            Random random = new Random();
            int bankNumber = random.Next(10000000, 99999999);


            PointRecord Pointre = new PointRecord
            {                
                BankNumber = bankNumber,                
            };
            return "123";
        }

        // PUT api/<testDTOController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<testDTOController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
