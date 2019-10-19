using System;
using System.Xml;

namespace VulkanParser
{
	/// <summary>
	/// Description of ExtensionsParser.
	/// </summary>
	public static class ExtensionsParser
	{
		//private static float f_Work;
		public static void ParseExtendedEnumValues(XmlDocument xdoc)
		{
			//f_Work = 0f;
			XmlNodeList xml_extens = xdoc.SelectNodes("/registry/extensions/extension"); //[@supported='vulkan']"); //Solo Vulkan
			int n_extensions = xml_extens.Count;
			for (int i=0;i<n_extensions;i++) //Pasear por las extensiones.
			{
				//XmlNodeList 
				XmlNode nodextension = xml_extens[i];
				XmlNodeList node_require = xml_extens[i].SelectNodes("require");
				if (node_require.Count > 0) // != null)
				{
					for (int j=0;j<node_require.Count;j++)
					{
						XmlNodeList listenumextends = node_require[j].SelectNodes("enum[@extends]"); //selecciona todos los nodos enum que tengan atributo "extends"
						if (listenumextends.Count>0)
						{
							for (int en=0;en<listenumextends.Count;en++) //Pasear por los valores de enumeradores.
							{
								string value_name = listenumextends[en].Attributes["name"].Value;
								string enumExtended = listenumextends[en].Attributes["extends"].Value;
								int valueoffset = 0;
								long value = 1000000000;
								string s_value ="";
								if (listenumextends[en].Attributes["offset"] != null)
								{
								   	valueoffset = int.Parse(listenumextends[en].Attributes["offset"].Value);
									int extension_number = int.Parse(xml_extens[i].Attributes["number"].Value);
									value = value + ((extension_number -1) * 1000) + valueoffset;
									s_value = value.ToString();
								}
								else if (listenumextends[en].Attributes["alias"] != null)
								{
									string s_alias = listenumextends[en].Attributes["alias"].Value;
									if (EnumParser.d_TotalEnumValues.ContainsKey(s_alias))
									{
										string s_valalias = EnumParser.d_TotalEnumValues[s_alias];
										s_value = s_valalias;
										/*try
										{
											value = int.Parse(s_valalias);
										}
										catch (Exception e)
										{
											throw new Exception("\""+s_alias+"\" | "+"\""+s_valalias+"\" "+e.Message);
										}*/
									}
								}
								else if (listenumextends[en].Attributes["value"] != null)
								{
									value = long.Parse(listenumextends[en].Attributes["value"].Value);
									s_value = value.ToString();
								}
								else if (listenumextends[en].Attributes["bitpos"] != null)
								{
									int valuetemp = int.Parse(listenumextends[en].Attributes["bitpos"].Value);
									if (valuetemp == 0)
									{
										value = 1;
									}
									else
									{
										value = (long)System.Math.Pow(2, valuetemp);
									}
									s_value = value.ToString();
								}
								else if (listenumextends[en].Attributes["extnumber"] != null)
								{
									valueoffset = int.Parse(listenumextends[en].Attributes["offset"].Value);
									int extension_number = int.Parse(listenumextends[en].Attributes["extnumber"].Value);
									value = value + ((extension_number -1) * 1000) + valueoffset;
									s_value = value.ToString();
								}
								else
								{
									throw new Exception("NO SE PUEDE LOCALIZAR EL VALOR PARA "+value_name);
								}
								
								
								if (listenumextends[en].Attributes["dir"] != null) // El atributo "dir" define el signo del valor
								{
									if (listenumextends[en].Attributes["dir"].Value == "-")
									{
										value = value * (-1);
										s_value = value.ToString();
									}
								}
								if (EnumParser.d_Enums.ContainsKey(enumExtended))
								{
									if (!EnumParser.d_Enums[enumExtended].ContainsKey(value_name))
									{
										EnumParser.d_Enums[enumExtended].Add(value_name, s_value);
									}	
									if (!EnumParser.d_TotalEnumValues.ContainsKey(value_name))
									{
										EnumParser.d_TotalEnumValues.Add(value_name, s_value);
									}									
								}
							}
						}
					}
				}
			}
		}
	}
	/*
	Implicit is the registered number of an extension, which is used to create a range of unused values 
	offset against a global extension base value. Individual enumerant values are calculated as offsets 
	in that range. Values are calculated as follows:

		base_value = 1000000000
		range_size = 1000
		enum_offset(extension_number,offset) = base_value (extension_number - 1) × range_size + offset
		Positive values: enum_offset(extension_number,offset)
		Negative values: -enum_offset(extension_number,offset)
	*/
}
