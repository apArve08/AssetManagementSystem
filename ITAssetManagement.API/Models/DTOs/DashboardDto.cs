namespace ITAssetManagement.API.Models.DTOs
{

    public class DashboardDto
    {
        public int TotalAssets { get; set; }
        public int AssignedAssets { get; set; }
        public int AvailableAssets { get; set; }
        public int RepairAssets { get; set; }
        public int RetiredAssets { get; set; }
        public Dictionary<string, int> AssetsByCategory { get; set; } = new();
        public Dictionary<string, int> AssetsByStatus { get; set; } = new();
    }


}