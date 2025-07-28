namespace ITAssetManagement.API.Models
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; } = null!;
        public string Action { get; set; } = string.Empty; // Created, Updated, Deleted, Assigned, Unassigned
        public string PerformedBy { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}