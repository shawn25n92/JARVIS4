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
        public static List<string> MerimenRequestStatistics(string file_path)
        {
            List<string> diagnostics = new List<string>();
            List<string> request_string_list = new List<string>();
            try
            {
                StreamReader source_file = new StreamReader(file_path);
                
                string file_data = source_file.ReadToEnd();
                request_string_list = file_data.Split('\n').ToList();
                while ((file_data = source_file.ReadLine()) != null)
                {
                    Console.WriteLine(file_data);
                }
                /*foreach (string request_string in request_string_list)
                {
                    // check if the query string contains fusebox and fuseaction
                    List<string> query_parameters = request_string.Split('&').ToList();
                    if (request_string.Contains("fusebox") && request_string.Contains("fuseaction"))
                    {
                        // Do something with it
                    }
                }*/
                source_file.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                diagnostics = new List<string>();
                diagnostics.Add(ex.ToString());
            }
            return diagnostics;
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
