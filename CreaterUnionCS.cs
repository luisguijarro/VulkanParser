using System;
using System.IO;

using VulkanParser.objetos;

namespace VulkanParser
{
	/// <summary>
	/// Description of CreaterUnionCS.
	/// </summary>
	public static class CreaterUnionCS
	{
		public static void CreateUnionCS()
		{
			StreamWriter sw = File.CreateText(VKxmlParser.GetDestination()+"VKUnions.cs");
			sw.WriteLine("// Document Created with VulkanParser.");
			sw.WriteLine("//       "+DateTime.Now.ToString("HH:mm:ss dd/mm/yyyy"));
			sw.WriteLine("// by BROTHERHOOD OF THE BLACK SWORD.");
			sw.WriteLine();
			sw.WriteLine("using System;");
			sw.WriteLine("using System.Runtime.InteropServices;");
			sw.WriteLine();
			sw.WriteLine("namespace "+VKxmlParser.GetNamespace());
			sw.WriteLine("{");
			
			foreach (string key in UnionParser.unions.Keys) //ESTRUCTURAS
			{
				sw.WriteLine("\t"+"[StructLayout(LayoutKind.Explicit)]");
				sw.WriteLine("\t"+"public unsafe struct "+key);
				sw.WriteLine("\t"+"{");
				
				//bool first = true;
				//int cant = 0;
				foreach (string key2 in UnionParser.unions[key].members.Keys) //VALORES DE LA ESTRUCTURA
				{
					sw.WriteLine("\t\t"+"[FieldOffset(0)]");
					unionmembers unm0 = UnionParser.unions[key].members[key2];
					if (unm0.cant.Length > 0)
					{
						sw.WriteLine("\t\t"+"public fixed "+Tools.VariableType(unm0.Type)+"\t"+unm0.name+"["+unm0.cant+"];");
					}
					else
					{
						sw.WriteLine("\t\t"+"public "+Tools.VariableType(unm0.Type)+"\t"+unm0.name+";");
					}
				}
				
				sw.WriteLine("\t"+"}");
			}
			
			sw.WriteLine("}");
			sw.WriteLine();
			sw.Close();
		}
	}
}
