using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using WebDevil.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDevil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RackController : ControllerBase
    {
        DB._666Context dataBase;
        public RackController(DB._666Context database) 
        {
            dataBase = database;
        }

        [HttpGet("GetRacks")]
        public async Task<ActionResult<ObservableCollection<Rack>>> Get()
        {
            ObservableCollection<Rack> racks = new ObservableCollection<Rack>(dataBase.Racks);
            return Ok(racks);
        }

        [HttpPost("AddRack")]
        public async Task<ActionResult> Post(Rack rack)
        {
            dataBase.Racks.Add(rack);
            await dataBase.SaveChangesAsync();
            return Ok("Мебель создана");
        }

        [HttpPut("PutRack")]
        public async Task<ActionResult> Put(Rack rack)
        {
            Rack find = await dataBase.Racks.FirstOrDefaultAsync(s => s.Id == rack.Id);
            find.Title = rack.Title;
            find.YearBuy = rack.YearBuy;
            find.UseCount = rack.UseCount;
            find.CurrentCount = rack.CurrentCount;
            find.IdDevil = rack.IdDevil;

            await dataBase.SaveChangesAsync();
            return Ok("Мебель изменена");
        }
    }
}
