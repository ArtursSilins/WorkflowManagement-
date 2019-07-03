using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SaveTo
{
    public class Excel
    {
        public void CreateFile(IExcelRepository excelRepository, string path)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                return;
            }

            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            Save(excelRepository, xlWorkSheet);
            xlWorkSheet.Columns.AutoFit();

            xlApp.DisplayAlerts = false;
            xlWorkBook.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue,
            misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);


            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);
        }
        private void Save(IExcelRepository excelRepository, Microsoft.Office.Interop.Excel.Worksheet worksheet)
        {
            int counter = 0; int a = 0; int b = 0;

            foreach (DataRow row in excelRepository.GetData().Rows)
            {
                a++; b++;
                counter++;
                if (counter == 1)
                {
                    worksheet.Cells[a, b] = "Item";
                    b++;
                    worksheet.Cells[a, b] = "Operation";
                    b++;
                    worksheet.Cells[a, b] = "Sart Time";
                    b++;
                    worksheet.Cells[a, b] = "End Time";
                    b++;
                    worksheet.Cells[a, b] = "Count";
                    b = 0;
                    a++; b++;
                }
                worksheet.Cells[a, b] = row["Item"].ToString();
                b++;
                worksheet.Cells[a, b] = row["Name"].ToString();
                b++;
                worksheet.Cells[a, b] = row["StartTime"].ToString();
                b++;
                worksheet.Cells[a, b] = row["EndTime"].ToString();
                b++;
                worksheet.Cells[a, b] = row["Count"].ToString();
                b = 0;
            }
        }
    }
}
