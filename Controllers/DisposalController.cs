using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using WebDevil.Model;

namespace WebDevil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisposalController : ControllerBase
    {
        DB._666Context dataBase;
        public DisposalController(DB._666Context database) 
        {
            dataBase = database;
        }

        [HttpGet("GetDisposals")]
        public async Task<ActionResult<ObservableCollection<Disposal>>> Get()
        {
            ObservableCollection<Disposal> disposal = new ObservableCollection<Disposal>(dataBase.Disposals);

            return Ok(disposal);
        }

        [HttpPost("PostDisposal")]
        public async Task<ActionResult> PostDisposal()
        {
            return Ok();
        }
    }
}
