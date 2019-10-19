using System;
using System.IO;

namespace VulkanParser
{
	/// <summary>
	/// Description of CreateEnumCS.
	/// </summary>
	public static class CreaterEnumCS
	{
		public static void CreateEnumCS()
		{
			StreamWriter sw = File.CreateText(VKxmlParser.GetDestination()+"VKEnums.cs");
			sw.WriteLine("// Document Created with VulkanParser.");
			sw.WriteLine("//       "+DateTime.Now.ToString("HH:mm:ss dd/mm/yyyy"));
			sw.WriteLine("// by BROTHERHOOD OF THE BLACK SWORD.");
			sw.WriteLine();
			sw.WriteLine("using System;");
			sw.WriteLine();
			sw.WriteLine("namespace "+VKxmlParser.GetNamespace());
			sw.WriteLine("{");
			
			foreach (string key in EnumParser.d_Enums.Keys)
			{
                bool tvneg = false;
                bool tvmasint = false;
                //bool tvmaslong = false;
                foreach (string key2 in EnumParser.d_Enums[key].Keys)
                {
                    long tempval;
                    if (long.TryParse(EnumParser.d_Enums[key][key2], out tempval))
                    {
                        if (tempval < 0)
                        {
                            tvneg = true;
                            if ((tempval * -1) > int.MaxValue)
                            {
                                tvmasint = true;
                            }
                        }
                        else
                        {
                            if (tempval > int.MaxValue)
                            {
                                tvmasint = true;
                                /*if (tempval > int.MaxValue)
                                {

                                }*/
                            }
                        }
                    }
                }

                string tvalor = "";

                if (tvneg)
                {
                    if (tvmasint)
                    {
                        tvalor = "long";
                    }
                    else
                    {
                        tvalor = "int";
                    }
                }
                else
                {
                    if (tvmasint)
                    {
                        tvalor = "ulong";
                    }
                    else
                    {
                        tvalor = "uint";
                    }
                }

                sw.WriteLine("\t"+"public enum "+key+" : "+tvalor);
				sw.WriteLine("\t"+"{");
				
				int maxkeys = EnumParser.d_Enums[key].Keys.Count;
				int contador = 0;
				foreach(string key2 in EnumParser.d_Enums[key].Keys)
				{
					if (EnumParser.d_EnumComments.ContainsKey(key2))
					{
						sw.WriteLine("\t"+"\t"+"/// <summary>"+EnumParser.d_EnumComments[key2]+"</summary>");
					}
					if (contador < maxkeys-1)
					{
						sw.WriteLine("\t"+"\t"+key2+" = "+EnumParser.d_Enums[key][key2]+",");
					}
					else
					{
						sw.WriteLine("\t"+"\t"+key2+" = "+EnumParser.d_Enums[key][key2]);
					}
					contador++;
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
