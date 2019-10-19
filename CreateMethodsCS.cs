using System;
using System.IO;
using System.Collections.Generic;

using VulkanParser.objetos;

namespace VulkanParser
{
	/// <summary>
	/// Description of CreateMethodsCS.
	/// </summary>
	public static class CreaterMethodsCS
	{
		public static void CreateMethodsCS()
		{
			foreach (string key in VersionParser.d_commandsbyversion.Keys) //por versiones
			{
				StreamWriter sw = File.CreateText(VKxmlParser.GetDestination()+"Vulkan"+ key.Replace(".","")+".cs");
				sw.WriteLine("// Document Created with VulkanParser.");
				sw.WriteLine("//       "+DateTime.Now.ToString("HH:mm:ss dd/mm/yyyy"));
				sw.WriteLine("// by BROTHERHOOD OF THE BLACK SWORD.");
				sw.WriteLine();
				sw.WriteLine("using System;");
				sw.WriteLine("using System.Runtime.InteropServices;");
				sw.WriteLine("using "+VKxmlParser.GetNamespace()+";");
				sw.WriteLine();
				sw.WriteLine("namespace "+VKxmlParser.GetNamespace()+".VK"+key.Replace(".",""));
				sw.WriteLine("{");
				
				foreach(string keygroups in VersionParser.d_commandsbyversion[key].grupocomandos.Keys) //por grupos
				{
					sw.WriteLine("\t"+"public static class "+keygroups.Split(new string[]{" ("}, StringSplitOptions.RemoveEmptyEntries)[0].Replace(" ", "_"));  //Agrupar metodos en clases como grupos
					sw.WriteLine("\t"+"{");
					List<string> listametodos = VersionParser.d_commandsbyversion[key].grupocomandos[keygroups];
					sw.WriteLine("\t\t"+"const string VulkanLibrary = \"vulkan-1\";");
					sw.WriteLine();
					
					for (int i=0;i<listametodos.Count;i++) //Recorrer Metodos
					{
						Metodo mtd = CommandParser.Metodos[listametodos[i]]; //Rescatar metodo del CommandParser
						sw.WriteLine("\t\t"+"[DllImport (VulkanLibrary, CallingConvention = CallingConvention.Winapi)]");
						string smetodo = "public static unsafe extern "+mtd.ValueReturned+" "+mtd.Nombre+"(";  //confecionar string de metodo
						foreach (string keyParams in mtd.Parametros.Keys)  //Añadir Parametros del método
						{
							Parametro param = mtd.Parametros[keyParams];
							/*if (param.constante)
							{
								smetodo += "const ";
							}*/
							string stipovalor = param.TipoValor;
                            if (HandleParser.handleTypes.Contains(stipovalor))
                            {
                                stipovalor = "IntPtr";
                            }
                            if (param.puntero)
							{
								stipovalor += "*";
							}
                            string svalorfinparam = Tools.VariableType(stipovalor);
							if (param.Nombre == "event")
							{
								param.Nombre = "@event";
							}
							smetodo += svalorfinparam + " " + param.Nombre + ", ";
						}
						smetodo = smetodo.Remove(smetodo.Length-2, 2) + ");"; //quitar comay espacio tras último parametro y cerrar parentesis.
						sw.WriteLine("\t"+"\t"+smetodo); //escribir enunciado de metodo.
						//sw.WriteLine("\t"+"\t"+"{");
						//sw.WriteLine("\t"+"\t"+"\t");
						//sw.WriteLine("\t"+"\t"+"}");
						sw.WriteLine();
					}
					sw.WriteLine("\t"+"}");
					sw.WriteLine();
				}
			
				sw.WriteLine("}");
				sw.WriteLine();
				sw.Close();
			}
		}
	}
}
