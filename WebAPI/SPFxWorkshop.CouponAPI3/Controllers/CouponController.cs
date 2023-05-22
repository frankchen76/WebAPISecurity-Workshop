using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using SPFxWorkshop.CouponAPI3.Models;

namespace SPFxWorkshop.CouponAPI3.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly CouponContext _context;

        public CouponController(CouponContext context)
        {
            _context = context;
        }

        // GET: api/Coupon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetCoupons()
        {
            List<Coupon> ret = null;
            ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
            string oid = principal.FindFirst("oid")?.Value;
            string sub = principal.FindFirst("sub")?.Value;
            bool isAppOnly = oid != null && sub != null && oid == sub;

            // Verify the token scope/role
            if (isAppOnly) 
            {
                HttpContext.ValidateAppRole(new string[] { "Coupon.All.FullControl"});
                //return await _context.Coupons.Where(c => String.Compare(c.Owner, HttpContext.User.Identity.Name, true) == 0).ToListAsync();
                var result = from c in _context.Coupons
                             select c;
                ret = await result.ToListAsync();
            }
            else
            {
                HttpContext.VerifyUserHasAnyAcceptedScope(new string[] { "Coupon.Read", "Coupon.ReadWrite", "Coupon.FullControl" });
                //return await _context.Coupons.Where(c => String.Compare(c.Owner, HttpContext.User.Identity.Name, true) == 0).ToListAsync();
                var result = from c in _context.Coupons
                             where c.Owner == principal.FindFirstValue("upn")
                             select c;
                ret = await result.ToListAsync();

            }
            return ret;
        }

        // GET: api/Coupon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coupon>> GetCoupon(int id)
        {
            ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;

            // Verify the token scope
            HttpContext.VerifyUserHasAnyAcceptedScope(new string[] { "Coupon.ReadWrite", "Coupon.FullControl" });

            // var coupon = await _context.Coupons.FindAsync(id);
            var couponQuery = from c in _context.Coupons
                              where c.Id == id && c.Owner == principal.FindFirstValue("upn")
                              select c;
            var coupon = await couponQuery.FirstOrDefaultAsync();

            if (coupon == null)
            {
                return NotFound();
            }

            return coupon;
        }

        // PUT: api/Coupon/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoupon(int id, Coupon coupon)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(new string[] { "Coupon.ReadWrite", "Coupon.FullControl" });

            if (id != coupon.Id)
            {
                return BadRequest();
            }

            _context.Entry(coupon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CouponExists(id))
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

        // POST: api/Coupon
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coupon>> PostCoupon(Coupon coupon)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(new string[] { "Coupon.ReadWrite", "Coupon.FullControl" });
            coupon.Owner = HttpContext.User.Identity.Name;
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoupon", new { id = coupon.Id }, coupon);
        }

        // DELETE: api/Coupon/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;

            HttpContext.VerifyUserHasAnyAcceptedScope(new string[] { "Coupon.FullControl" });
            // var coupon = await _context.Coupons.FindAsync(id);
            var couponQuery = from c in _context.Coupons
                              where c.Id == id && c.Owner == principal.FindFirstValue("upn")
                              select c;
            var coupon = await couponQuery.FirstOrDefaultAsync();

            if (coupon == null)
            {
                return NotFound();
            }

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CouponExists(int id)
        {
            return _context.Coupons.Any(e => e.Id == id);
        }
    }
}
