using System;
using System.IO;
using System.Collections.Generic;

using VulkanParser.objetos;

namespace VulkanParser
{
    /// <summary>
    /// Description of CreateMethodsCS.
    /// </summary>
    public static class CreaterMethodsCS3
    {
        public static void CreateMethodsCS()
        {
            foreach (string key in VersionParser.d_commandsbyversion.Keys) //por versiones
            {
                StreamWriter sw = File.CreateText(VKxmlParser.GetDestination() + "Vulkan" + key.Replace(".", "") + ".cs");
                sw.WriteLine("// Document Created with VulkanParser.");
                sw.WriteLine("//       " + DateTime.Now.ToString("HH:mm:ss dd/mm/yyyy"));
                sw.WriteLine("// by BROTHERHOOD OF THE BLACK SWORD.");
                sw.WriteLine();
                sw.WriteLine("using System;");
                sw.WriteLine("using System.Runtime.InteropServices;");
                sw.WriteLine("using " + VKxmlParser.GetNamespace() + ";");
                sw.WriteLine();
                sw.WriteLine("namespace " + VKxmlParser.GetNamespace() + ".VK" + key.Replace(".", ""));
                sw.WriteLine("{");

                foreach (string keygroups in VersionParser.d_commandsbyversion[key].grupocomandos.Keys) //por grupos
                {
                    sw.WriteLine("\t" + "public static class " + keygroups.Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace(" ", "_"));  //Agrupar metodos en clases como grupos
                    sw.WriteLine("\t" + "{");
                    List<string> listametodos = VersionParser.d_commandsbyversion[key].grupocomandos[keygroups];
                    sw.WriteLine("\t\t" + "const string VulkanLibrary = \"vulkan-1.dll\";");
                    sw.WriteLine();

                    for (int i = 0; i < listametodos.Count; i++) //Recorrer Metodos
                    {
                        Metodo mtd = CommandParser.Metodos[listametodos[i]]; //Rescatar metodo del CommandParser
                        sw.WriteLine("\t\t" + "[DllImport (VulkanLibrary, EntryPoint =\"" + mtd.Nombre + "\", CallingConvention = CallingConvention.Winapi)]");

                        bool tienearray = false;
                        foreach (string keyParams in mtd.Parametros.Keys)  //Consultar Parametros del método
                        {
                            if (mtd.Parametros[keyParams].esarray)
                            {
                                if (mtd.Parametros[keyParams].TipoValor != "char")
                                {
                                    tienearray = true; //Indica si el metodo va a tener fixeds.
                                }
                            }
                        }
                        string smetodo = "";

                        string valreturned = !mtd.ValueReturned.Contains("PFN_") ? mtd.ValueReturned : "IntPtr";
                        if (tienearray)
                        {
                            smetodo = "private static unsafe extern " + valreturned + " p_" + mtd.Nombre + "(";  //confecionar string de metodo

                        }
                        else
                        {
                            smetodo = "public static unsafe extern " + valreturned + " " + mtd.Nombre + "(";  //confecionar string de metodo
                        }

                        foreach (string keyParams in mtd.Parametros.Keys)  //Añadir Parametros del método
                        {
                            Parametro param = mtd.Parametros[keyParams];
                            string stipovalor = param.TipoValor;
                            if (stipovalor == "VkAllocationCallbacks")
                            {
                                smetodo += "VkAllocationCallbacks* " + param.Nombre + ", ";
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
                                            smetodo += "ref ";
                                            if (svalorfinparam.Contains("*"))
                                            {
                                                svalorfinparam = svalorfinparam.Replace("*", "");
                                            }
                                        }
                                    }
                                }
                                smetodo += svalorfinparam + " " + param.Nombre + ", ";
                            }
                        }
                        smetodo = smetodo.Remove(smetodo.Length - 2, 2) + ");"; //quitar coma y espacio tras último parametro y cerrar parentesis.
                        sw.WriteLine("\t" + "\t" + smetodo); //escribir enunciado de metodo.
                        sw.WriteLine();

                        if (tienearray)
                        {
                            CreatePublic(sw, mtd);
                            sw.WriteLine();
                        }
                    }
                    sw.WriteLine("\t" + "}");  //CERRAR CLASE
                    sw.WriteLine();
                }
                sw.WriteLine("}"); //CERRAR NAMESPACE
                sw.WriteLine();
                sw.Close();
            }
        }

        private static void CreatePublic(StreamWriter sw, Metodo mtd)
        {
            string smetodo = "public static unsafe " + mtd.ValueReturned + " " + mtd.Nombre + "(";  //confecionar string de metodo
            int paramcont = 0;
            foreach (string keyparam in mtd.Parametros.Keys) //recorrer parametros para enunciado.
            {
                Parametro param = mtd.Parametros[keyparam];
                if (param.Nombre == "event")
                {
                    param.Nombre = "@event";
                }
                paramcont++;
                if (param.TipoValor == "VkAllocationCallbacks")
                {
                    smetodo += "VkAllocationCallbacks*";
                }
                else
                {
                    if (param.puntero)
                    {
                        smetodo += "ref ";
                        if (param.TipoValor == "void")
                        {
                            smetodo += "IntPtr";
                        }
                        else if (param.TipoValor == "char")
                        {
                            smetodo += "string"; // + "*");
                        }
                        else
                        {
                            smetodo += Tools.VariableType(param.TipoValor); // + "*");
                        }
                    }
                    else
                    {
                        smetodo += Tools.VariableType(param.TipoValor);
                    }
                    if ((param.esarray) && (param.TipoValor != "char"))
                    {
                        smetodo += "[]";
                    }
                }
                smetodo += " " + param.Nombre + ", ";
            }

            if (smetodo.Substring(smetodo.Length - 2) == ", ") //Retirar la ultima coma y espacio para cerrar el metodo.
            {
                smetodo = smetodo.Remove(smetodo.Length - 2);
            }
            smetodo += ")"; //Cerrar enunciado de metodo.
            sw.WriteLine("\t" + "\t" + smetodo); //escribir enunciado de metodo.
            sw.WriteLine("\t" + "\t" + "{"); //Abrir Metodo.
            List<string> l_params = new List<string>(); //Lista de metodos a incluir en llamada a extern
            int ntabs = 0;
            foreach (string keyparam in mtd.Parametros.Keys) //recorrer parametros.
            {
                Parametro param = mtd.Parametros[keyparam];
                if (param.Nombre == "event")
                {
                    param.Nombre = "@event";
                }

                if (param.puntero)
                {
                    if (StructParser2.estructuras.ContainsKey(param.TipoValor))
                    {
                        if (param.TipoValor == "VkAllocationCallbacks")
                        {
                            l_params.Add(param.Nombre);
                        }
                        else
                        {
                            if (!param.esarray)
                            {
                                l_params.Add("ref " + param.Nombre);
                            }
                            else
                            {
                                string fixedtabs = "";
                                for (int t = 0; t < ntabs; t++)
                                {
                                    fixedtabs += "\t";
                                }
                                string tempvalor = "";
                                if (param.TipoValor == "void")
                                {
                                    tempvalor = "IntPtr";
                                }
                                else
                                {
                                    tempvalor = Tools.VariableType(param.TipoValor);
                                }
                                sw.WriteLine("\t" + "\t" + "\t" + fixedtabs + "fixed(" + /*param.TipoValor*/tempvalor + "* p_" + param.Nombre + " = &" + param.Nombre + "[0])");
                                sw.WriteLine("\t" + "\t" + "\t" + fixedtabs + "{"); //abrir fixed;
                                l_params.Add("p_" + param.Nombre);
                                ntabs++; //añadir numero de tabulaciones
                            }
                        }
                    }
                    else
                    {
                        if (param.TipoValor == "char")
                        {
                           /* string fixedtabs = "";
                            for (int t = 0; t < ntabs; t++)
                            {
                                fixedtabs += "\t";
                            }
                            if (param.esarray)
                            {
                                sw.WriteLine("\t" + "\t" + "\t" + fixedtabs + "fixed(" + "char* p_" + param.Nombre + " = &" + param.Nombre + ".ToCharArray()[0])");
                            }
                            else
                            {
                                sw.WriteLine("\t" + "\t" + "\t" + fixedtabs + "fixed(" + "char* p_" + param.Nombre + " = &" + param.Nombre + ")");
                            }
                            sw.WriteLine("\t" + "\t" + "\t" + fixedtabs + "{"); //abrir fixed;*/
                            l_params.Add(/*"p_" + */param.Nombre);
                            //ntabs++; //añadir numero de tabulaciones
                        }
                        else
                        {
                            string fixedtabs = "";
                            for (int t = 0; t < ntabs; t++)
                            {
                                fixedtabs += "\t";
                            }
                            string tempvalor = "";
                            if (param.TipoValor == "void")
                            {
                                tempvalor = "IntPtr";
                            }
                            else
                            {
                                tempvalor = Tools.VariableType(param.TipoValor);
                            }
                            if (param.esarray)
                            {
                                sw.WriteLine("\t" + "\t" + "\t" + fixedtabs + "fixed(" + /*param.TipoValor*/tempvalor + "* p_" + param.Nombre + " = &" + param.Nombre + "[0])");
                            }
                            else
                            {
                                sw.WriteLine("\t" + "\t" + "\t" + fixedtabs + "fixed(" + /*param.TipoValor*/tempvalor + "* p_" + param.Nombre + " = &" + param.Nombre + ")");
                            }
                            sw.WriteLine("\t" + "\t" + "\t" + fixedtabs + "{"); //abrir fixed;
                            l_params.Add("p_" + param.Nombre);
                            ntabs++; //añadir numero de tabulaciones
                        }
                    }
                }
                else
                {
                    l_params.Add(param.Nombre);
                }
            }

            #region Escribir llamada a metodo extern

            string sMethodCall = "";
            for (int i = 0; i < ntabs; i++) //Añadir Tabulaciones
            {
                sMethodCall += "\t";
            }
            if (mtd.ValueReturned != "void")
            {
                sMethodCall += "return ";
            }
            sMethodCall += "p_" + mtd.Nombre + "("; //Añadir nombre de metodo llamado
            for (int m = 0; m < l_params.Count; m++)
            {
                sMethodCall += l_params[m];
                if (m < l_params.Count - 1)
                {
                    sMethodCall += ", "; //añadir coma y espacio si no es el ultimo parametro
                }
            }
            sMethodCall += ");";
            sw.WriteLine("\t" + "\t" + "\t" + sMethodCall);

            #endregion

            #region Cerrar fixeds

            for (int i = 0; i < ntabs; i++) //Añadir Tabulaciones
            {
                string tabscierres = "";
                for (int e = 0; e < ntabs - i; e++)
                {
                    tabscierres += "\t";
                }
                sw.WriteLine("\t" + "\t" + tabscierres + "}");
            }

            #endregion

            sw.WriteLine("\t" + "\t" + "}"); //Cerrar Metodo.
        }
    }
}