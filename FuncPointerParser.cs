using System;
using System.Xml;
using System.Collections.Generic;
using VulkanParser.objetos;

namespace VulkanParser
{
	/// <summary>
	/// Description of FuncPointerParser. Usar: Marshal.GetDelegateForFunctionPointer
	/// </summary>
	public static class FuncPointerParser
	{
		//private static float f_Work;
		public static Dictionary<string, string> funPointers;
		public static void Parse(XmlDocument xdoc)
		{
			//f_Work = 0f;
			funPointers = new Dictionary<string, string>();
			XmlNodeList xml_funcPointers = xdoc.SelectNodes("/registry/types/type[@category='funcpointer']");
			for (int i=0;i<xml_funcPointers.Count;i++)
			{
				string funcion = "";
				XmlNode fp_node = xml_funcPointers[i];
				List<string> fp_lista = new List<string>();				
				string[] stodo = fp_node.InnerXml.Split();
				for (int l=0;l<stodo.Length;l++)
				{
					if (stodo[l].Length > 0)
					{
						fp_lista.Add(stodo[l]);
					}
				}
				string s_tipo = stodo[1];
				string s_name = fp_node.SelectSingleNode("name").InnerText;
				//XmlNodeList xml_paramList = fp_node.SelectNodes("type");
				
				funcion = s_tipo;
				string fname = fp_lista[3].Replace("<name>", "").Replace("</name>)", "");
				if (fname[0] == '*')
				{
					//funcion += "*";
					funcion += " "+fname.Remove(0,1);
				}
				
				
				for (int p=4;p<fp_lista.Count;p+=2)
				{
					string s_ptipo = fp_lista[p].Replace("<type>", "").Replace("</type>", "");
					string s_pname = fp_lista[p+1];
					if (s_ptipo == "const")
					{
						funcion += s_ptipo+" ";
						funcion += s_pname.Replace("<type>", "").Replace("</type>", "")+" ";
						funcion += fp_lista[p+2]+" ";
						p++;
					}
					else
					{
						funcion += s_ptipo+" ";
						funcion += s_pname+" ";
					}
				}
				funcion = TypeReplacer.Replace(funcion);
				funPointers.Add(s_name, funcion);
			}
		}
	}
}
