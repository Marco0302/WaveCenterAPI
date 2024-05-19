using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaveCenter.Model;

namespace WaveCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VouchersController : ControllerBase
    {
        private readonly WaveCenterContext _context;

        public VouchersController(WaveCenterContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Voucher>>> GetVouchers()
        {
            if (_context.Vouchers == null)
            {
                return NotFound();
            }

            var vouchers = await _context.Vouchers
                                            .Select(v => new Voucher
                                            {
                                                Id = v.Id,
                                                Codigo = v.Codigo,
                                                Descricao = v.Descricao,
                                                DataInicio = v.DataInicio,
                                                DataFim = v.DataFim
                                            })
                                            .ToListAsync();

            return Ok(vouchers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Voucher>> GetVoucher(int id)
        {
            if (_context.Vouchers == null)
            {
                return NotFound();
            }
            var voucher = await _context.Vouchers.FindAsync(id);

            if (voucher == null)
            {
                return NotFound();
            }

            return voucher;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoucher(int id, Voucher voucher)
        {
            if (id != voucher.Id)
            {
                return BadRequest();
            }

            _context.Entry(voucher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoucherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Voucher>> PostVoucher(Voucher voucher)
        {
            if (_context.Vouchers == null)
            {
                return Problem("Entity is null.");
            }
            _context.Vouchers.Add(voucher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoucher", new { id = voucher.Id }, voucher);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucher(int id)
        {
            if (_context.Vouchers == null)
            {
                return NotFound();
            }
            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }

            _context.Vouchers.Remove(voucher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoucherExists(int id)
        {
            return (_context.Vouchers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
