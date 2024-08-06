using CarAuction.Service.DTOs.Cars;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;

namespace CarAuction.Service.Helpers;

public static class ExcelReport
{
    public static IFormFile GenerateReportAsExcel(this List<CarGetDto> dtos)
    {
        var stream = new MemoryStream();
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Cars");

            // Add column headers
            worksheet.Cell(1, 1).Value = "Admin";
            worksheet.Cell(1, 2).Value = "Id";
            worksheet.Cell(1, 3).Value = "Vin";
            worksheet.Cell(1, 4).Value = "FabricationYear";
            worksheet.Cell(1, 5).Value = "Odometer";
            worksheet.Cell(1, 6).Value = "NoGears";
            worksheet.Cell(1, 7).Value = "Transmission";
            worksheet.Cell(1, 8).Value = "FuelCity";
            worksheet.Cell(1, 9).Value = "FuelWay";
            worksheet.Cell(1, 10).Value = "Power";
            worksheet.Cell(1, 11).Value = "Motor";
            worksheet.Cell(1, 12).Value = "Description";
            worksheet.Cell(1, 13).Value = "Ban";
            worksheet.Cell(1, 14).Value = "Fuel";
            worksheet.Cell(1, 15).Value = "Model";
            worksheet.Cell(1, 16).Value = "Color";
            worksheet.Cell(1, 17).Value = "InitialPrice";
            worksheet.Cell(1, 18).Value = "AuctionWinPrice";
            worksheet.Cell(1, 19).Value = "AuctionDate";
            worksheet.Cell(1, 20).Value = "FinishDate";
            worksheet.Cell(1, 21).Value = "WinnerName";

            // Add rows of data
            for (var i = 0; i < dtos.Count; i++)
            {
                var car = dtos[i];
                var row = i + 2;
                worksheet.Cell(row, 1).Value = car.Admin;
                worksheet.Cell(row, 2).Value = car.Id.ToString();
                worksheet.Cell(row, 3).Value = car.Vin;
                worksheet.Cell(row, 4).Value = car.FabricationYear;
                worksheet.Cell(row, 5).Value = car.Odometer;
                worksheet.Cell(row, 6).Value = car.NoGears;
                worksheet.Cell(row, 7).Value = car.Transmission;
                worksheet.Cell(row, 8).Value = car.FuelCity;
                worksheet.Cell(row, 9).Value = car.FuelWay;
                worksheet.Cell(row, 10).Value = car.Power;
                worksheet.Cell(row, 11).Value = car.Motor;
                worksheet.Cell(row, 12).Value = car.Description;
                worksheet.Cell(row, 13).Value = car.Ban?.Name;
                worksheet.Cell(row, 14).Value = car.Fuel?.Name;
                worksheet.Cell(row, 15).Value = car.Model?.Name;
                worksheet.Cell(row, 16).Value = car.Color?.Name;
                worksheet.Cell(row, 17).Value = car.InitialPrice;
                worksheet.Cell(row, 18).Value = car.AuctionWinPrice;
                worksheet.Cell(row, 19).Value = car.AuctionDate.ToString();
                worksheet.Cell(row, 20).Value = car.FinishDate.ToString();
                worksheet.Cell(row, 21).Value = car.WinnerName ==null ? "No one":car.WinnerName;
            }

            workbook.SaveAs(stream);
            stream.Position = 0; // Rewind the stream to the beginning
        }

        var fileName = "CarReport.xlsx";
        var fileContent = stream.ToArray();

        return new FormFile(new MemoryStream(fileContent), 0, fileContent.Length, null, fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        };
    }
}
