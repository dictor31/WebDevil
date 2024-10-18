using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.ObjectModel;
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
        public async Task<ActionResult<ObservableCollection<Devil>>> Get()
        {
            ObservableCollection<Devil> devils = new ObservableCollection<Devil>(dataBase.Devils);
            
            return Ok(devils);
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
            Devil find = await dataBase.Devils.FirstOrDefaultAsync(s => s.Id == devil.Id);
            find.Nick = devil.Nick;
            find.Rank = devil.Rank;
            find.Year = devil.Year;

            await dataBase.SaveChangesAsync();
            return Ok(find);
        }

        [HttpDelete("DeleteDevil")]
        public async Task<ActionResult> Delete(int id)
        {
            Devil find = await dataBase.Devils.FirstOrDefaultAsync(s => s.Id == id);
            if (find == null)
            {
                return BadRequest("Пусто");
            }
            dataBase.Devils.Remove(find);
            Disposal disposal = new();
            disposal.Title = find.Nick;
            disposal.Year = find.Year;
            disposal.Date = DateTime.Now;
            dataBase.Disposals.Add(disposal);
            await dataBase.SaveChangesAsync();
            return Ok("Списано");
        }
    }
}
