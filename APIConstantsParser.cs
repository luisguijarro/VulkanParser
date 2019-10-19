using System;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VulkanParser
{
	/// <summary>
	/// Description of APIConstantsParser.
	/// </summary>
	public static class APIConstantsParser
	{
		public static Dictionary<string,string> ConstValues;
		private static float f_Work;
		public static void Parse(XmlDocument xdoc)
		{
			ConstValues = new Dictionary<string, string>();
			f_Work = 0f;
			XmlNodeList xml_enums = xdoc.SelectNodes("/registry/enums[@name='API Constants']/enum");
			float workincrement = 100f/(float)xml_enums.Count;
			for (int i=0;i<xml_enums.Count;i++)
			{
				string s_name = xml_enums[i].Attributes["name"].Value;
				string s_value = "";
				if (xml_enums[i].Attributes["alias"] != null)
				{
					s_value = ConstValues[xml_enums[i].Attributes["alias"].Value];
				}
				else
				{
					s_value = xml_enums[i].Attributes["value"].Value;
				}
				if (s_value.Length>0)
				{
					ConstValues.Add(s_name, s_value);
					IncrementWork(workincrement, "Leida Constante "+s_name);
				}
				else
				{
					throw new Exception("La Constante "+s_name+" no tiene valor");
				}
			}
			IncrementWork(0, "\r\n ---------- Leidas "+xml_enums.Count+" Constantes. --------- \r\n");
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
