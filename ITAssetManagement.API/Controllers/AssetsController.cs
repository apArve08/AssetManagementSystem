using ITAssetManagement.API.Data;
using ITAssetManagement.API.Models;
using ITAssetManagement.API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ITAssetManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssetsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AssetsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets([FromQuery] string? search)
        {
            var query = _context.Assets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(a => 
                    a.AssetTag.ToLower().Contains(search) ||
                    a.Name.ToLower().Contains(search) ||
                    a.Category.ToLower().Contains(search) ||
                    a.Brand.ToLower().Contains(search) ||
                    a.SerialNumber.ToLower().Contains(search) ||
                    (a.AssignedTo != null && a.AssignedTo.ToLower().Contains(search))
                );
            }

            return await query.OrderBy(a => a.AssetTag).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(int id)
        {
            var asset = await _context.Assets
                .Include(a => a.ActivityLogs)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asset == null)
            {
                return NotFound();
            }

            return asset;
        }

        [HttpPost]
        public async Task<ActionResult<Asset>> CreateAsset(AssetDto assetDto)
        {
            var asset = new Asset
            {
                AssetTag = assetDto.AssetTag,
                Name = assetDto.Name,
                Category = assetDto.Category,
                Brand = assetDto.Brand,
                Model = assetDto.Model,
                SerialNumber = assetDto.SerialNumber,
                PurchaseDate = assetDto.PurchaseDate,
                PurchasePrice = assetDto.PurchasePrice,
                Status = assetDto.Status,
                AssignedTo = assetDto.AssignedTo,
                AssignedDate = assetDto.AssignedDate,
                Location = assetDto.Location,
                Notes = assetDto.Notes
            };

            _context.Assets.Add(asset);

            // Add activity log
            var activityLog = new ActivityLog
            {
                Asset = asset,
                Action = "Created",
                PerformedBy = User.Identity?.Name ?? "System",
                Details = $"Asset {asset.AssetTag} created"
            };
            _context.ActivityLogs.Add(activityLog);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAsset), new { id = asset.Id }, asset);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsset(int id, AssetDto assetDto)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            var oldStatus = asset.Status;
            var oldAssignedTo = asset.AssignedTo;

            asset.AssetTag = assetDto.AssetTag;
            asset.Name = assetDto.Name;
            asset.Category = assetDto.Category;
            asset.Brand = assetDto.Brand;
            asset.Model = assetDto.Model;
            asset.SerialNumber = assetDto.SerialNumber;
            asset.PurchaseDate = assetDto.PurchaseDate;
            asset.PurchasePrice = assetDto.PurchasePrice;
            asset.Status = assetDto.Status;
            asset.AssignedTo = assetDto.AssignedTo;
            asset.AssignedDate = assetDto.AssignedDate;
            asset.Location = assetDto.Location;
            asset.Notes = assetDto.Notes;
            asset.UpdatedAt = DateTime.UtcNow;

            // Add activity log
            var action = "Updated";
            var details = $"Asset {asset.AssetTag} updated";

            if (oldAssignedTo != asset.AssignedTo)
            {
                if (string.IsNullOrEmpty(oldAssignedTo) && !string.IsNullOrEmpty(asset.AssignedTo))
                {
                    action = "Assigned";
                    details = $"Asset {asset.AssetTag} assigned to {asset.AssignedTo}";
                }
                else if (!string.IsNullOrEmpty(oldAssignedTo) && string.IsNullOrEmpty(asset.AssignedTo))
                {
                    action = "Unassigned";
                    details = $"Asset {asset.AssetTag} unassigned from {oldAssignedTo}";
                }
            }

            var activityLog = new ActivityLog
            {
                AssetId = asset.Id,
                Action = action,
                PerformedBy = User.Identity?.Name ?? "System",
                Details = details
            };
            _context.ActivityLogs.Add(activityLog);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            // Add activity log
            var activityLog = new ActivityLog
            {
                AssetId = asset.Id,
                Action = "Deleted",
                PerformedBy = User.Identity?.Name ?? "System",
                Details = $"Asset {asset.AssetTag} deleted"
            };
            _context.ActivityLogs.Add(activityLog);

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<DashboardDto>> GetDashboard()
        {
            var assets = await _context.Assets.ToListAsync();

            var dashboard = new DashboardDto
            {
                TotalAssets = assets.Count,
                AssignedAssets = assets.Count(a => a.Status == "Assigned"),
                AvailableAssets = assets.Count(a => a.Status == "Available"),
                RepairAssets = assets.Count(a => a.Status == "Repair"),
                RetiredAssets = assets.Count(a => a.Status == "Retired"),
                AssetsByCategory = assets.GroupBy(a => a.Category)
                    .ToDictionary(g => g.Key, g => g.Count()),
                AssetsByStatus = assets.GroupBy(a => a.Status)
                    .ToDictionary(g => g.Key, g => g.Count())
            };

            return dashboard;
        }

        [HttpGet("{id}/activities")]
        public async Task<ActionResult<IEnumerable<ActivityLog>>> GetAssetActivities(int id)
        {
            var activities = await _context.ActivityLogs
                .Where(a => a.AssetId == id)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            return activities;
        }
    }
}