using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace JARVIS4
{
    class Program
    {
        /// <summary>
        /// Handle all JARVIS logic
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Dictionary<string, Type> JARVIS_types = new Dictionary<string, Type>();
            Dictionary<string, MethodInfo> JARVIS_methods = new Dictionary<string, MethodInfo>();
            List<Thread> available_threads = new List<Thread>();

            bool program_running = true;
            string user_input = "";
            Console.Write("Enter a command: ");
            user_input = Console.ReadLine();

            while (program_running)
            {
                JARVISLearning.log_command(JARVISDiagnostics.get_JARVIS_executable_path(), "Logs", "Logfile.txt", user_input);
                List<string> command_parameters = user_input.Split(' ').ToList();
                string primary_command = command_parameters[0].Trim().ToLower();
                try
                {
                    if (primary_command != "exit")
                    {
                        if (primary_command == "start")
                        {
                            if (command_parameters.ElementAtOrDefault(1) != null)
                            {
                                string secondary_command = command_parameters[1];
                                if (secondary_command == "process")
                                {
                                    string path = user_input.Remove(0, "start process".Length).Trim();
                                    if (path.Length > 0)
                                    {
                                        Console.WriteLine("Starting process {0}", path);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid process path/name");
                                    }
                                    JARVISDiagnostics.StartProcess(path);
                                }
                                else
                                {
                                    Console.WriteLine("Unable to perform operation \"start\". Incorrect secondary command");
                                }
                            }
                            else
                            {

                            }
                        }
                        else if (primary_command == "new")
                        {
                            if (command_parameters.ElementAtOrDefault(1) != null)
                            {
                                string secondary_command = command_parameters[1];
                                if (secondary_command == "class")
                                {

                                }
                                else
                                {
                                    Console.WriteLine("Unable to perform operation \"start\". Incorrect secondary command");
                                }
                            }
                        }
                        else if (primary_command == "diagnostics")
                        {
                            JARVISConsole.list_to_console(JARVISDiagnostics.list_JARVIS_types());
                            JARVISConsole.list_to_console(JARVISDiagnostics.list_JARVIS_type_properties("JARVIS4.JARVISLearning"));
                            Console.WriteLine("Path = {0}", JARVISDiagnostics.get_JARVIS_executable_path());
                        }
                        else if (primary_command == "read")
                        {
                            JARVISCustomAlgorithms.MerimenRequestStatistics_Analyze(@"C:\Users\shawn\Documents\MERIMEN CUSTOMIZATIONS\#22773\Split Files");
                        }
                        else if (primary_command == "customalgo")
                        {
                            Console.WriteLine("JARVIS Custom Algorithms");
                            JARVISConsole.list_to_console(JARVISDiagnostics.list_JARVIS_type_properties("JARVIS4.JARVISCustomAlgorithms"));
                        }
                        else if (primary_command == "fileprocessing")
                        {
                            if (command_parameters.ElementAtOrDefault(1) != null)
                            {
                                string file_path = user_input.Replace("fileprocessing","").Trim();
                                JARVISFileProcessing.read_large_text_file(file_path);
                            }
                            else
                            {
                                Console.WriteLine("Unable to perform operation \"textprocessing\". File name not provided");
                            }
                        }
                        else if (primary_command == "run")
                        {
                            if(command_parameters.ElementAtOrDefault(1) != null)
                            {
                                string type_name = command_parameters[1];
                                string function_name = command_parameters.ElementAtOrDefault(2);
                                string function_parameters = command_parameters.ElementAtOrDefault(3);
                                //JARVISDiagnostics.run_JARVIS_function(type_name, function_name, function_parameters);
                                //object[] arguments = JARVISDiagnostics.string_to_parameter_list(type_name, function_name, function_parameters);
                                //JARVISDiagnostics.run_JARVIS_function(type_name, function_name, arguments);
                                JARVISDiagnostics.run_JARVIS_function("JARVIS4", type_name, function_name, function_parameters);
                            }
                            else
                            {
                                Console.WriteLine("Unable to peform operation \"run\". Type name not provided");
                            }
                        }
                        else if (primary_command == "thread")
                        {
                            string secondary_command = command_parameters.ElementAtOrDefault(1);
                            if(secondary_command != null)
                            {
                                if (secondary_command == "add")
                                {
                                    Thread new_thread = new Thread(new ThreadStart(JARVISCustomAlgorithms.randomalgo3));
                                    new_thread.Start();
                                    Thread.Sleep(10000);
                                    new_thread.Abort();
                                }
                                else if (secondary_command == "stop")
                                {

                                }
                                else if (secondary_command == "stopall")
                                {

                                }
                            }
                            else
                            {
                                Console.WriteLine("Unable to perform operation \"add\". Insufficient Information Provided");
                            }
                        }
                        else if (primary_command == "datasource")
                        {
                            string secondary_command = command_parameters.ElementAtOrDefault(1);
                            string server_name = command_parameters.ElementAtOrDefault(2);
                            string database_name = command_parameters.ElementAtOrDefault(3);
                            string database_user_id = command_parameters.ElementAtOrDefault(4);
                            string database_password = command_parameters.ElementAtOrDefault(5);
                            if (secondary_command != null && secondary_command.Length > 0)
                            {
                                if (secondary_command == "add")
                                {
                                    StatusObject SO_dbconnect = JARVISDataSourceManager.ConnectToDataSource(server_name, database_name, database_user_id, database_password);
                                    if(SO_dbconnect.Status == StatusObject.StatusCode.FAILURE)
                                    {
                                        Console.WriteLine(SO_dbconnect.ErrorStacktrace);
                                    }
                                }
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Unable to perform operation \"start\". Insufficient information provided");
                        }
                        Console.Write("Enter a command: ");
                        user_input = Console.ReadLine();
                    }
                    else
                    {
                        program_running = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.Write("Enter a command: ");
                    user_input = Console.ReadLine();
                    program_running = true;
                }
            }
        }
    }
}
