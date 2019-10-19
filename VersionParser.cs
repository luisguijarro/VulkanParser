using System;
using System.Xml;
using System.Collections.Generic;
using VulkanParser.objetos;

namespace VulkanParser
{
	/// <summary>
	/// Description of VersionParser.
	/// </summary>
	public static  class VersionParser
	{
		private static float f_Work;
		public static Dictionary<string,version> d_commandsbyversion;
		public static void Parse(XmlDocument xdoc)
		{
			f_Work = 0f;
			XmlNodeList xml_versions = xdoc.SelectNodes("/registry/feature[@api='vulkan']");
			if (xml_versions.Count>0)
			{
				d_commandsbyversion = new Dictionary<string,version>();
				for (int i=0;i<xml_versions.Count;i++)
				{
					string n_version = xml_versions[i].Attributes["number"].Value;
					XmlNodeList xml_g_comandos = xml_versions[i].SelectNodes("require");
					if (xml_g_comandos.Count > 0) //GRUPOS DE COMMANDOS
					{
						Dictionary<string,List<string>> grupocomandos = new Dictionary<string, List<string>>();
						for (int c=0;c<xml_g_comandos.Count;c++)
						{
							if (xml_g_comandos[c].Attributes["comment"] != null)
							{
								string nombregrupo = xml_g_comandos[c].Attributes["comment"].Value; //<feactures><require comment="
								XmlNodeList xml_commandos = xml_g_comandos[c].SelectNodes("command");
								if (xml_commandos.Count>0)
								{
									//LEER COMANDOS DENTRO DEL GRUPO
									List<string> L_comandos = new List<string>();
									for (int cc=0;cc<xml_commandos.Count;cc++)
									{
										L_comandos.Add(xml_commandos[cc].Attributes["name"].Value);
									}
									grupocomandos.Add(nombregrupo, L_comandos);
								}
							}
						}
						d_commandsbyversion.Add(n_version, new version(n_version, grupocomandos));
						IncrementWork(100/xml_versions.Count, "Registrada Versión de Vulkan "+n_version);						
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
