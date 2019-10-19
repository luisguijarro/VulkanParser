using System;
using System.IO;
using System.Collections.Generic;

using VulkanParser.objetos;

namespace VulkanParser
{
    public static class VKDeviceMaker
    {
        public static void VKDeviceMake()
        {
            StreamWriter sw = File.CreateText(VKxmlParser.GetDestination() + "VKDevice.cs"); // + key.Replace(".", "") + ".cs");
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
            sw.WriteLine("\t"+"public class VKDevice");
            sw.WriteLine("\t" + "{");
            sw.WriteLine("\t" + "\t" + "private IntPtr ip_handle;");
            sw.WriteLine("\t" + "\t" + "public VKDevice(IntPtr handle)");
            sw.WriteLine("\t" + "\t" + "{");
            sw.WriteLine("\t" + "\t" + "\t" + "this.ip_handle = handle;");			
			foreach (string key in VersionParser.d_commandsbyversion.Keys) //por versiones
            {
				sw.WriteLine("\t" + "\t" + "\t" + "this.VK" + key.Replace(".", "") + " = new vk" + key.Replace(".", "") + "();");
			}
            sw.WriteLine("\t" + "\t" + "\t" + VKxmlParser.GetNamespace() + ".VKDelegator.InitDelegatesVK(this);");
            sw.WriteLine("\t" + "\t" + "}");
            sw.WriteLine();

            #region handle:
            sw.WriteLine("\t" + "\t" + "public IntPtr Handle");
            sw.WriteLine("\t" + "\t" + "{");
            sw.WriteLine("\t" + "\t" + "\t" + "get { return this.ip_handle; }");
            sw.WriteLine("\t" + "\t" + "}");
            sw.WriteLine();
            #endregion

            CreateBaseMethods(sw);

            sw.WriteLine("\t" + "}");
            sw.WriteLine("}");
            sw.Close();
        }

        private static void CreateBaseMethods(StreamWriter sw)
        {

            #region VK METHODS

            foreach (string key in VersionParser.d_commandsbyversion.Keys) //por versiones
            {
                sw.WriteLine("\t" + "\t" + "public vk" + key.Replace(".", "") + " VK" + key.Replace(".", "") + ";");
                sw.WriteLine();
                sw.WriteLine("\t" + "\t" + "public class vk" + key.Replace(".", ""));
                sw.WriteLine("\t" + "\t" + "{");
                sw.WriteLine("\t" + "\t" + "\t" + "public vk" + key.Replace(".", "")+"()"); //Constructor
                sw.WriteLine("\t" + "\t" + "\t" + "{ ");
                foreach (string keygroups in VersionParser.d_commandsbyversion[key].grupocomandos.Keys) //por grupos
                {
                    string s_grupo = keygroups.Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace(" ", "_");
                    string sp_grupo = char.ToUpper(s_grupo[0]) + s_grupo.Substring(1);
                    sw.WriteLine("\t" + "\t" + "\t" + "\t" + sp_grupo + " = new " + s_grupo.ToLower() + "();");
                }
                sw.WriteLine("\t" + "\t" + "\t" + "}");
                sw.WriteLine();
                sw.WriteLine("\t" + "\t" + "\t" + "#region Gruposaccesibles");
                foreach (string keygroups in VersionParser.d_commandsbyversion[key].grupocomandos.Keys) //Declaración de las clases de los grupos de métodos.
                {
                    string s_grupo = keygroups.Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace(" ", "_");
                    string sp_grupo = char.ToUpper(s_grupo[0]) + s_grupo.Substring(1);
                    sw.WriteLine("\t" + "\t" + "\t" + "public " + s_grupo.ToLower() + " " + sp_grupo + ";");
                }
                sw.WriteLine("\t" + "\t" + "\t" + "#endregion");
                sw.WriteLine();
                foreach (string keygroups in VersionParser.d_commandsbyversion[key].grupocomandos.Keys) //Definición de las clases de los grupos de metodos.
                {
                    List<string> listametodos = VersionParser.d_commandsbyversion[key].grupocomandos[keygroups];
                    string s_grupo = keygroups.Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace(" ", "_");
                    string sp_grupo = char.ToUpper(s_grupo[0]) + s_grupo.Substring(1);
                    sw.WriteLine("\t" + "\t" + "\t" + "public class " + s_grupo.ToLower());  //Agrupar metodos en clases como grupos
                    sw.WriteLine("\t" + "\t" + "\t" + "{");
                    for (int i = 0; i < listametodos.Count; i++) //Recorrer Metodos del grupo
                    {
                        Metodo mtd = CommandParser.Metodos[listametodos[i]]; //Rescatar metodo del CommandParser
                        string s_metodo = "";

                        #region PASO 0: tiene Array
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
                        #endregion

                        #region PASO 1: Valor Retornado y Apertura de Metodo
                        string valreturned = !mtd.ValueReturned.Contains("PFN_") ? mtd.ValueReturned : "IntPtr";

                        s_metodo = "internal unsafe " + VKxmlParser.GetNamespace() + ".VKDelegates.VK" + key.Replace(".", "") + "." + sp_grupo + "." + mtd.Nombre + " p_" + mtd.Nombre + ";";  //confecionar string de metodo

                        #endregion

                        sw.WriteLine("\t" + "\t" + "\t" + "\t" + s_metodo); //ESCRIBIR METODO

                        sw.WriteLine();

                        if (tienearray)
                        {
                            CreatePublicMethods(sw, mtd);
                            sw.WriteLine();
                        }
                        else
                        {
                            CreateNormalPublicMethods(sw, mtd);
                            sw.WriteLine();
                        }
                    }
                    sw.WriteLine("\t" + "\t" + "\t" + "}");
                    sw.WriteLine();
                }
                sw.WriteLine("\t" + "\t" + "}");
                sw.WriteLine();
            }

            #endregion

        }

