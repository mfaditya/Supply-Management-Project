using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupplyManagement.API.Data;
using SupplyManagement.API.Models;

namespace SupplyManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly MyContext _context;
        private readonly IWebHostEnvironment _env;

        public CompanyController(MyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterCompany([FromForm] CompanyRegisterData data){
            var fileName = $"{Guid.NewGuid()}_{data.File.FileName}";
            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
            var filePath = Path.Combine(uploads, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await data.File.CopyToAsync(stream);

            var company = new Company
            {
                CompanyName = data.CompanyName,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                LogoPath = $"/uploads/{fileName}"
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return Ok(new { company.Id });
        }

        [HttpPut("Approve/admin/{id")]
        public async Task<IActionResult> ApproveByAdmin(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null) return NotFound();
            company.IsApprovedByAdmin = true;
            await _context.SaveChangesAsync();
            return Ok(company);
        }

        [HttpPut("approve/manager/{id}")]
        public async Task<IActionResult> ApproveByManager(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null) return NotFound();
            if (!company.IsApprovedByAdmin) return BadRequest("Belum disetujui admin");
            company.IsApprovedByManager = true;
            await _context.SaveChangesAsync();
            return Ok(company);
        }
    }
}
