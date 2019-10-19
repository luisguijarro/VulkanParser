using System;
using System.Xml;
using System.Collections.Generic;

using VulkanParser.objetos;

namespace VulkanParser
{
	/// <summary>
	/// Description of UnionParser.
	/// </summary>
	public static class UnionParser
	{
		public static Dictionary<string, Union> unions;
		public static void Parse(XmlDocument xdoc)
		{
			unions = new Dictionary<string, Union>();
			XmlNodeList xml_unions = xdoc.SelectNodes("/registry/types/type[@category='union']");
			if (xml_unions.Count > 0)
			{
				for (int i=0;i<xml_unions.Count;i++)
				{
					XmlNode n_union = xml_unions[i];
					string s_name = n_union.Attributes["name"].Value;
					Union un0 = new Union();
					un0.Name = s_name;
					XmlNodeList miembros = n_union.SelectNodes("member");
					if (miembros.Count > 0)
					{
						for (int m=0; m<miembros.Count;m++)
						{
							unionmembers unm0 = new unionmembers();
							string s_mname = miembros[m].SelectSingleNode("name").InnerText;
							string s_mtipo = miembros[m].SelectSingleNode("type").InnerText;
							unm0.name = s_mname;
							unm0.Type = s_mtipo;
							if (miembros[m].InnerText.Contains("["))
							{
								//es array con máximo.
								string scant = miembros[m].InnerText.Split('[')[1];
								scant = scant.Split(']')[0];
								unm0.cant = scant;
							}
							un0.members.Add(s_mname, unm0);
						}
					}
					unions.Add(s_name, un0);
				}
			}
		}
	}
}
