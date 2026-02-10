using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services
{
	public class ExcelReadFisData
	{
		public static List<FisData> ReadFisData(IFormFile file)
		{
			List<FisData> fisList = new List<FisData>();

			try
			{
				using(Stream stream = file.OpenReadStream()) 
				{
					using (SpreadsheetDocument doc = SpreadsheetDocument.Open(stream, false))
					{
						WorkbookPart           workbookPart			 = doc.WorkbookPart;
						SharedStringTablePart? sharedStringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
						SharedStringTable?     sharedStringTable     = sharedStringTablePart?.SharedStringTable;

						WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
						SheetData     sheetData     = worksheetPart.Worksheet.Elements<SheetData>().First();

						foreach (Row row in sheetData.Elements<Row>().Skip(1))
						{
							List<Cell> cells = row.Elements<Cell>().ToList();
							if (!cells.Any()) continue;

							FisData fisData = new FisData
							{
								FisNo     = Helpers.ExcelHelpers.GetCellValue(cells.ElementAtOrDefault(0), sharedStringTable),
								FisTarih  = (DateTime)Helpers.ExcelHelpers.ParseExcelDate(Helpers.ExcelHelpers.GetCellValue(cells.ElementAtOrDefault(1), sharedStringTable)),
								HesapKodu = Helpers.ExcelHelpers.GetCellValue(cells.ElementAtOrDefault(2), sharedStringTable),
								HesapAdi  = Helpers.ExcelHelpers.GetCellValue(cells.ElementAtOrDefault(3), sharedStringTable),
								Aciklama  = Helpers.ExcelHelpers.GetCellValue(cells.ElementAtOrDefault(4), sharedStringTable),
								FaturaNo  = Helpers.ExcelHelpers.GetCellValue(cells.ElementAtOrDefault(5), sharedStringTable),
								Borc      = Helpers.ExcelHelpers.ParseDecimal(Helpers.ExcelHelpers.GetCellValue(cells.ElementAtOrDefault(6), sharedStringTable)),
								Alacak    = Helpers.ExcelHelpers.ParseDecimal(Helpers.ExcelHelpers.GetCellValue(cells.ElementAtOrDefault(7), sharedStringTable))
							};

							fisList.Add(fisData);
						}
					}
				}
			}
			catch(Exception ex)
			{
				throw new Exception("Excel okuma sırasında bir hata oluştu: " + ex.Message);
			}

			return fisList;
		}
	}
}
