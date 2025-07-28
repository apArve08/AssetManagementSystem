namespace ITAssetManagement.API.Models.DTOs
{

    public class AssetDto
    {
        public int Id { get; set; }
        public string AssetTag { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; }
        public decimal PurchasePrice { get; set; }
        public string Status { get; set; } = "Available";
        public string? AssignedTo { get; set; }
        public DateTime? AssignedDate { get; set; }
        public string? Location { get; set; }
        public string? Notes { get; set; }
    }

}