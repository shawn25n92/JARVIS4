using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Access;
using System.Runtime;

namespace JARVIS4
{
    /// <summary>
    /// This class is purely for reading of massive text files
    /// </summary>
    public static class JARVISFileProcessing
    {
        private static string FileDirectory = @"C:\JARVIS4\UserDefinedFiles";
        public static bool read_large_text_file(string file_path)
        {
            try
            {
                FileStream target_file = File.Open(file_path, FileMode.Open, FileAccess.Read, FileShare.Read);
                BufferedStream target_file_buffered = new BufferedStream(target_file);
                StreamReader target_file_buffered_stream = new StreamReader(target_file_buffered);
                string line;
                while ((line = target_file_buffered_stream.ReadLine()) != null)
                {
                    // Do some logic here
                    Console.WriteLine(line);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool read_large_text_file(string file_path, params Func<string, bool>[] function_array)
        {
            try
            {
                FileStream target_file = File.Open(file_path, FileMode.Open, FileAccess.Read, FileShare.Read);
                BufferedStream target_file_buffered = new BufferedStream(target_file);
                StreamReader target_file_buffered_stream = new StreamReader(target_file_buffered);
                string line;
                while ((line = target_file_buffered_stream.ReadLine()) != null)
                {
                    // Do some logic here
                    foreach (Func<string, bool> function in function_array)
                    {
                        function(line);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool ReadExcelFile(string file_path)
        {
            try
            {
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public static StatusObject WriteExcelFile()
        {
            StatusObject SO = new StatusObject();
            try
            {
                Microsoft.Office.Interop.Excel.Application oXL;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Microsoft.Office.Interop.Excel._Worksheet oSheet;
                Microsoft.Office.Interop.Excel.Range oRng;
                object misvalue = System.Reflection.Missing.Value;

                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                //Get a new workbook.
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "First Name";
                oSheet.Cells[1, 2] = "Last Name";
                oSheet.Cells[1, 3] = "Full Name";
                oSheet.Cells[1, 4] = "Salary";

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "D1").Font.Bold = true;
                oSheet.get_Range("A1", "D1").VerticalAlignment =
                    Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                // Create an array to multiple values at once.
                string[,] saNames = new string[5, 2];

                saNames[0, 0] = "John";
                saNames[0, 1] = "Smith";
                saNames[1, 0] = "Tom";

                saNames[4, 1] = "Johnson";

                //Fill A2:B6 with an array of values (First and Last Names).
                oSheet.get_Range("A2", "B6").Value2 = saNames;

                //Fill C2:C6 with a relative formula (=A2 & " " & B2).
                oRng = oSheet.get_Range("C2", "C6");
                oRng.Formula = "=A2 & \" \" & B2";

                //Fill D2:D6 with a formula(=RAND()*100000) and apply format.
                oRng = oSheet.get_Range("D2", "D6");
                oRng.Formula = "=RAND()*100000";
                oRng.NumberFormat = "$0.00";

                //AutoFit columns A:D.
                oRng = oSheet.get_Range("A1", "D1");
                oRng.EntireColumn.AutoFit();

                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs("c:\\test\\test505.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();
            }
            catch(Exception e)
            {
                SO = new StatusObject(e.Message, "WriteToExcel", StatusObject.StatusCode.FAILURE, e.ToString());
            }
            return SO;
        }

        public static StatusObject CompareFileHashes(string OriginalText, string NewText)
        {
            StatusObject SO = new StatusObject();
            try
            {
                MD5 hashGenerator = MD5.Create();
            }
            catch(Exception e)
            {
                SO = new StatusObject(e.Message, "FILEHASHMISMATCH", StatusObject.StatusCode.FAILURE, e.ToString());
            }
            return SO;
        }
    }
}
