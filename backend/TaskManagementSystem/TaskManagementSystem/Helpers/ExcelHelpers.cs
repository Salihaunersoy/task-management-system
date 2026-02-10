using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;

namespace TaskManagementSystem.Helpers
{
	public class ExcelHelpers
	{
		public static string GetCellValue(Cell? cell, SharedStringTable? sharedStringTable)
		{
			string cellRef = cell?.CellReference?.Value ?? "Bilinmeyen Hücre";

			if (cell == null)
				throw new Exception($"{cellRef} hücresi bulunamadı!");

			if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
			{
				if (cell.CellValue != null && sharedStringTable != null)
				{
					int index = int.Parse(cell.CellValue.InnerText);
					return sharedStringTable.ChildElements[index].InnerText;
				}
			}

			if (cell.DataType != null && cell.DataType.Value == CellValues.InlineString)
			{
				return cell.InlineString?.Text?.Text ?? cell.InnerText;
			}

			string value = cell.CellValue?.InnerText ?? cell.InnerText;

			if (string.IsNullOrWhiteSpace(value))
			{
				throw new Exception($"{cellRef} hücresinde veri bulunamadı!");
			}

			return value;
		}

		public static DateTime ParseExcelDate(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new Exception("Tarih alanı boş bırakılamaz!");
			}

			if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double oaDate))
			{
				return DateTime.FromOADate(oaDate);
			}

			if (DateTime.TryParse(value, out DateTime dt))
			{
				return dt;
			}

			throw new Exception("Geçersiz tarih formatı algılandı: '{value}'. Lütfen hücreyi kontrol edin.");
		}

		public static decimal ParseDecimal(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return 0;
			}

			if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
			{
				return result;
			}

			string cleanedValue = value.Replace(".", "").Replace(",", ".");
			if (decimal.TryParse(cleanedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
			{
				return result;
			}

			return 0;
		}
	}
}
