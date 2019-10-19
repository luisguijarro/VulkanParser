using System;
using System.IO;
using System.Collections.Generic;

using VulkanParser.objetos;

namespace VulkanParser
{
    public static class VKDelegatorMaker
    {
        public static void VKDelegatorMake()
        {
            StreamWriter sw = File.CreateText(VKxmlParser.GetDestination() + "VKDelegatesTools.cs"); // + key.Replace(".", "") + ".cs");
            sw.WriteLine("// Document Created with VulkanParser.");
            sw.WriteLine("//       " + DateTime.Now.ToString("HH:mm:ss dd/mm/yyyy"));
            sw.WriteLine("// by BROTHERHOOD OF THE BLACK SWORD.");
            sw.WriteLine();
            sw.WriteLine("using System;");
            sw.WriteLine("using System.Runtime.InteropServices;");
            sw.WriteLine("using " + VKxmlParser.GetNamespace() + ";");
            sw.WriteLine();
            sw.WriteLine("namespace " + VKxmlParser.GetNamespace());
            sw.WriteLine("{");

            sw.WriteLine("\t" + "internal static class VKDelegator");
            sw.WriteLine("\t" + "{");
            sw.WriteLine("\t" + "\t" + "internal static void InitDelegatesVK(VKDevice vkdevice)");
            sw.WriteLine("\t" + "\t" + "{");
            sw.WriteLine("\t" + "\t" + "\t" + "IntPtr ptr_mehod;");
            foreach (string key in VersionParser.d_commandsbyversion.Keys) //por versiones
            {
                foreach (string keygroups in VersionParser.d_commandsbyversion[key].grupocomandos.Keys) //por grupos
                {
                    string s_grupo = keygroups.Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace(" ", "_");
                    string sp_grupo = char.ToUpper(s_grupo[0]) + s_grupo.Substring(1);
                    List<string> listametodos = VersionParser.d_commandsbyversion[key].grupocomandos[keygroups];
                    for (int i = 0; i < listametodos.Count; i++) //Recorrer Metodos
                    {
                        Metodo mtd = CommandParser.Metodos[listametodos[i]]; //Rescatar metodo del CommandParser

                        sw.WriteLine("\t" + "\t" + "\t" + "ptr_mehod = GetVKMethodAdress(vkdevice, \"" + mtd.Nombre + "\");");
                        sw.WriteLine("\t" + "\t" + "\t" + "if (ptr_mehod != IntPtr.Zero)");
                        sw.WriteLine("\t" + "\t" + "\t" + "{");
                        string s_metodo = "";
                        s_metodo = "vkdevice.VK" + key.Replace(".", "") + "." + sp_grupo + ".p_" + mtd.Nombre + " = (" + VKxmlParser.GetNamespace() + ".VKDelegates.VK" + key.Replace(".", "") + "." + sp_grupo + "." + mtd.Nombre + ")";

                        s_metodo += "Marshal.GetDelegateForFunctionPointer<" + VKxmlParser.GetNamespace() + ".VKDelegates.VK" + key.Replace(".", "") + "." + sp_grupo + "." + mtd.Nombre + " >(ptr_mehod);";
                        sw.WriteLine("\t" + "\t" + "\t" + "\t" + s_metodo); //Escribir Fila De Declaracion de delegado.
                        sw.WriteLine("\t" + "\t" + "\t" + "}");
                    }
                }
                sw.WriteLine();
            }

            sw.WriteLine("\t" + "\t" + "}");

            GetAdress(sw); //Escribit metodo de obtención de direccion de metodo.

            sw.WriteLine("\t" + "}");
            sw.WriteLine("}");
            sw.WriteLine();
            sw.Close();
        }
    
        public static void GetAdress(StreamWriter sw)
        {
            sw.WriteLine("\t" + "\t" + "public static unsafe IntPtr GetVKMethodAdress(VKDevice vkdevice, string metodo)");
            sw.WriteLine("\t" + "\t" + "{");
            sw.WriteLine("\t" + "\t" + "\t" + "IntPtr p_ret = IntPtr.Zero;");
            sw.WriteLine("\t" + "\t" + "\t" + "p_ret = " + VKxmlParser.GetNamespace() + ".VK10.Device_initialization.vkGetDeviceProcAddr(vkdevice.Handle, metodo);");
            sw.WriteLine("#if DEBUG");
            sw.WriteLine("\t" + "\t" + "\t" + "if (p_ret != IntPtr.Zero){ Console.WriteLine(metodo);}");
            sw.WriteLine("#endif");
            sw.WriteLine("\t" + "\t" + "\t" + "return p_ret;");
            sw.WriteLine("\t" + "\t" + "}");
        }
    }
}
