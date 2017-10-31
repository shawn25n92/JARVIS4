using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Threading;

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
        public static List<string> MerimenRequestStatistics_Analyze(string file_path)
        {
            List<string> returned_list = new List<string>();
            try
            {
                DateTime start_date = Convert.ToDateTime("2017-08-01");
                DateTime end_date = Convert.ToDateTime("2017-08-31");
                while (start_date <= end_date)
                {
                    Dictionary<string, int> requests_per_hour = new Dictionary<string, int>();
                    Dictionary<string, Dictionary<string, int>> unique_sessions_per_hour = new Dictionary<string, Dictionary<string, int>>();
                    Dictionary<string, Dictionary<string, int>> unique_users_per_hour = new Dictionary<string, Dictionary<string, int>>();
                    Dictionary<string, int> unique_sessions_temp = new Dictionary<string, int>();
                    Dictionary<string, int> fusebox_fuseaction_count = new Dictionary<string, int>();
                    List<string> suspicious_fusebox_fuseaction_list = new List<string>();
                    string file_name = start_date.ToString("yyyy-MM-dd");
                    File.Create(String.Format(String.Format(@"{0}\{1}_analytics.txt", file_path, file_name))).Close();
                    File.Create(String.Format(String.Format(@"{0}\{1}_request_analytics.txt", file_path, file_name))).Close();
                    StreamWriter date_analytics = new StreamWriter(String.Format(@"{0}\{1}_analytics.txt", file_path, file_name), append: true);
                    StreamWriter request_analytics = new StreamWriter(String.Format(@"{0}\{1}_request_analytics.txt", file_path, file_name), append: true);


                    FileStream target_file = File.Open(String.Format(@"{0}\{1}.txt", file_path, file_name), FileMode.Open, FileAccess.Read, FileShare.Read);
                    BufferedStream target_file_buffered = new BufferedStream(target_file);
                    StreamReader target_file_buffered_streamed = new StreamReader(target_file_buffered);
                    string line;
                    int count = 0;
                    while ((line = target_file_buffered_streamed.ReadLine()) != null)
                    {

                        List<string> parameter_list = line.Split('\t').ToList();
                        string datepart = parameter_list[1].Split(' ')[0];
                        string timepart = parameter_list[1].Split(' ')[1];
                        string hour = timepart.Split(':')[0];
                        if (!unique_sessions_per_hour.ContainsKey(hour))
                        {
                            unique_sessions_per_hour.Add(hour, new Dictionary<string, int>());
                        }
                        if (!unique_users_per_hour.ContainsKey(hour))
                        {
                            unique_users_per_hour.Add(hour, new Dictionary<string, int>());
                        }
                        if (parameter_list.ElementAtOrDefault(4) != null)
                        {
                            string username = parameter_list.ElementAtOrDefault(4);
                            if (!unique_users_per_hour[hour].ContainsKey(username) && username.Trim().Length > 0)
                            {
                                unique_users_per_hour[hour].Add(username, 0);
                            }
                        }
                        if (parameter_list.ElementAtOrDefault(5) != null)
                        {
                            string querystring = parameter_list[5].ToLower();
                            List<string> url_parameter_list = querystring.Split('&').ToList();
                            if (url_parameter_list.Any(x => x.Contains("cfid")) && url_parameter_list.Any(x => x.Contains("cftoken")))
                            {
                                string cfid = url_parameter_list.Where(x => x.Contains("cfid")).FirstOrDefault();
                                string cftoken = url_parameter_list.Where(x => x.Contains("cftoken")).FirstOrDefault();
                                if (!unique_sessions_per_hour[hour].ContainsKey(String.Format("{0}.{1}", cfid, cftoken)))
                                {
                                    unique_sessions_per_hour[hour].Add(String.Format("{0}.{1}", cfid, cftoken), 0);
                                }
                            }
                            if (url_parameter_list.Any(x => x.Contains("fusebox")) && url_parameter_list.Any(x => x.Contains("fuseaction")))
                            {
                                string fusebox = url_parameter_list.Where(x => x.Contains("fusebox")).FirstOrDefault();
                                string fuseaction = url_parameter_list.Where(x => x.Contains("fuseaction")).FirstOrDefault();
                                string fusebox_fuseaction = String.Format("{0}&{1}", fusebox, fuseaction);
                                Console.WriteLine(fusebox_fuseaction);
                                if (fusebox_fuseaction_count.ContainsKey(fusebox_fuseaction))
                                {
                                    fusebox_fuseaction_count[fusebox_fuseaction] = fusebox_fuseaction_count[fusebox_fuseaction] + 1;
                                }
                                else
                                {
                                    fusebox_fuseaction_count.Add(fusebox_fuseaction, 1);
                                }
                            }
                        }
                        if (requests_per_hour.ContainsKey(hour))
                        {
                            requests_per_hour[hour] = requests_per_hour[hour] + 1;
                        }
                        else
                        {
                            requests_per_hour.Add(hour, 1);
                        }
                        // Requests per hour
                        count++;
                    }
                    date_analytics.WriteLine("------------------------------------------------------------");
                    date_analytics.WriteLine("Requests per hour");
                    date_analytics.WriteLine("------------------------------------------------------------");
                    foreach (KeyValuePair<string, int> request_count in requests_per_hour)
                    {
                        date_analytics.WriteLine("{0}\t{1} requests", request_count.Key, request_count.Value);
                    }
                    date_analytics.WriteLine("------------------------------------------------------------");
                    date_analytics.WriteLine("Unique Sessions Per Hour");
                    date_analytics.WriteLine("------------------------------------------------------------");
                    foreach (KeyValuePair<string, Dictionary<string, int>> unique_session_count in unique_sessions_per_hour)
                    {
                        date_analytics.WriteLine("{0}\t{1} sessions", unique_session_count.Key, unique_session_count.Value.Count);
                    }
                    date_analytics.WriteLine("------------------------------------------------------------");
                    date_analytics.WriteLine("Unique Users Per Hour");
                    date_analytics.WriteLine("------------------------------------------------------------");
                    foreach (KeyValuePair<string, Dictionary<string, int>> unique_user_count in unique_users_per_hour)
                    {
                        date_analytics.WriteLine("{0}\t{1} unique users", unique_user_count.Key, unique_user_count.Value.Count);
                    }
                    date_analytics.WriteLine("------------------------------------------------------------");
                    date_analytics.WriteLine("Fusebox and Fuseaction Tally");
                    date_analytics.WriteLine("------------------------------------------------------------");
                    foreach (KeyValuePair<string, int> unique_fusebox_fuseaction in fusebox_fuseaction_count)
                    {
                        date_analytics.WriteLine("{0}\t{1} requests", unique_fusebox_fuseaction.Key, unique_fusebox_fuseaction.Value);
                    }

                    date_analytics.Close();
                    start_date = start_date.AddDays(1);
                    target_file.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
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

        public static StatusObject GetFuseboxFuseactionList(string ClaimsPath)
        {
            StatusObject SO = new StatusObject();
            try
            {
                Directory.CreateDirectory(@"C:\JARVIS4\CustomAlgorithmOutputs\FuseboxFuseaction");
                // Get the claims development folder
                StreamReader claims_index = new StreamReader(String.Format(@"{0}\index.cfm", ClaimsPath));
                Dictionary<string, Dictionary<string, int>> fusebox_fuseaction_dictionary = new Dictionary<string, Dictionary<string, int>>();
                File.Create(String.Format(@"C:\JARVIS4\CustomAlgorithmOutputs\FuseboxFuseaction\ValidFuseboxFuseaction.txt")).Close();
                string line;
                List<string> code_list = new List<string>();
                List<string> filtered_code_list = new List<string>();
                while((line = claims_index.ReadLine())!= null)
                {
                    code_list.Add(line);
                }
                // For MTRRoot/AppRoot, it will re-invoke claims/index.cfc to redirect to the proper pages. may have to take that into account as well.
                bool navigate_fuseaction = false;
                for(int i = 0; i < code_list.Count; i++)
                {
                    string fusebox = code_list[i].ToLower();
                    if (fusebox.Contains("mtrroot,approot"))
                    {
                        navigate_fuseaction = true;
                    }
                    if (fusebox.Contains("<cfcase"))
                    {
                        string filtered_fusebox = fusebox.Replace("<!---", "").Replace("--->", "").Replace("<", "").Replace("cfcase value=\"", "").Replace("\">", "").Trim();
                        filtered_code_list.Add(filtered_fusebox);
                        if (navigate_fuseaction)
                        {
                            Console.WriteLine("\t{0}", filtered_fusebox);
                        }
                        else
                        {
                            Console.WriteLine(filtered_fusebox);
                        }
                        int current_line = i;
                        for(current_line = i; current_line < code_list.Count; current_line++)
                        {
                            string target;
                            string indentation;
                            string target_fuseaction = "";
                            if (navigate_fuseaction)
                            {
                                target = "cfinvoke";
                                indentation = "\t\t";
                            }
                            else
                            {
                                target = "cfinclude";
                                indentation = "\t";
                            }
                            if (code_list[current_line].Contains(target))
                            {
                                if(target == "cfinclude")
                                {
                                    // Navigate to that specific index and do the checking for fuseactions
                                    string current_cfinclude = code_list[current_line].ToLower();
                                    string filtered_cfinclude = current_cfinclude.Replace("/", "\\").Replace("<cfinclude template=\"", "").Replace("\">", "").Trim();
                                    Console.WriteLine("{0}{1}", indentation, filtered_cfinclude);
                                    try
                                    {
                                        StreamReader target_index = new StreamReader(String.Format(@"{0}\{1}", ClaimsPath, filtered_cfinclude));
                                        File.Create(String.Format(String.Format(@"C:\JARVIS4\CustomAlgorithmOutputs\FuseboxFuseaction\{0}.txt", filtered_fusebox))).Close();
                                        StreamWriter output = new StreamWriter(String.Format(@"C:\JARVIS4\CustomAlgorithmOutputs\FuseboxFuseaction\{0}.txt", filtered_fusebox), append: true);
                                        StreamWriter valid_fusebox_fuseaction = new StreamWriter(@"C:\JARVIS4\CustomAlgorithmOutputs\FuseboxFuseaction\ValidFuseboxFuseaction.txt", append: true);
                                        string fuseaction_line;
                                        while((fuseaction_line = target_index.ReadLine()) != null)
                                        {
                                            if (fuseaction_line.Contains("<cfcase"))
                                            {
                                                string filtered_fuseaction_line = fuseaction_line.ToLower();
                                                string cleansed_fuseaction_line = filtered_fuseaction_line.Replace("<", "").Replace("cfcase", "").Replace("\"", "").Replace("value=", "").Replace(">", "").Replace("!", "").Replace("-", "").Replace("\t", "").Trim();
                                                output.WriteLine(cleansed_fuseaction_line);
                                                valid_fusebox_fuseaction.WriteLine("fusebox={0}&fuseaction={1}", filtered_fusebox, cleansed_fuseaction_line);
                                            }
                                        }
                                        //output.Write(target_index.ReadToEnd());
                                        valid_fusebox_fuseaction.Close();
                                        output.Close();
                                        target_index.Close();
                                    }
                                    catch(Exception ex)
                                    {
                                        Console.WriteLine(ex.ToString());
                                        continue;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("{0}{1}", indentation, code_list[current_line].Trim());
                                }
                                break;
                            }
                        }
                    }
                }
                Console.WriteLine(filtered_code_list.Count);
                claims_index.Close();
            }
            catch(Exception e)
            {
                SO = new StatusObject(e.Message, "GetFuseboxFuseactionList", StatusObject.StatusCode.FAILURE, e.ToString());
            }
            return SO;
        }

        public static StatusObject GetCFStoredProcCalls(string DevelopmentFolderPath)
        {
            StatusObject SO = new StatusObject();
            try
            {
                
            }
            catch(Exception e)
            {

            }
            return SO;
        }
    }
}
