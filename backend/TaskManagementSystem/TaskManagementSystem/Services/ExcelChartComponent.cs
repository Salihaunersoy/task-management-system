using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using C  = DocumentFormat.OpenXml.Drawing.Charts;
using A  = DocumentFormat.OpenXml.Drawing;
using XDR = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using XS  = DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace TaskManagementSystem.Services
{
	public static class ExcelChartComponent
	{
		public static void AddChart(WorksheetPart worksheetPart, ChartRequest chartReq, int chartIndex = 0)
		{
			DrawingsPart drawingsPart = worksheetPart.DrawingsPart ?? worksheetPart.AddNewPart<DrawingsPart>();
			if (drawingsPart.WorksheetDrawing == null)
				drawingsPart.WorksheetDrawing = new XDR.WorksheetDrawing();

			ChartPart chartPart = drawingsPart.AddNewPart<ChartPart>();
			string relId        = drawingsPart.GetIdOfPart(chartPart);

			C.ChartSpace chartSpace = new C.ChartSpace();
			chartSpace.Append(new C.EditingLanguage() { Val = "tr-TR" });

			C.Chart chart = new C.Chart();
			chart.Append(new C.AutoTitleDeleted() { Val = false });
			chart.Append(new C.Title(
				new C.ChartText(
					new C.RichText(
						new A.BodyProperties(),
						new A.ListStyle(),
						new A.Paragraph(
							new A.Run(
								new A.RunProperties() { FontSize = 1200, Bold = true },
								new A.Text         () { Text = chartReq.Title }
							)
						)
					)
				),
				new C.Overlay() { Val = false }
			));

			C.PlotArea plotArea = new C.PlotArea();
			plotArea.Append(new C.Layout());

			if (chartReq.Type == "doughnut")
			{
				C.DoughnutChart doughnutChart = new C.DoughnutChart();
				doughnutChart.Append(new C.VaryColors() { Val = true });

				C.PieChartSeries series = new C.PieChartSeries();
				series.Append(new C.Index() { Val = 0 });
				series.Append(new C.Order() { Val = 0 });
				series.Append(new C.SeriesText(new C.NumericValue() { Text = chartReq.Title }));

				C.StringReference stringReference = new C.StringReference();
				C.StringCache stringCache = new C.StringCache();

				stringCache.Append(new C.PointCount() { Val = (uint)chartReq.Data.Count });

				uint catIdx = 0;

				foreach (string key in chartReq.Data.Keys)
				{
					stringCache.Append(new C.StringPoint(new C.NumericValue() { Text = key }) { Index = catIdx });
					catIdx++;
				}
				stringReference.Append(stringCache);
				series.Append(new C.CategoryAxisData(stringReference));

				C.NumberReference numberReference = new C.NumberReference();
				C.NumberingCache NumberCache = new C.NumberingCache();
				NumberCache.Append(new C.FormatCode() { Text = "General" });
				NumberCache.Append(new C.PointCount() { Val = (uint)chartReq.Data.Count });

				uint valIdx = 0;
				foreach (int val in chartReq.Data.Values)
				{
					NumberCache.Append(new C.NumericPoint(new C.NumericValue() { Text = val.ToString() }) { Index = valIdx });
					valIdx++;
				}
				numberReference.Append(NumberCache);
				series.Append(new C.Values(numberReference));

				doughnutChart.Append(series);
				doughnutChart.Append(new C.HoleSize() { Val = 70 });

				plotArea.Append(doughnutChart);
			}
			if (chartReq.Type == "bar")
			{
				C.BarChart barChart = new C.BarChart();
				barChart.Append(new C.BarDirection() { Val = C.BarDirectionValues.Column });
				barChart.Append(new C.BarGrouping () { Val = C.BarGroupingValues.Stacked });
				barChart.Append(new C.VaryColors  () { Val = false });

				string[] categories = chartReq.Data.Keys.ToArray();

				HashSet<string> allStatuses = new HashSet<string>();
				foreach (var userStatuses in chartReq.BarData.Values)
				{
					foreach (string status in userStatuses.Keys)
					{
						allStatuses.Add(status);
					}
				}

				uint seriesIdx = 0;
				foreach (string status in allStatuses)
				{
					C.BarChartSeries series = new C.BarChartSeries();
					series.Append(new C.Index() { Val = seriesIdx });
					series.Append(new C.Order() { Val = seriesIdx });
					series.Append(new C.SeriesText(new C.NumericValue() { Text = status }));

					C.StringReference catRef = new C.StringReference();
					C.StringCache catCache   = new C.StringCache();
					catCache.Append(new C.PointCount() { Val = (uint)categories.Length });

					for (uint i = 0; i < categories.Length; i++)
					{
						catCache.Append(new C.StringPoint(new C.NumericValue() { Text = categories[i] }) { Index = i });
					}
					catRef.Append(catCache);
					series.Append(new C.CategoryAxisData(catRef));

					C.NumberReference numberReference = new C.NumberReference();
					C.NumberingCache numberCache      = new C.NumberingCache();

					numberCache.Append(new C.FormatCode() { Text = "General" });
					numberCache.Append(new C.PointCount() { Val  = (uint)categories.Length });

					for (uint i = 0; i < categories.Length; i++)
					{
						int val = chartReq.BarData[categories[i]].GetValueOrDefault(status, 0);
						numberCache.Append(new C.NumericPoint(new C.NumericValue() { Text = val.ToString() }) { Index = i });
					}
					numberReference.Append(numberCache);
					series.Append(new C.Values(numberReference));

					barChart.Append(series);
					seriesIdx++;
				}

				barChart.Append(new C.AxisId() { Val = 1 });
				barChart.Append(new C.AxisId() { Val = 2 });

				plotArea.Append(barChart);

				plotArea.Append(new C.CategoryAxis(
					new C.AxisId() { Val = 1 },
					new C.Scaling(new C.Orientation() { Val = C.OrientationValues.MinMax }),
					new C.Delete() { Val = false },
					new C.AxisPosition() { Val = C.AxisPositionValues.Bottom },
					new C.CrossingAxis() { Val = 2 },
					new C.Crosses() { Val = C.CrossesValues.AutoZero }
				));

				plotArea.Append(new C.ValueAxis(
					new C.AxisId() { Val = 2 },
					new C.Scaling(new C.Orientation() { Val = C.OrientationValues.MinMax }),
					new C.Delete() { Val = false },
					new C.AxisPosition() { Val = C.AxisPositionValues.Left },
					new C.CrossingAxis() { Val = 1 },
					new C.Crosses() { Val = C.CrossesValues.AutoZero }
				));

			}


			chart.Append(plotArea);
			chart.Append(new C.Legend(
				new C.LegendPosition() { Val = C.LegendPositionValues.Right },
				new C.Overlay       () { Val = false }
			));
			chart.Append(new C.PlotVisibleOnly() { Val = true });

			chartSpace.Append(chart);
			chartPart.ChartSpace = chartSpace;
			chartPart.ChartSpace.Save();

			AppendChartToDrawing(drawingsPart, worksheetPart, relId, chartIndex);
		}

		private static void AppendChartToDrawing(DrawingsPart drawingsPart, WorksheetPart worksheetPart, string relId, int chartIndex)
		{
			int chartHeight  = 14;
			int chartSpacing = 2;
			int startRow     = 1 + (chartIndex * (chartHeight + chartSpacing));

			XDR.OneCellAnchor oneCellAnchor = new XDR.OneCellAnchor();

			oneCellAnchor.Append(new XDR.FromMarker(
				new XDR.ColumnId("1"), new XDR.ColumnOffset("0"),
				new XDR.RowId(startRow.ToString()), new XDR.RowOffset("0")
			));

			oneCellAnchor.Append(new XDR.Extent()
			{
				Cx = 9144000L,
				Cy = 5486400L
			});

			var graphicFrame = new XDR.GraphicFrame(
				new XDR.NonVisualGraphicFrameProperties(
					new XDR.NonVisualDrawingProperties()
					{
						Id   = (UInt32Value)(uint)(chartIndex + 2),
						Name = "Chart" + (chartIndex + 1)
					},
					new XDR.NonVisualGraphicFrameDrawingProperties()
				),
				new XDR.Transform(
					new A.Offset()  { X = 0, Y = 0 },
					new A.Extents() { Cx = 9144000L, Cy = 5486400L }
				),
				new A.Graphic(
					new A.GraphicData(
						new C.ChartReference() { Id = relId }
					)
					{ Uri = "http://schemas.openxmlformats.org/drawingml/2006/chart" }
				)
			);

			oneCellAnchor.Append(graphicFrame);
			oneCellAnchor.Append(new XDR.ClientData());

			drawingsPart.WorksheetDrawing!.Append(oneCellAnchor);
			drawingsPart.WorksheetDrawing.Save();

			if (!worksheetPart.Worksheet!.Elements<XS.Drawing>().Any())
			{
				worksheetPart.Worksheet.Append(new XS.Drawing() { Id = worksheetPart.GetIdOfPart(drawingsPart) });
			}

			worksheetPart.Worksheet.Save();
		}
	}
}
