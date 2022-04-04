using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using SPFxWorkshop.CouponAPI.Models;

namespace SPFxWorkshop.CouponAPI.Controllers
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
        public async Task<ActionResult<IEnumerable<CouponCode>>> GetCouponCode()
        {
            // Verify the token scope
            HttpContext.VerifyUserHasAnyAcceptedScope(new string[] { "Coupon.Read", "Coupon.ReadWrite", "Coupon.FullControl" });
            //return await _context.Coupons.Where(c => String.Compare(c.Owner, HttpContext.User.Identity.Name, true) == 0).ToListAsync();
            var result = from c in _context.CouponCode
                         where c.Owner == HttpContext.User.Identity.Name
                         select c;
            return await result.ToListAsync();
            // return await _context.CouponCode.ToListAsync();
        }

        // GET: api/CouponCode/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CouponCode>> GetCouponCode(int id)
        {
            var couponCode = await _context.CouponCode.FindAsync(id);

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
            _context.CouponCode.Add(couponCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCouponCode", new { id = couponCode.Id }, couponCode);
        }

        // DELETE: api/CouponCode/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCouponCode(int id)
        {
            var couponCode = await _context.CouponCode.FindAsync(id);
            if (couponCode == null)
            {
                return NotFound();
            }

            _context.CouponCode.Remove(couponCode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CouponCodeExists(int id)
        {
            return _context.CouponCode.Any(e => e.Id == id);
        }
    }
}
