using System.ComponentModel.DataAnnotations;

namespace ITAssetManagement.API.Models
{
    public class Asset
    {
        public int Id { get; set; }
        
        [Required]
        public string AssetTag { get; set; } = string.Empty;
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string Category { get; set; } = string.Empty;
        
        public string Brand { get; set; } = string.Empty;
        
        public string Model { get; set; } = string.Empty;
        
        public string SerialNumber { get; set; } = string.Empty;
        
        public DateTime PurchaseDate { get; set; }
        
        public decimal PurchasePrice { get; set; }
        
        public string Status { get; set; } = "Available"; // Available, Assigned, Repair, Retired
        
        public string? AssignedTo { get; set; }
        
        public DateTime? AssignedDate { get; set; }
        
        public string? Location { get; set; }
        
        public string? Notes { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public List<ActivityLog> ActivityLogs { get; set; } = new();
    }
}