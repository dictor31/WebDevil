using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using WebDevil.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDevil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevilController : ControllerBase
    {
        readonly DB._666Context dataBase;

        public DevilController(DB._666Context database)
        {
            dataBase = database;
        }

        [HttpGet("GetDevils")]
        public async Task<ActionResult<Devil>> Get()
        {
            return Ok();
        }

        [HttpPost("AddDevil")]
        public async Task<ActionResult> Post(Devil devil)
        {
            dataBase.Devils.Add(devil);
            await dataBase.SaveChangesAsync();
            return Ok("Дьявол создан");
        }

        [HttpPut("PutDevil")]
        public async Task<ActionResult> Put(Devil devil)
        {

            return Ok();
        }

        [HttpDelete("DeleteDevil")]
        public async Task<ActionResult> Delete(Devil devil)
        {

            return Ok(devil);
        }
    }
}
