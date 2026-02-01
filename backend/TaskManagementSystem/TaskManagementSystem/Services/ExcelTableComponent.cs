using System.Collections;
using System.Reflection;
using DocumentFormat.OpenXml.Spreadsheet;

namespace TaskManagementSystem.Services
{
	public static class ExcelTableComponent
	{
		public static void AddTable<T>(List<T> data, SheetData sheetData)
		{
			PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

			foreach (T item in data)
			{
				Row newRow = new Row();

				foreach (PropertyInfo prop in properties)
				{
					object? rawValue  = prop.GetValue(item);
					string  cellValue = rawValue switch
					{
						null       => "",
						DateTime d => d.ToString("dd.MM.yyyy"),
						_          => rawValue.ToString() ?? ""
					};

					Cell cell = new Cell()
					{
						CellValue = new CellValue(cellValue),
					};

					if (double.TryParse(cellValue, out double numericValue))
					{
						cell.DataType = CellValues.Number;
					}
					else
					{
						cell.DataType = CellValues.String;
					}

					newRow.Append(cell);
				}

				sheetData.Append(newRow);
			}
		}
	}
}
