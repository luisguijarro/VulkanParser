using System;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace VulkanParser
{
	/// <summary>
	/// Description of EnumParser.
	/// </summary>
	public static class EnumParser
	{
		public static Dictionary<string,Dictionary<string, string>> d_Enums; //Enumerator, (valueName, Value)
		public static Dictionary<string, string> d_EnumComments;  //valueName, Comment
		public static Dictionary<string, string> d_TotalEnumValues;  //valueName, Comment
		//public static event EventHandler<WorkingEventArgs> working;
		private static float f_Work;
		
		public static void Parse(XmlDocument xdoc)
		{
			f_Work = 0f;
			XmlNodeList xnl_enums = xdoc.SelectNodes("/registry/enums[@type='enum']|/registry/enums[@type='bitmask']");
			if (xnl_enums.Count > 0)
			{
				float workincrement = 100f/(float)xnl_enums.Count;
				d_Enums = new Dictionary<string, Dictionary<string, string>>();
				d_EnumComments = new Dictionary<string, string>();
				d_TotalEnumValues = new Dictionary<string, string>();
				
				for (int i=0;i<xnl_enums.Count;i++)
				{ //<enums>
					string s_name = xnl_enums[i].Attributes["name"].Value;
					XmlNodeList xnl_values = xnl_enums[i].SelectNodes("enum");
					if (xnl_values.Count >0)
					{ 
						Dictionary<string,string> D_valuesEnums = new Dictionary<string, string>();
						for (int s=0;s<xnl_values.Count;s++)
						{ //<enums><enum>...
							string s_ValueName = xnl_values[s].Attributes["name"].Value;
							string s_ValueValue= "";
							if (xnl_values[s].Attributes["alias"] !=null)
							{
								s_ValueValue = d_TotalEnumValues[xnl_values[s].Attributes["alias"].Value];
							}
							else
							{
								if (xnl_values[s].Attributes["bitpos"] !=null)
								{
									int valuetemp = int.Parse(xnl_values[s].Attributes["bitpos"].Value);
									if (valuetemp == 0)
									{
										s_ValueValue = "1";
									}
									else
									{
										s_ValueValue = System.Math.Pow(2, valuetemp).ToString();
									}
								}
								else
								{
									s_ValueValue = xnl_values[s].Attributes["value"].Value;
								}
							}
							
							D_valuesEnums.Add(s_ValueName, s_ValueValue);
							d_TotalEnumValues.Add(s_ValueName, s_ValueValue);
							
							if (xnl_values[s].Attributes["comment"] != null)
							{
								d_EnumComments.Add(s_ValueName, xnl_values[s].Attributes["comment"].Value); // ADD VALUE COMMENT
							}
						}
						d_Enums.Add(s_name, D_valuesEnums); //ADD ENUM WITH VALUES
					}
					IncrementWork(workincrement, "Leido Enumerador "+s_name);
				}
			}
			else
			{
				throw new Exception("No se han encontrado enumeradores");
			}
			IncrementWork(0, "\r\n ---------- Leidos "+xnl_enums.Count+" Enumeradores. --------- \r\n");
		}
		
		public static void ExtParse(XmlDocument xdoc)
		{
			XmlNodeList xnl_enums = xdoc.SelectNodes("/registry/feature[@api='vulkan']/require/enum[@extends]");
			if (xnl_enums.Count > 0)
			{
				for (int i=0;i<xnl_enums.Count;i++)
				{
					string s_extends = xnl_enums[i].Attributes["extends"].Value;
					if (EnumParser.d_Enums.ContainsKey(s_extends)) //Si existe el enumerador que extiende...
					{
						string s_enumvaluename =  xnl_enums[i].Attributes["name"].Value;
						string s_valor = "";
						if (!EnumParser.d_Enums[s_extends].ContainsKey(s_enumvaluename)) //Si el enumerador no tiene el valor... se le añade.
						{
							if (xnl_enums[i].Attributes["bitpos"] != null)
							{
								int i_val = int.Parse(xnl_enums[i].Attributes["bitpos"].Value);
								if (i_val > 0)
								{
									s_valor = System.Math.Pow(2, i_val).ToString();
								}
								else
								{
									s_valor = "1";
								}
							}
							else if (xnl_enums[i].Attributes["extnumber"] != null)
							{
								int value = 1000000000;
								int valueoffset = int.Parse(xnl_enums[i].Attributes["offset"].Value);
								int extension_number = int.Parse(xnl_enums[i].Attributes["extnumber"].Value);
								value = value + ((extension_number -1) * 1000) + valueoffset;
								s_valor = value.ToString();
							}
							else if (xnl_enums[i].Attributes["alias"] != null)
							{
								s_valor = xnl_enums[i].Attributes["alias"].Value;
								
							}
							d_Enums[s_extends].Add(s_enumvaluename, s_valor);
							if (!d_TotalEnumValues.ContainsKey(s_enumvaluename))
							{
								d_TotalEnumValues.Add(s_enumvaluename, s_valor);
							}
						}
					}
				}
			}
		}
		
		private static void IncrementWork(float cant, string accion)
		{
			f_Work += cant;
			if (f_Work > 100f) { f_Work = 100f;}
			
			FormUpdater.UpdateText(accion);
			FormUpdater.UpdateProgress((int)f_Work);
		}
		
	}
}
