using MeetingWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebChat.Controllers
{
    [Route("api")]
    [ApiController]
    public class SeedController : ControllerBase
    {
       
        private readonly ApplicationContext ctx;

        public SeedController(ApplicationContext ctx)
        {
            
            this.ctx = ctx;
        }

        [HttpGet("seed")]
        public async Task<IActionResult> CreateDb()
        {
            await ctx.Database.MigrateAsync();
            return Ok();
        }
        
    }
}
