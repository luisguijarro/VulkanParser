using System;
using System.Xml;
using System.Collections.Generic;

using VulkanParser.objetos;

namespace VulkanParser
{
	/// <summary>
	/// Description of CommandParser.
	/// </summary>
	public static class CommandParser
	{
		private static float f_Work;
		public static Dictionary<string, Metodo> Metodos;
		public static void Parse(XmlDocument xdoc)
		{
			Metodos = new Dictionary<string, Metodo>();
			XmlNodeList xml_commands = xdoc.SelectNodes("/registry/commands/command");
			float incremento = 100f/(float)xml_commands.Count;
			if (xml_commands.Count>0)
			{
				for (int i=0;i<xml_commands.Count;i++)
				{
					string s_name = "";
					if (xml_commands[i].Attributes["alias"] == null)
					{
						string s_return = xml_commands[i].SelectSingleNode("./proto/type").InnerText; //VALOR RETORNADO
						s_name = xml_commands[i].SelectSingleNode("./proto/name").InnerText; //NOMBRE DEL METODO
						if (s_name == "vkCreateInstance")
                        {
                            Console.WriteLine("Mierda");
                        }
                        Metodo metodo = new Metodo(s_name);
						metodo.ValueReturned = s_return;
						
						XmlNodeList xml_params = xml_commands[i].SelectNodes("./param");
						if (xml_params.Count > 0)
						{
							for (int ip=0;ip<xml_params.Count;ip++) //PARAMETROS
							{
								string s_tpvalue = xml_params[ip].SelectSingleNode("./type").InnerText; //TIPO DE VALOR
								string s_pname = xml_params[ip].SelectSingleNode("./name").InnerText; //NOMBRE DEL PARAMETRO
								
								Parametro parametro = new Parametro(s_pname);
								parametro.TipoValor = s_tpvalue;
								
								parametro.puntero = xml_params[ip].InnerText.Contains("*"); //¿PUNTERO?
								
								parametro.constante = xml_params[ip].InnerText.Contains("const"); //¿CONSTANTE?
                                parametro.esarray = xml_params[ip].Attributes["len"] != null ? true : false; //¿ES ARRAY?

                                metodo.Parametros.Add(s_pname, parametro);
							}
						}
						
						//ALMACENAR INFO
						Metodos.Add(s_name, metodo);
					}
					else
					{
						//ALIAS
						s_name = xml_commands[i].Attributes["name"].Value;
						string s_alias = xml_commands[i].Attributes["alias"].Value;
						Metodos.Add(s_name, new Metodo(s_name, s_alias));
					}
					
					IncrementWork(incremento, "Leido Metodo "+s_name+".");
				}
				IncrementWork(0, "\r\n ---------- Leidos "+xml_commands.Count+" Metodos. --------- \r\n");
			}
			else
			{
				throw new Exception("No se han encontrado comandos");
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
