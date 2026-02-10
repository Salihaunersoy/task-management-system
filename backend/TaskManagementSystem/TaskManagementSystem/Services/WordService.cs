using System.Reflection;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services
{
	public class WordService
	{
		public byte[] GenerateWordReport(List<FisData> data)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document))
				{
					MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
					mainPart.Document = new Document();

					Body body = new Body();

					Table table = new Table();

					TableProperties tableProps = new TableProperties(
						new TableBorders(
							new TopBorder              { Val = BorderValues.Single, Size = 4 },
							new BottomBorder           { Val = BorderValues.Single, Size = 4 },
							new LeftBorder             { Val = BorderValues.Single, Size = 4 },
							new RightBorder            { Val = BorderValues.Single, Size = 4 },
							new InsideHorizontalBorder { Val = BorderValues.Single, Size = 4 },
							new InsideVerticalBorder   { Val = BorderValues.Single, Size = 4 }
						),
						new TableLayout { Type = TableLayoutValues.Autofit }
					);
					table.Append(tableProps);

					PropertyInfo[] properties = typeof(FisData).GetProperties(BindingFlags.Public | BindingFlags.Instance);

					TableRow headerRow = new TableRow();
					foreach (PropertyInfo prop in properties)
					{
						TableCell cell = new TableCell();

						TableCellProperties cellProperties = new TableCellProperties(
							new TableCellWidth { Type = TableWidthUnitValues.Auto }
						);
						cell.Append(cellProperties);

						Paragraph paragraph = new Paragraph(
							new Run(
								new RunProperties(new Bold()),
								new Text(prop.Name)
							)
						);

						cell.Append(paragraph);

						headerRow.Append(cell);
					}
					table.Append(headerRow);

					foreach (FisData fis in data)
					{
						TableRow dataRow = new TableRow();
						foreach (PropertyInfo prop in properties)
						{
							object? value = prop.GetValue(fis);
							string cellValue = value switch
							{
								null       => "",
								DateTime d => d.ToString("dd.MM.yyyy"),
								decimal m  => m.ToString("N2"),
								_          => value.ToString() ?? ""
							};
							dataRow.Append(
								new TableCell(
									new Paragraph(
										new Run(
											new Text(cellValue)
										)
									)
								)
							);
						}
						table.Append(dataRow);
					}

					body.Append(table);
					mainPart.Document.Append(body);
					mainPart.Document.Save();
				}

				return memoryStream.ToArray();
			}
		}

	}
}
