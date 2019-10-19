using System;
using System.IO;
using System.Collections.Generic;

using VulkanParser.objetos;

namespace VulkanParser
{
	/// <summary>
	/// Description of CreateMethodsCS.
	/// </summary>
	public static class CreaterMethodsICS
	{
		public static void CreateMethodsICS()
		{
			foreach (string key in VersionParser.d_commandsbyversion.Keys) //por versiones
			{
				if (!Directory.Exists(VKxmlParser.GetDestination()+"/Internals"))
				{
					Directory.CreateDirectory(VKxmlParser.GetDestination()+"/Internals");
				}
				StreamWriter sw = File.CreateText(VKxmlParser.GetDestination()+"/Internals/"+"Vulkan"+ key.Replace(".","")+"i.cs");
				sw.WriteLine("// Document Created with VulkanParser.");
				sw.WriteLine("//       "+DateTime.Now.ToString("HH:mm:ss dd/mm/yyyy"));
				sw.WriteLine("// by BROTHERHOOD OF THE BLACK SWORD.");
				sw.WriteLine();
				sw.WriteLine("using System;");
				sw.WriteLine();
				sw.WriteLine("namespace "+VKxmlParser.GetNamespace());
				sw.WriteLine("{");
				
				sw.WriteLine("\t"+"internal static class VK"+key.Replace(".","")+"Internals");  //Agrupar metodos en clase internal dentro de namespace de version
				sw.WriteLine("\t"+"{");
					
				foreach(string keygroups in VersionParser.d_commandsbyversion[key].grupocomandos.Keys) //por grupos
				{
					
					List<string> listametodos = VersionParser.d_commandsbyversion[key].grupocomandos[keygroups];
					
					for (int i=0;i<listametodos.Count;i++)
					{
						Metodo mtd = CommandParser.Metodos[listametodos[i]]; //Rescatar metodo del CommandParser
						string smetodo = "internal static VK"+key.Replace(".","")+"Delegates."+mtd.Nombre+" "+mtd.Nombre+";";  //confecionar string de metodo						
						sw.WriteLine("\t"+"\t"+smetodo); //escribir enunciado de metodo.
						sw.WriteLine();
					}
					
					//sw.WriteLine();
				}
			
				sw.WriteLine("\t"+"}");
				sw.WriteLine("}");
				sw.WriteLine();
				sw.Close();
			}
		}
	}
}
