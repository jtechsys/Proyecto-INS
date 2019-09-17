using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Web;

namespace doMain.Utils
{
    /// <summary>
    /// Summary description for ExportarUtils
    /// </summary>
    public class ExportarUtils
    {

        public string DescargarDataTableToExcel(DataTable tbl, string pNombreHoja, object[] pNombreCabeceras, object[] pEstiloCeldas, System.Web.HttpResponse pResponse)
        {
            try
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    //Creando la Hoja de Excel
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(pNombreHoja/*fileName*/);

                    //Cargar el DataTable en la hoja, empezando desde A1. Imprimir la columna en la fila 1
                    ws.Cells[1, 1].LoadFromDataTable(tbl, true);

                    //Formato de la Cabeza 
                    ws.Row(1).Height = 25;

                    //Asigna nombre a las columnas
                    for (int i = 0; i < pNombreCabeceras.Length; i++)
                        ws.Cells[1, i + 1].Value = pNombreCabeceras[i];

                    using (ExcelRange rng = ws.Cells[1, 1, 1, pNombreCabeceras.Length])
                    {
                        rng.Style.Font.Bold = true;
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(103, 128, 132));
                        rng.Style.Font.Size = 12;
                        rng.AutoFitColumns();
                        rng.Style.Font.Color.SetColor(Color.White);
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    }

                    DateTime dd;
                    for (int i = 0; i < pEstiloCeldas.Length; i++)
                    {
                        object[] item = (object[])pEstiloCeldas[i];
                        int nCelda = (int)item[0];
                        OleDbType TipoCelda = (OleDbType)item[1];
                        //////Nota: ew1.Cells[filaDesde, columnaDesde, filaHasta, columnaHasta]
                        using (ExcelRange col = ws.Cells[2, nCelda, 2 + tbl.Rows.Count, nCelda])
                        {
                            if (TipoCelda == OleDbType.Date)
                            {
                                col.Style.Numberformat.Format = "dd/MM/yyyy";
                                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            }
                            if (TipoCelda == OleDbType.DBTimeStamp)
                            {
                                col.Style.Numberformat.Format = "dd/MM/yyyy hh:mm:ss";
                                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            }
                            else if (TipoCelda == OleDbType.Decimal)
                            {
                                col.Style.Numberformat.Format = "#,##0.00";
                                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            }
                            else if (TipoCelda == OleDbType.Currency)
                            {
                                col.Style.Numberformat.Format = "#,##0;[Red]-#,##0";
                                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            }
                            else if (TipoCelda == OleDbType.Double)
                            {
                                col.Style.Numberformat.Format = "#,##0.000";
                                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            }
                            else if (TipoCelda == OleDbType.Integer)
                            {
                                col.Style.Numberformat.Format = "#";
                                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                col.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            }
                        }
                    }

                    if (ws.Dimension != null)
                        ws.Cells[ws.Dimension.Address].AutoFitColumns();

                    pResponse.Clear();
                    pResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    pResponse.AddHeader("content-disposition", "attachment;  filename=" + pNombreHoja.Replace(" ", "-").Trim() + ".xlsx");
                    pResponse.BinaryWrite(pck.GetAsByteArray());
                    pResponse.Flush();
                    pResponse.SuppressContent = true;
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    return "OK";
                }

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

    }
}
