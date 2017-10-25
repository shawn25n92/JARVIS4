using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace JARVIS4
{
    /// <summary>
    /// This class is purely for reading of massive text files
    /// </summary>
    public static class JARVISFileProcessing
    {
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
        public static bool read_excel_file(string file_path)
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
        public static StatusObject CompareFileHashes()
        {
            StatusObject SO = new StatusObject();
            try
            {

            }
            catch(Exception e)
            {
                SO = new StatusObject(e.Message, "FILEHASHMISMATCH", StatusObject.StatusCode.FAILURE, e.ToString());
            }
            return SO;
        }
    }
}
