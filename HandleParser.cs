using System;
using System.Xml;
using System.Collections.Generic;

namespace VulkanParser
{
	/// <summary>
	/// Description of HandleParser.
	/// </summary>
	public static class HandleParser
	{
		public static List<string> handleTypes;
		public static void Parse(XmlDocument xdoc)
		{
			XmlNodeList xml_lhandlers = xdoc.SelectNodes("/registry/types/type[@category='handle']");
			handleTypes = new List<string>();
			for (int i=0;i<xml_lhandlers.Count;i++)
			{
				if (xml_lhandlers[i].SelectSingleNode("name") != null)
				{
					handleTypes.Add(xml_lhandlers[i].SelectSingleNode("name").InnerText);
				}
				else
				{
					handleTypes.Add(xml_lhandlers[i].Attributes["name"].Value);
				}
			}

            XmlNodeList xml_includes = xdoc.SelectNodes("/registry/types/type[@category='include']");
            foreach (XmlNode nodo_include in xml_includes)
            {
                if (nodo_include.Attributes["name"].Value != "vk_platform")
                {
                    XmlNodeList nodes_includes = xdoc.SelectNodes("/registry/types/type[@requires='" + nodo_include.Attributes["name"].Value + "']");
                    foreach (XmlNode nodo_handle in nodes_includes)
                    {
                        if (!handleTypes.Contains(nodo_handle.Attributes["name"].Value))
                        {
                            handleTypes.Add(nodo_handle.Attributes["name"].Value);
                        }
                    }
                }
            }

            XmlNodeList xml_defines = xdoc.SelectNodes("/registry/types/type[@category='define']");
            foreach (XmlNode nodo_define in xml_defines)
            {
                if (nodo_define.InnerText.Contains("struct"))
                {
                    if (nodo_define.SelectSingleNode("name") != null)
                    {
                        if (!handleTypes.Contains(nodo_define.SelectSingleNode("name").InnerText))
                        {
                            handleTypes.Add(nodo_define.SelectSingleNode("name").InnerText);
                        }
                    }
                }
            }

            handleTypes.Add("CAMetalLayer");
        }
	}
}
