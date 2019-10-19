using System;
using System.IO;

namespace VulkanParser
{
	/// <summary>
	/// Description of CreateConstCS.
	/// </summary>
	public static class CreaterConstCS
	{
		public static void CreateConstCS()
		{
			StreamWriter sw = File.CreateText(VKxmlParser.GetDestination()+"VKConst.cs");
			sw.WriteLine("// Document Created with VulkanParser.");
			sw.WriteLine("//       "+DateTime.Now.ToString("HH:mm:ss dd/mm/yyyy"));
			sw.WriteLine("// by BROTHERHOOD OF THE BLACK SWORD.");
			sw.WriteLine();
			sw.WriteLine("using System;");
			sw.WriteLine();
			sw.WriteLine("namespace "+VKxmlParser.GetNamespace());
			sw.WriteLine("{");
			
			
			sw.WriteLine("\t"+"public static class VKConst");
			sw.WriteLine("\t"+"{");
			//for (int i=0;APIConstantsParser.ConstValues.Count;i++)
			foreach(string key in APIConstantsParser.ConstValues.Keys)
			{
				string dv = APIConstantsParser.ConstValues[key];
				string tpv = TypeValor(dv);
				string valor = TransValor(dv);
				
				sw.WriteLine("\t"+"\t"+"public const "+tpv+" "+key+" = "+valor+";");//tab
			}
			
			sw.WriteLine("\t}");
			
			sw.WriteLine("}");
			sw.WriteLine();
			sw.Close();
		}
		
		private static string TransValor(string val)
		{
			switch (val)
			{
				case "(~0ULL)":
					return "ulong.MaxValue"; //.ToString();
				case "(~0U)":
					return "ushort.MaxValue"; //.ToString();
				default:
					if (val.Contains("(~0U-"))
					{
						string minival = val.Substring(5,1);
						return (ushort.MaxValue - ushort.Parse(minival)).ToString();
					}
					else
					{
						return val;
					}
					//return "";
			}
		}
		private static string TypeValor(string val)
		{
			switch (val)
			{
				case "(~0ULL)":
					return "ulong";
				case "(~0U)":
					return "ushort";
				default:
					if (val.Contains("(~0U-"))
					{
						return "ushort";
					}
					else if (val.Contains("f"))
					{
						return "float";
					}
					else
					{
						return "int";
					}
					//return "";
			}
		}
	}
}
