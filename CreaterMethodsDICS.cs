using System;
using System.IO;
using System.Collections.Generic;

using VulkanParser.objetos;

namespace VulkanParser
{
	/// <summary>
	/// Description of CreateMethodsCS.
	/// </summary>
	public static class CreaterMethodsDICS
	{
		public static void CreateMethodsDICS()
		{
			foreach (string key in VersionParser.d_commandsbyversion.Keys) //por versiones
			{
				StreamWriter sw = File.CreateText(VKxmlParser.GetDestination()+"Vulkan"+ key.Replace(".","")+".cs");
				sw.WriteLine("// Document Created with VulkanParser.");
				sw.WriteLine("//       "+DateTime.Now.ToString("HH:mm:ss dd/mm/yyyy"));
				sw.WriteLine("// by BROTHERHOOD OF THE BLACK SWORD.");
				sw.WriteLine();
				sw.WriteLine("namespace "+VKxmlParser.GetNamespace()+".VK"+key.Replace(".",""));
				sw.WriteLine("{");
				
				sw.WriteLine("\t"+"internal static class VKDI"+key.Replace(".",""));  //Clase de inicio de delegados.
				sw.WriteLine("\t"+"{");
				sw.WriteLine("\t"+"\t"+"internal static void InitDelegates()");
				sw.WriteLine("\t"+"\t"+"{");
				foreach(string keygroups in VersionParser.d_commandsbyversion[key].grupocomandos.Keys) //por grupos
				{					
					List<string> listametodos = VersionParser.d_commandsbyversion[key].grupocomandos[keygroups];
					
					for (int i=0;i<listametodos.Count;i++)
					{
						Metodo mtd = CommandParser.Metodos[listametodos[i]]; //Rescatar metodo del CommandParser
						string smetodo = "public static "+mtd.ValueReturned+" "+mtd.Nombre+"(";  //confecionar string de metodo
						foreach (string keyParams in mtd.Parametros.Keys)  //Añadir Parametyros del método
						{
							Parametro param = mtd.Parametros[keyParams];
							if (param.constante)
							{
								smetodo += "const ";
							}
							string stipovalor = param.TipoValor;
							if (param.puntero)
							{
								stipovalor += "*";
							}
							string svalorfinparam = Tools.VariableType(stipovalor);
							smetodo += svalorfinparam + " " + param.Nombre + ", ";
						}
						smetodo = smetodo.Remove(smetodo.Length-2, 2) + ")"; //quitar comay espacio tras último parametro y cerrar parentesis.
						sw.WriteLine("\t"+"\t"+smetodo); //escribir enunciado de metodo.
						sw.WriteLine("\t"+"\t"+"{");
						sw.WriteLine("\t"+"\t"+"\t");
						sw.WriteLine("\t"+"\t"+"}");
						sw.WriteLine();
					}
					
				}
				
				sw.WriteLine("\t"+"\t"+"}");
				sw.WriteLine("}");
				sw.WriteLine();
				sw.Close();
			}
		}
	}
}
