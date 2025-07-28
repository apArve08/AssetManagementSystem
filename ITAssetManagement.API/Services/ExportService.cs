using ClosedXML.Excel;
using ITAssetManagement.API.Models;

namespace ITAssetManagement.API.Services
{
    public class ExportService
    {
        public byte[] ExportAssetsToExcel(List<Asset> assets)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Assets");

            // Headers
            worksheet.Cell(1, 1).Value = "Asset Tag";
            worksheet.Cell(1, 2).Value = "Name";
            worksheet.Cell(1, 3).Value = "Category";
            worksheet.Cell(1, 4).Value = "Brand";
            worksheet.Cell(1, 5).Value = "Model";
            worksheet.Cell(1, 6).Value = "Serial Number";
            worksheet.Cell(1, 7).Value = "Purchase Date";
            worksheet.Cell(1, 8).Value = "Purchase Price";
            worksheet.Cell(1, 9).Value = "Status";
            worksheet.Cell(1, 10).Value = "Assigned To";
            worksheet.Cell(1, 11).Value = "Location";
            worksheet.Cell(1, 12).Value = "Notes";

            // Style headers
            var headerRange = worksheet.Range(1, 1, 1, 12);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

            // Data
            int row = 2;
            foreach (var asset in assets)
            {
                worksheet.Cell(row, 1).Value = asset.AssetTag;
                worksheet.Cell(row, 2).Value = asset.Name;
                worksheet.Cell(row, 3).Value = asset.Category;
                worksheet.Cell(row, 4).Value = asset.Brand;
                worksheet.Cell(row, 5).Value = asset.Model;
                worksheet.Cell(row, 6).Value = asset.SerialNumber;
                worksheet.Cell(row, 7).Value = asset.PurchaseDate.ToString("yyyy-MM-dd");
                worksheet.Cell(row, 8).Value = asset.PurchasePrice;
                worksheet.Cell(row, 9).Value = asset.Status;
                worksheet.Cell(row, 10).Value = asset.AssignedTo ?? "";
                worksheet.Cell(row, 11).Value = asset.Location ?? "";
                worksheet.Cell(row, 12).Value = asset.Notes ?? "";
                row++;
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public byte[] ExportUsersToExcel(List<User> users)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Users");

            // Headers
            worksheet.Cell(1, 1).Value = "Username";
            worksheet.Cell(1, 2).Value = "Full Name";
            worksheet.Cell(1, 3).Value = "Email";
            worksheet.Cell(1, 4).Value = "Role";
            worksheet.Cell(1, 5).Value = "Status";
            worksheet.Cell(1, 6).Value = "Created At";

            // Style headers
            var headerRange = worksheet.Range(1, 1, 1, 6);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

            // Data
            int row = 2;
            foreach (var user in users)
            {
                worksheet.Cell(row, 1).Value = user.Username;
                worksheet.Cell(row, 2).Value = user.FullName;
                worksheet.Cell(row, 3).Value = user.Email;
                worksheet.Cell(row, 4).Value = user.Role;
                worksheet.Cell(row, 5).Value = user.IsActive ? "Active" : "Inactive";
                worksheet.Cell(row, 6).Value = user.CreatedAt.ToString("yyyy-MM-dd HH:mm");
                row++;
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}