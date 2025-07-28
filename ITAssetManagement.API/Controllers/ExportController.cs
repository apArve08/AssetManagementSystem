using ITAssetManagement.API.Data;
using ITAssetManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITAssetManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExportController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ExportService _exportService;

        public ExportController(AppDbContext context, ExportService exportService)
        {
            _context = context;
            _exportService = exportService;
        }

        [HttpGet("assets")]
        public async Task<IActionResult> ExportAssets()
        {
            var assets = await _context.Assets.ToListAsync();
            var fileContent = _exportService.ExportAssetsToExcel(assets);
            var fileName = $"Assets_Export_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ExportUsers()
        {
            var users = await _context.Users.ToListAsync();
            var fileContent = _exportService.ExportUsersToExcel(users);
            var fileName = $"Users_Export_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}