using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPFxWorkshop.CouponAPI3.Models;

namespace SPFxWorkshop.CouponAPI3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponCodeController : ControllerBase
    {
        private readonly CouponContext _context;

        public CouponCodeController(CouponContext context)
        {
            _context = context;
        }

        // GET: api/CouponCode
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CouponCode>>> GetCouponCodes()
        {
          if (_context.CouponCodes == null)
          {
              return NotFound();
          }
            return await _context.CouponCodes.ToListAsync();
        }

        // GET: api/CouponCode/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CouponCode>> GetCouponCode(int id)
        {
          if (_context.CouponCodes == null)
          {
              return NotFound();
          }
            var couponCode = await _context.CouponCodes.FindAsync(id);

            if (couponCode == null)
            {
                return NotFound();
            }

            return couponCode;
        }

        // PUT: api/CouponCode/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCouponCode(int id, CouponCode couponCode)
        {
            if (id != couponCode.Id)
            {
                return BadRequest();
            }

            _context.Entry(couponCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouponCodeExists(id))
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

        // POST: api/CouponCode
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CouponCode>> PostCouponCode(CouponCode couponCode)
        {
          if (_context.CouponCodes == null)
          {
              return Problem("Entity set 'CouponContext.CouponCodes'  is null.");
          }
            _context.CouponCodes.Add(couponCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCouponCode", new { id = couponCode.Id }, couponCode);
        }

        // DELETE: api/CouponCode/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCouponCode(int id)
        {
            if (_context.CouponCodes == null)
            {
                return NotFound();
            }
            var couponCode = await _context.CouponCodes.FindAsync(id);
            if (couponCode == null)
            {
                return NotFound();
            }

            _context.CouponCodes.Remove(couponCode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CouponCodeExists(int id)
        {
            return (_context.CouponCodes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
