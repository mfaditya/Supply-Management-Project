using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplyManagement.API.Data;
using SupplyManagement.API.Models;

namespace SupplyManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorControllers : ControllerBase
    {
        private readonly MyContext _context;
        public VendorControllers(MyContext context) { _context = context; }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateVendor(VendorData data)
        {
            var company = await _context.Companies.FindAsync(data.CompanyId);
            if (company == null || !company.IsApprovedByAdmin || !company.IsApprovedByManager)
                return BadRequest("Company belum disetujui sepenuhnya");

            var vendor = new Vendor
            {
                CompanyId = data.CompanyId,
                BusinessField = data.BusinessField,
                CompanyType = data.CompanyType
            };

            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
            return Ok(vendor);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendor(int id)
        {
            var v = await _context.Vendors.Include(v => v.Company)
                         .FirstOrDefaultAsync(v => v.Id == id);
            if (v == null) return NotFound();
            return Ok(v);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(int id, VendorData data)
        {
            var v = await _context.Vendors.FindAsync(id);
            if (v == null) return NotFound();
            v.BusinessField = data.BusinessField;
            v.CompanyType = data.CompanyType;
            await _context.SaveChangesAsync();
            return Ok(v);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var v = await _context.Vendors.FindAsync(id);
            if (v == null) return NotFound();
            _context.Vendors.Remove(v);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