        private static void CreateNormalPublicMethods(StreamWriter sw, Metodo mtd)
        {
            string s_ret = mtd.ValueReturned;
            if (s_ret.Contains("PFN"))
            {
                s_ret = "IntPtr";
            }
            string s_metodo = "public unsafe " + s_ret + " " + mtd.Nombre + "(";  //confecionar string de metodo

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
            s_metodo = s_metodo.Remove(s_metodo.Length - 2, 2) + ")"; //quitar coma y espacio tras último parametro y cerrar parentesis.
            #endregion

            sw.WriteLine("\t" + "\t" + "\t" + "\t" + s_metodo); //ESCRIBIR METODO

            #region PASO 3: Escribir contenido de metodo y llamada al metodo privado.
            sw.WriteLine("\t" + "\t" + "\t" + "\t" + "{");
            string s_internalMethod = mtd.ValueReturned != "void" ? "return " : ""; // ¿Retorna?
            s_internalMethod += "p_" + mtd.Nombre + "(";

            foreach (string kparam in mtd.Parametros.Keys)
            {
                if (referidos.Contains(kparam))
                {
                    s_internalMethod += "ref ";
                }
                s_internalMethod += mtd.Parametros[kparam].Nombre + ", ";
            }

            s_internalMethod = s_internalMethod.Remove(s_internalMethod.Length - 2, 2) + ");"; //Cerrar LLamada quitando ultimo coma y espacio.
            sw.WriteLine("\t" + "\t" + "\t" + "\t" + "\t" + s_internalMethod); // Escribir Llamada a Internal Method.

            sw.WriteLine("\t" + "\t" + "\t" + "\t" + "}");
            #endregion
        }

        private static void CreatePublicMethods(StreamWriter sw, Metodo mtd)
        {
            string smetodo = "public unsafe " + mtd.ValueReturned + " " + mtd.Nombre + "(";  //confecionar string de metodo
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
            sw.WriteLine("\t" + "\t" + "\t" + "\t" + smetodo); //escribir enunciado de metodo.
            sw.WriteLine("\t" + "\t" + "\t" + "\t" + "{"); //Abrir Metodo.
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
                                sw.WriteLine("\t" + "\t" + "\t" + "\t" + "\t" + fixedtabs + "fixed(" + /*param.TipoValor*/tempvalor + "* p_" + param.Nombre + " = &" + param.Nombre + "[0])");
                                sw.WriteLine("\t" + "\t" + "\t" + "\t" + "\t" + fixedtabs + "{"); //abrir fixed;
                                l_params.Add("p_" + param.Nombre);
                                ntabs++; //añadir numero de tabulaciones
                            }
                        }
                    }
                    else
                    {
                        if (param.TipoValor == "char")
                        {
                            /*string fixedtabs = "";
                            for (int t = 0; t < ntabs; t++)
                            {
                                fixedtabs += "\t";
                            }
                            if (param.esarray)
                            {
                                sw.WriteLine("\t" + "\t" + "\t" + "\t" + "\t" + fixedtabs + "fixed(" + "char* p_" + param.Nombre + " = &" + param.Nombre + ".ToCharArray()[0])");
                            }
                            else
                            {
                                sw.WriteLine("\t" + "\t" + "\t" + "\t" + "\t" + fixedtabs + "fixed(" + "char* p_" + param.Nombre + " = &" + param.Nombre + ")");
                            }
                            sw.WriteLine("\t" + "\t" + "\t" + "\t" + "\t" + fixedtabs + "{"); //abrir fixed;
                            */
                            l_params.Add(param.Nombre);
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
                                sw.WriteLine("\t" + "\t" + "\t" + "\t" + "\t" + fixedtabs + "fixed(" + /*param.TipoValor*/tempvalor + "* p_" + param.Nombre + " = &" + param.Nombre + "[0])");
                            }
                            else
                            {
                                sw.WriteLine("\t" + "\t" + "\t" + "\t" + "\t" + fixedtabs + "fixed(" + /*param.TipoValor*/tempvalor + "* p_" + param.Nombre + " = &" + param.Nombre + ")");
                            }
                            sw.WriteLine("\t" + "\t" + "\t" + "\t" + "\t" + fixedtabs + "{"); //abrir fixed;
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
            sw.WriteLine("\t" + "\t" + "\t" + "\t" + "\t" + sMethodCall);

            #endregion

            #region Cerrar fixeds

            for (int i = 0; i < ntabs; i++) //Añadir Tabulaciones
            {
                string tabscierres = "";
                for (int e = 0; e < ntabs - i; e++)
                {
                    tabscierres += "\t";
                }
                sw.WriteLine("\t" + "\t" + "\t" + "\t" + tabscierres + "}");
            }

            #endregion

            sw.WriteLine("\t" + "\t" + "\t" + "\t" + "}"); //Cerrar Metodo.
        }
    }
}
