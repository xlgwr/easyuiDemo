using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;
using System.Reflection;
using System.Web;
using PetaPoco;
using NPOI.SS.Util;

namespace Valeo.Common
{
    public class ExcelHelper : IDisposable
    {
        private string fileName = null; //文件名
        private IWorkbook workbook = null;
        private FileStream fs = null;
        private bool disposed;

        public ExcelHelper()
        {

        }

        public ExcelHelper(string fileName)
        {
            this.fileName = fileName;
            disposed = false;
        }

        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 基于模板导出数据源中指定列
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="mubUrl"></param>
        /// <param name="context"></param>
        /// <param name="fileName"></param>
        public void ExportExcelall(DataTable dt, string mubUrl, HttpContextBase context, string fileName)
        {
            try
            {
                if (dt == null)
                {
                    return;
                }
                string road = context.Server.MapPath(mubUrl);
                FileInfo s = new FileInfo(road);

                using (FileStream stream = new FileStream(road, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    IWorkbook workbook = new HSSFWorkbook(stream);

                    ISheet sheet = workbook.GetSheetAt(0);

                    List<ICell> lisCell = sheet.GetRow(0).Cells;
                    for (int y = 0; y < dt.Rows.Count; y++)
                    {
                        IRow currentrow = sheet.CreateRow(y + 1);
                        for (int i = 0; i < lisCell.Count; i++)
                        {
                            string strTitle = "";
                            ICell cell = lisCell[i];
                            if (cell == null)
                                break;
                            string[] strFileName = lisCell[i].ToString().Trim().Split(',');
                            if (strFileName.Length == 2)
                            {
                                strTitle = strFileName[1];
                                if (!String.IsNullOrEmpty(strTitle) && dt.Rows[y][strTitle] != null)
                                {
                                    currentrow.HeightInPoints = 30;
                                    try
                                    {
                                        currentrow.CreateCell(i)
                                            .SetCellValue(dt.Rows[y][(strTitle)].ToString());
                                    }
                                    catch
                                    {
                                        currentrow.CreateCell(i).SetCellValue(dt.Rows[y][strTitle].ToString());
                                    }

                                }
                            }
                        }
                    }
                    for (int i = 0; i < lisCell.Count; i++)
                    {

                        ICell cell = lisCell[i];
                        if (cell == null)
                            break;
                        string[] strFileName = lisCell[i].ToString().Trim().Split(',');
                        if (strFileName.Length == 2)
                        {


                            if (!string.IsNullOrEmpty(strFileName[0]))
                                cell.SetCellValue(strFileName[0]);

                        }
                    }

                    using (MemoryStream ms = new MemoryStream())
                    {
                        workbook.Write(ms);
                        ms.Position = 0;
                        if (context.Request.Browser.Browser == "IE")
                            fileName = HttpUtility.UrlEncode(fileName);
                        context.Response.AddHeader("Content-Disposition", "attachment;fileName=" + fileName);
                        context.Response.BinaryWrite(ms.ToArray());
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }    

        }

        public bool ExportExcelallPost(DataTable dt, string mubUrl, HttpContextBase context, string fileName, string savefileName)
        {
            try
            {
                if (dt == null)
                {
                    return false;
                }
                string road = context.Server.MapPath(mubUrl);
                FileInfo s = new FileInfo(road);

                using (FileStream stream = new FileStream(road, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    IWorkbook workbook = new HSSFWorkbook(stream);

                    ISheet sheet = workbook.GetSheetAt(0);

                    List<ICell> lisCell = sheet.GetRow(0).Cells;
                    for (int y = 0; y < dt.Rows.Count; y++)
                    {
                        IRow currentrow = sheet.CreateRow(y + 1);
                        for (int i = 0; i < lisCell.Count; i++)
                        {
                            string strTitle = "";
                            ICell cell = lisCell[i];
                            if (cell == null)
                                break;
                            string[] strFileName = lisCell[i].ToString().Trim().Split(',');
                            if (strFileName.Length == 2)
                            {
                                strTitle = strFileName[1];
                                if (!String.IsNullOrEmpty(strTitle) && dt.Rows[y][strTitle] != null)
                                {
                                    currentrow.HeightInPoints = 30;
                                    try
                                    {
                                        currentrow.CreateCell(i)
                                            .SetCellValue(dt.Rows[y][Convert.ToInt32(strTitle)].ToString());
                                    }
                                    catch
                                    {
                                        currentrow.CreateCell(i).SetCellValue(dt.Rows[y][strTitle].ToString());
                                    }

                                }
                            }
                        }
                    }
                    for (int i = 0; i < lisCell.Count; i++)
                    {

                        ICell cell = lisCell[i];
                        if (cell == null)
                            break;
                        string[] strFileName = lisCell[i].ToString().Trim().Split(',');
                        if (strFileName.Length == 2)
                        {


                            if (!string.IsNullOrEmpty(strFileName[0]))
                                cell.SetCellValue(strFileName[0]);

                        }
                    }

                    using (FileStream sff = new FileStream(savefileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        workbook.Write(sff);
                        sff.Close();
                        sff.Dispose();
                    }
                    return true;
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    workbook.Write(ms);
                    //    ms.Position = 0;
                    //    if (context.Request.Browser.Browser == "IE")
                    //        fileName = HttpUtility.UrlEncode(fileName);
                    //    context.Response.AddHeader("Content-Disposition", "attachment;fileName=" + fileName);
                    //    context.Response.BinaryWrite(ms.ToArray());
                    //}

                }
            }
            catch (Exception)
            {
                return false;
            }



        }
        /// <summary>
        /// Excel复制行
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="sheet"></param>
        /// <param name="starRow"></param>
        /// <param name="rows"></param>
        private void insertRow(HSSFWorkbook wb, HSSFSheet sheet, int starRow, int rows)
        {
            /*
             * ShiftRows(int startRow, int endRow, int n, bool copyRowHeight, bool resetOriginalRowHeight); 
             * 
             * startRow 开始行
             * endRow 结束行
             * n 移动行数
             * copyRowHeight 复制的行是否高度在移
             * resetOriginalRowHeight 是否设置为默认的原始行的高度
             * 
             */

            sheet.ShiftRows(starRow, sheet.LastRowNum, rows, true, true);

            starRow = starRow - 1;

            for (int i = 0; i < rows; i++)
            {

                HSSFRow sourceRow = null;
                HSSFRow targetRow = null;
                HSSFCell sourceCell = null;
                HSSFCell targetCell = null;

                short m;

                starRow = starRow + 1;
                sourceRow = (HSSFRow)sheet.GetRow(starRow);
                targetRow = (HSSFRow)sheet.CreateRow(starRow + 1);
                targetRow.HeightInPoints = sourceRow.HeightInPoints;

                for (m = (short)sourceRow.FirstCellNum; m < sourceRow.LastCellNum; m++)
                {

                    sourceCell = (HSSFCell)sourceRow.GetCell(m);
                    targetCell = (HSSFCell)targetRow.CreateCell(m);

                    //targetCell.Encoding = sourceCell.Encoding;
                    targetCell.CellStyle = sourceCell.CellStyle;
                    targetCell.SetCellType(sourceCell.CellType);

                }
            }

        }

        #region Excel复制行

        /// <summary>
        /// Excel复制行
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="sheet"></param>
        /// <param name="starRow"></param>
        /// <param name="rows"></param>
        private void insertRow2(HSSFWorkbook wb, HSSFSheet sheet, int starRow, int rows)
        {
            /*
             * ShiftRows(int startRow, int endRow, int n, bool copyRowHeight, bool resetOriginalRowHeight); 
             * 
             * startRow 开始行
             * endRow 结束行
             * n 移动行数
             * copyRowHeight 复制的行是否高度在移
             * resetOriginalRowHeight 是否设置为默认的原始行的高度
             * 
             */

            sheet.ShiftRows(starRow + 1, sheet.LastRowNum, rows, true, true);

            starRow = starRow - 1;

            for (int i = 0; i < rows; i++)
            {

                HSSFRow sourceRow = null;
                HSSFRow targetRow = null;
                HSSFCell sourceCell = null;
                HSSFCell targetCell = null;

                short m;

                starRow = starRow + 1;
                sourceRow = (HSSFRow)sheet.GetRow(starRow);
                targetRow = (HSSFRow)sheet.CreateRow(starRow + 1);
                targetRow.HeightInPoints = sourceRow.HeightInPoints;

                for (m = (short)sourceRow.FirstCellNum; m < sourceRow.LastCellNum; m++)
                {

                    sourceCell = (HSSFCell)sourceRow.GetCell(m);
                    targetCell = (HSSFCell)targetRow.CreateCell(m);

                    //targetCell.Encoding = sourceCell.Encoding;
                    targetCell.CellStyle = sourceCell.CellStyle;
                    targetCell.SetCellType(sourceCell.CellType);

                }
            }

        }

        private void insertRowQuote(HSSFWorkbook wb, HSSFSheet sheet, int starRow, int rows)
        {
            /*
             * ShiftRows(int startRow, int endRow, int n, bool copyRowHeight, bool resetOriginalRowHeight); 
             * 
             * startRow 开始行
             * endRow 结束行
             * n 移动行数
             * copyRowHeight 复制的行是否高度在移
             * resetOriginalRowHeight 是否设置为默认的原始行的高度
             * 
             */

            sheet.ShiftRows(starRow + 1, sheet.LastRowNum, rows, true, true);

            starRow = starRow - 1;

            for (int i = 0; i < rows; i++)
            {

                HSSFRow sourceRow = null;
                HSSFRow targetRow = null;
                HSSFCell sourceCell = null;
                HSSFCell targetCell = null;

                short m;

                starRow = starRow + 1;
                sourceRow = (HSSFRow)sheet.GetRow(starRow);
                targetRow = (HSSFRow)sheet.CreateRow(starRow + 1);
                targetRow.HeightInPoints = sourceRow.HeightInPoints;

                for (m = (short)sourceRow.FirstCellNum; m < sourceRow.LastCellNum; m++)
                {

                    sourceCell = (HSSFCell)sourceRow.GetCell(m);
                    targetCell = (HSSFCell)targetRow.CreateCell(m);

                    //targetCell.Encoding = sourceCell.Encoding;
                    targetCell.CellStyle = sourceCell.CellStyle;
                    targetCell.SetCellType(sourceCell.CellType);

                }
            }

        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (fs != null)
                        fs.Close();
                }

                fs = null;
                disposed = true;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="excelFileStream"></param>
        /// <param name="typeExcel">0 xls 1 xlsx</param>
        /// <returns></returns>
        public System.Data.DataTable RenderFromExcel(Stream excelFileStream, int typeExcel)
        {
            using (excelFileStream)
            {
                IWorkbook workbook = null;
                if (typeExcel == 0)
                {
                    workbook = new HSSFWorkbook(excelFileStream);
                }
                else
                {
                    workbook = new XSSFWorkbook(excelFileStream);
                }
                ISheet sheet = workbook.GetSheetAt(0); //取第一个表

                DataTable
                    table = new DataTable();

                IRow
                    headerRow = sheet.GetRow(0); //第一行为标题行
                int cellCount
                    = headerRow.LastCellNum; //LastCellNum=PhysicalNumberOfCells
                int rowCount
                    = sheet.LastRowNum; //LastRowNum=PhysicalNumberOfRows - 1

                //handling header.
                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    DataColumn
                        column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    table.Columns.Add(column);
                }

                for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    if (row != null)
                    {
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                dataRow[j] = row.GetCell(j);
                        }
                    }

                    table.Rows.Add(dataRow);
                }
                return table;

            }
        }

 
        /// <summary>
        ///  获取指定EXCEL路径的工作表
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Microsoft.Office.Interop.Excel.Workbook GetWorkBook(string filePath)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                object missing = System.Reflection.Missing.Value;

                //note by wujianbo 20151110 打开模板
                Microsoft.Office.Interop.Excel.Workbook wb = excel.Application.Workbooks.Open(filePath, missing, missing, missing, missing, missing,
                missing, missing, missing, true, missing, missing, missing, missing, missing);
                return wb;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// create by wujianbo 20151110 填充EXCEL模板后关闭EXCEL
        /// update by wujianbo 20151127
        /// </summary>
        /// <param name="wb"></param>
        /// <returns></returns>
        private bool SetFillExcelReport(Microsoft.Office.Interop.Excel.Workbook wb)
        {
            try
            {
                object missing = System.Reflection.Missing.Value;
                wb.RefreshAll();
                wb.Save();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (wb.Application != null)
                {
                    //wb.Application.Workbooks.Close();
                    //wb.Application.Quit();
                    wb.Close();
                }
                //CloseExcelProcess(wb.Application);
            }
        }
    }

}



