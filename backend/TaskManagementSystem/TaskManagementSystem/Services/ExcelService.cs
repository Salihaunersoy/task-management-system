using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace TaskManagementSystem.Services
{
	public class ChartRequest
	{
		public string                                       Title   { get; set; } = "";
		public string                                       Type    { get; set; } = "doughnut";
		public Dictionary<string, int>?                     Data    { get; set; }
		public Dictionary<string, Dictionary<string, int>>? BarData { get; set; }
	}

	public class ExcelService
	{
		public byte[] GenerateReport<T>(List<T> data, List<ChartRequest>? charts = null)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (SpreadsheetDocument document = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
				{
					WorkbookPart workbookPart = document.AddWorkbookPart();
					workbookPart.Workbook     = new Workbook();

					Sheets sheets = new Sheets();
					workbookPart.Workbook.Append(sheets);

					string typeName  = typeof(T).Name;
					string sheetName = typeName.EndsWith("DTO") ? typeName[..^3] : typeName;

					WorksheetPart dataWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
					Worksheet dataWorksheet         = new Worksheet();
					SheetData dataSheetData         = new SheetData();

					dataWorksheet.Append(dataSheetData);
					ExcelTableComponent.AddTable(data, dataSheetData);
					dataWorksheetPart.Worksheet = dataWorksheet;

					sheets.Append(new Sheet()
					{
						Id      = workbookPart.GetIdOfPart(dataWorksheetPart),
						SheetId = 1,
						Name    = sheetName
					});

					if (charts != null && charts.Count > 0)
					{
						uint sheetId = 2;
						foreach (ChartRequest chartReq in charts)
						{
							WorksheetPart chartWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
							Worksheet chartWorksheet         = new Worksheet();
							SheetData chartSheetData         = new SheetData();

							chartWorksheet.Append(chartSheetData);
							chartWorksheetPart.Worksheet = chartWorksheet;

							sheets.Append(new Sheet()
							{
								Id      = workbookPart.GetIdOfPart(chartWorksheetPart),
								SheetId = sheetId,
								Name    = chartReq.Title
							});

							ExcelChartComponent.AddChart(chartWorksheetPart, chartReq, 0);

							chartWorksheetPart.Worksheet.Save();
							sheetId++;
						}
					}

					workbookPart.Workbook.Save();
				}
				return memoryStream.ToArray();
			}
		}
	}
}
