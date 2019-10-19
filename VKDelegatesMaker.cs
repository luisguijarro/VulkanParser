using System;
using System.IO;
using System.Collections.Generic;

using VulkanParser.objetos;

namespace VulkanParser
{
    public static class VKDelegatesMaker
    {
        public static void VKDelegatesMake()
        {
            StreamWriter sw = File.CreateText(VKxmlParser.GetDestination() + "VKDelegates.cs"); // + key.Replace(".", "") + ".cs");
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

            sw.WriteLine("\t" + "internal static class VKDelegates"); //ABRIR CLASE
            sw.WriteLine("\t" + "{");

            CreateBaseMethods(sw);

            sw.WriteLine("\t" + "}"); //CERRAR CLASE
            sw.WriteLine("}");
            sw.Close();
        }

        private static void CreateBaseMethods(StreamWriter sw)
        {

            #region VK METHODS

            foreach (string key in VersionParser.d_commandsbyversion.Keys) //por versiones
            {
                sw.WriteLine("\t" + "\t" + "internal static class VK" + key.Replace(".", ""));
                sw.WriteLine("\t" + "\t" + "{");
                foreach (string keygroups in VersionParser.d_commandsbyversion[key].grupocomandos.Keys) //por grupos
                {
                    List<string> listametodos = VersionParser.d_commandsbyversion[key].grupocomandos[keygroups];
                    string s_grupo = keygroups.Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace(" ", "_");
                    string sp_grupo = char.ToUpper(s_grupo[0]) + s_grupo.Substring(1);
                    sw.WriteLine("\t" + "\t" + "\t" + "internal static class " + sp_grupo);  //Agrupar metodos en clases como grupos
                    sw.WriteLine("\t" + "\t" + "\t" + "{");

                    for (int i = 0; i < listametodos.Count; i++) //Recorrer Metodos del grupo
                    {
                        Metodo mtd = CommandParser.Metodos[listametodos[i]]; //Rescatar metodo del CommandParser
                        string s_metodo = "";

                        #region PASO 1: Valor Retornado y Apertura de Metodo
                        string valreturned = !mtd.ValueReturned.Contains("PFN_") ? mtd.ValueReturned : "IntPtr";

                        s_metodo = "internal unsafe delegate " + valreturned + " " + mtd.Nombre + "(";  //confecionar string de metodo

                        #endregion

                        #region PASO 2: Añadir Parametros al Metodo
                        List<string> referidos = new List<string>(); //controlar parametros referidos
                        foreach (string keyParams in mtd.Parametros.Keys)  //Añadir Parametros del método
                        {
                            Parametro param = mtd.Parametros[keyParams];
                            string stipovalor = param.TipoValor;
                            if (stipovalor == "VkAllocationCallbacks")
                            {
                                s_metodo += "VkAllocationCallbacks* " + param.Nombre + ", ";
                            }
                            else
                            {
                                if (HandleParser.handleTypes.Contains(stipovalor))
                                {
                                    stipovalor = "IntPtr";
                                }
                                if ((param.puntero) && (stipovalor != "char"))
                                {
                                    stipovalor += "*";
                                }
                                string svalorfinparam = Tools.VariableType(stipovalor);
                                if (param.esarray)
                                {
                                    if (stipovalor == "char")
                                    {
                                        svalorfinparam = "string";  //"char*";
                                    }
                                    if ((stipovalor != "char") && (!svalorfinparam.Contains("*")) && (!StructParser2.estructuras.ContainsKey(param.TipoValor)))
                                    {
                                        svalorfinparam += "*";
                                    }
                                }
                                if (param.Nombre == "event")
                                {
                                    param.Nombre = "@event";
                                }
                                if (param.puntero)
                                {
                                    if (StructParser2.estructuras.ContainsKey(param.TipoValor) || (stipovalor.Contains("IntPtr")))
                                    {
                                        if (!param.esarray)
                                        {
                                            s_metodo += "ref ";
                                            referidos.Add(keyParams);
                                            if (svalorfinparam.Contains("*"))
                                            {
                                                svalorfinparam = svalorfinparam.Replace("*", "");
                                            }
                                        }
                                    }
                                }
                                s_metodo += svalorfinparam + " " + param.Nombre + ", ";
                            }
                        }
                        #endregion
                        s_metodo = s_metodo.Remove(s_metodo.Length - 2, 2) + ");"; //quitar coma y espacio tras último parametro y cerrar parentesis.

                        sw.WriteLine("\t" + "\t" + "\t" + "\t" + s_metodo); //ESCRIBIR METODO

                        sw.WriteLine();

                    }
                    sw.WriteLine("\t" + "\t" + "\t" + "}");
                    sw.WriteLine();
                }
                sw.WriteLine("\t" + "\t" + "}");
                sw.WriteLine();
            }

            #endregion

        }
    }
}
