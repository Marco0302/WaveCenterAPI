using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaveCenter.Model;

namespace WaveCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocaisController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public LocaisController(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Local>>> GetLocais()
        {
            if (_context.Locais == null)
            {
                return NotFound();
            }

            var clientes = await _context.Locais.ToListAsync();

            return Ok(clientes);
        }

    }
}
