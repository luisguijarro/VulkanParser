using System;
using System.IO;

namespace VulkanParser
{
	/// <summary>
	/// Description of CreateIntPtrs.
	/// </summary>
	public static class CreaterIntPtrsCS
	{
		public static void CreateIntPtrsCS()
		{
			StreamWriter sw = File.CreateText(VKxmlParser.GetDestination()+"VKIntPtrs.cs");
			sw.WriteLine("// Document Created with VulkanParser.");
			sw.WriteLine("//       "+DateTime.Now.ToString("HH:mm:ss dd/mm/yyyy"));
			sw.WriteLine("// by BROTHERHOOD OF THE BLACK SWORD.");
			sw.WriteLine();
			sw.WriteLine("using System;");
			sw.WriteLine();
			sw.WriteLine("namespace "+VKxmlParser.GetNamespace());
			sw.WriteLine("{");
			
			//MEOLLO
			for (int i=0;i<IntPtrsParser.IntPtrs.Count;i++) //IntPtrs
			{
				sw.WriteLine("\t"+"using "+IntPtrsParser.IntPtrs[i]+" = IntPtr;");
			}
			
			sw.WriteLine();
			for (int i=0;i<HandleParser.handleTypes.Count;i++) //handles
			{
				sw.WriteLine("\t"+"using "+HandleParser.handleTypes[i]+" = IntPtr;");
			}
			
			sw.WriteLine("}");
			sw.WriteLine();
			sw.Close();
		}
	}
}
