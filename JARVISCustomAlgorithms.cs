using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JARVIS4
{
    public static class JARVISCustomAlgorithms
    {
        /// <summary>
        /// This function will split the Merimen Log File based on day
        /// </summary>
        /// <param name="file_path"></param>
        /// <param name="log_directory"></param>
        /// <returns></returns>
        public static delegate bool merimen_splitfile(string record);
        public static bool MerimenRequest_splitfile(string record)
        {
            Console.WriteLine(record + "hello");
            return true;
        }
        public static List<string> MerimenRequestStatistics_SplitFile(string file_path, string root_directory, string log_directory)
        {
            List<string> diagnostics = new List<string>();
            List<string> request_string_list = new List<string>();
            // Need to create a proper directory
            try
            {
                
                Directory.CreateDirectory(String.Format(@"{0}\{1}", root_directory, log_directory));
                FileStream target_file = File.Open(file_path, FileMode.Open, FileAccess.Read, FileShare.Read);
                BufferedStream target_file_buffered = new BufferedStream(target_file);
                StreamReader target_file_buffered_streamed = new StreamReader(target_file_buffered);
                string line;
                while ((line = target_file_buffered_streamed.ReadLine()) != null)
                {
                    
                    string[] columns = line.Split('\t').Select(x => x.Trim()).ToArray();
                    string datepart = columns[1].Split(' ')[0];
                    
                    DateTime record_date = new DateTime();
                    if(DateTime.TryParse(datepart,out record_date))
                    {
                        StreamWriter new_file = new StreamWriter(String.Format(@"{0}\{1}\{2}.txt", root_directory, log_directory, datepart), append: true);
                        new_file.WriteLine(line);
                        new_file.Close();
                        Console.WriteLine(line);
                    }
                    else
                    {
                        Console.WriteLine("Cannot convert to a date");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                diagnostics = new List<string>();
                diagnostics.Add(ex.ToString());
            }
            return diagnostics;
        }
        public static List<string> MerimenRequestStatistics_Analyze()
        {
            List<string> returned_list = new List<string>();
            try
            {
                DateTime start_date = Convert.ToDateTime("2017-08-01");
                DateTime end_date = Convert.ToDateTime("2017-08-31");
            }
            catch(Exception ex)
            {
                returned_list.Add(ex.ToString());
            }
            return returned_list;
        }
        /// <summary>
        /// This function will be used to analyse the stupidity that is sspTRXInsClmAssignGet
        /// </summary>
        /// <param name="file_path"></param>
        /// <returns></returns>
        public static List<string> sspTRXInsClmAssignGetAnalyzer(string file_path)
        {
            List<string> ruleset = new List<string>();
            try
            {
                // Find all line matches containing iGCOIDS
            }
            catch(Exception ex)
            {
                ruleset = new List<string>();
                ruleset.Add(ex.ToString());
            }
            return ruleset;
        }
    }
}
