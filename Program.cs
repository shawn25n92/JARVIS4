using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace JARVIS4
{
    class Program
    {
        /// <summary>
        /// Handle all JARVIS logic
        /// </summary>
        /// <param name="args"></param>
        public static JARVISDiagnostics program_diagnostics = new JARVISDiagnostics();
        static void Main(string[] args)
        {
            bool program_running = true;
            string user_input = "";
            Console.Write("Enter a command: ");
            user_input = Console.ReadLine();
            while (program_running)
            {
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
                                    program_diagnostics.StartProcess(path);
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
                        if (primary_command == "new")
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
                    program_running = true;
                }
            }
        }
    }
}
