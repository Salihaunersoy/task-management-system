using TaskManagementSystem.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;

namespace TaskManagementSystem.Services
{
	public class PdfService
	{
		public byte[] GeneratePdfReport(List<FisData> data)
		{
			PdfDocument document = new PdfDocument();

			document.PageSettings.Orientation = PdfPageOrientation.Portrait;
			document.PageSettings.Margins.All = 15;

			PdfPage page = document.Pages.Add();
			PdfGrid pdfGrid = new PdfGrid();

			pdfGrid.DataSource = data;
			pdfGrid.RepeatHeader = true;

			string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
			
			PdfFont turkishFont     = new PdfTrueTypeFont(fontPath, 8, PdfFontStyle.Regular);
			PdfFont turkishFontBold = new PdfTrueTypeFont(fontPath, 9, PdfFontStyle.Bold);

			pdfGrid.Style.Font = turkishFont;

			if (pdfGrid.Headers.Count > 0)
			{
				PdfGridRow headerRow = pdfGrid.Headers[0];

				PdfGridCellStyle headerStyle = new PdfGridCellStyle();
				headerStyle.Font             = turkishFontBold; 
				headerStyle.BackgroundBrush  = PdfBrushes.LightGray;

				headerRow.ApplyStyle(headerStyle);
			}

			pdfGrid.Style.CellPadding = new PdfPaddings(3, 3, 3, 3);

			pdfGrid.Draw(page, new PointF(0, 0));

			using (MemoryStream stream = new MemoryStream())
			{
				document.Save(stream);
				document.Close(true);
				return stream.ToArray();
			}
		}
	}
}