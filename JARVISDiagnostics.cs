using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace JARVIS4
{
    public static class JARVISDiagnostics
    {
        public static bool StartProcess(string process_path)
        {
            try
            {
                Process.Start(process_path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<string> list_JARVIS_types()
        {
            List<string> JARVIS_types = new List<string>();
            try
            {
                Assembly JARVIS_assembly = Assembly.GetExecutingAssembly();
                Type[] JARVIS_type_array = JARVIS_assembly.GetTypes();
                foreach(Type JARVIS_type in JARVIS_type_array)
                {
                    JARVIS_types.Add(JARVIS_type.FullName);
                }
            }
            catch(Exception ex)
            {
                JARVIS_types.Add(ex.ToString());
            }
            return JARVIS_types;
        }
        public static List<MethodInfo> list_JARVIS_type_methods(string type_name)
        {
            List<MethodInfo> type_method_list = new List<MethodInfo>();
            try
            {
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                type_method_list = null;
            }
            return type_method_list;
        }
        public static List<string> list_JARVIS_type_properties(string JARVIS_type_full_name)
        {
            List<string> JARVIS_type_properties = new List<string>();
            try
            {
                Type JARVIS_type = Type.GetType(JARVIS_type_full_name);
                if(JARVIS_type != null)
                {
                    FieldInfo[] JARVIS_type_field_array = JARVIS_type.GetFields();
                    PropertyInfo[] JARVIS_type_property_array = JARVIS_type.GetProperties();
                    MethodInfo[] JARVIS_type_method_array = JARVIS_type.GetMethods();
                    JARVIS_type_properties.Add(String.Format("Field Information for {0}", JARVIS_type_full_name));
                    foreach(FieldInfo JARVIS_type_field in JARVIS_type_field_array)
                    {
                        JARVIS_type_properties.Add(String.Format("{0} {1}", JARVIS_type_field.Name, JARVIS_type_field.FieldType));
                    }
                    JARVIS_type_properties.Add(String.Format("Property Information for {0}", JARVIS_type_full_name));
                    foreach(PropertyInfo JARVIS_type_property in JARVIS_type_property_array)
                    {
                        JARVIS_type_properties.Add(String.Format("{0} {1}", JARVIS_type_property.Name, JARVIS_type_property.PropertyType));
                    }
                    JARVIS_type_properties.Add(String.Format("Method Information for {0}", JARVIS_type_full_name));
                    foreach(MethodInfo JARVIS_type_method in JARVIS_type_method_array)
                    {
                        JARVIS_type_properties.Add(String.Format("{0} returns {1}", JARVIS_type_method.Name, JARVIS_type_method.ReturnType));
                    }
                }
                else
                {
                    JARVIS_type_properties.Add(String.Format("Unable to find JARVIS type {0}", JARVIS_type_full_name));
                }
            }
            catch(Exception ex)
            {

            }
            return JARVIS_type_properties;
        }
        /// <summary>
        /// Run a single JARVIS function
        /// </summary>
        /// <param name="namespace_name"></param>
        /// <param name="type_name"></param>
        /// <param name="method_name"></param>
        /// <param name="method_parameters"></param>
        /// <returns></returns>
        public static bool run_JARVIS_function(string namespace_name, string type_name, string method_name, string method_parameters)
        {
            try
            {
                Type target_type = Type.GetType(String.Format("{0}.{1}", namespace_name, type_name));
                if (target_type != null)
                {
                    MethodInfo target_method = target_type.GetMethod(method_name);
                    if (target_method != null)
                    {
                        List<string> method_parameter_list = method_parameters.Split('|').Select(x => x.Trim()).ToList();
                        List<object> converted_method_parameter_list = new List<object>();
                        ParameterInfo[] target_method_parameters = target_method.GetParameters();
                        if (method_parameter_list.Count != target_method_parameters.Length && target_method_parameters.Length > 0)
                        {
                            Console.WriteLine("Insufficient Parameters for method {0}.{1}", type_name, method_name);
                            return false;
                        }
                        else
                        {
                            for (int i = 0; i < target_method_parameters.Length; i++)
                            {
                                converted_method_parameter_list.Add(Convert.ChangeType(method_parameter_list[i], target_method_parameters[i].ParameterType));
                            }
                            if (target_method.IsStatic)
                            {
                                target_method.Invoke(null, converted_method_parameter_list.ToArray());
                            }
                            else
                            {
                                object target_type_instance = Activator.CreateInstance(target_type);
                                target_method.Invoke(target_type_instance, converted_method_parameter_list.ToArray());
                            }
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// Runs a JARVIS function
        /// Will create an instance of the type if the type needs a constructor
        /// </summary>
        /// <param name="type_name"></param>
        /// <param name="function_name"></param>
        /// <param name="function_parameters"></param>
        /// <returns></returns>
        public static bool run_JARVIS_function(string type_name, string function_name, object[] function_parameters)
        {
            try
            {
                Type JARVIS_type = Type.GetType(String.Format("JARVIS4.{0}", type_name));
                if (JARVIS_type != null)
                {
                    MethodInfo JARVIS_type_method = JARVIS_type.GetMethod(function_name);
                    if (JARVIS_type_method != null)
                    {
                        foreach (ParameterInfo parameters in JARVIS_type_method.GetParameters())
                        {
                            Console.WriteLine("{0} {1}", parameters.Name, parameters.ParameterType);
                        }
                        // Split string, and convert to the necessary argument types
                        JARVIS_type_method.Invoke(null, function_parameters);
                    }
                    else
                    {
                        Console.WriteLine("Unable to find method");
                    }
                }
                else
                {
                    Console.WriteLine("Unable to find type");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public static string get_JARVIS_executable_path()
        {
            string JARVIS_executable_path = "";
            try
            {
                Assembly JARVIS_assembly = Assembly.GetExecutingAssembly();
                JARVIS_executable_path = System.IO.Path.GetDirectoryName(JARVIS_assembly.Location);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return JARVIS_executable_path;
        }
        public static object[] string_to_parameter_list(string type_name, string method_name, string parameters)
        {
            try
            {
                List<string> parameter_list = parameters.Split('|').Select(x => x.Trim()).ToList();
                Type JARVIS_type = Type.GetType(String.Format("JARVIS4.{0}", type_name));
                if (JARVIS_type != null)
                {
                    MethodInfo target_method = JARVIS_type.GetMethod(method_name);
                    if (target_method != null)
                    {
                        ParameterInfo[] target_method_parameters = target_method.GetParameters();
                        if (target_method_parameters.Length != parameter_list.Count)
                        {
                            return null;
                        }
                        else
                        {
                            List<object> object_list = new List<object>();
                            for (int i = 0; i < target_method_parameters.Length; i++)
                            {
                                object_list.Add(Convert.ChangeType(parameter_list[i], target_method_parameters[i].ParameterType));
                            }
                            return object_list.ToArray();
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }   
        }
        public static bool create_JARVIS_type()
        {
            try
            {
                Assembly JARVIS_assembly = Assembly.GetExecutingAssembly();
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
        public static List<string> get_executable_list()
        {
            return new List<string>();
        }
    }
}
