using System;
using System.Xml;
using System.Collections.Generic;

namespace VulkanParser
{
	/// <summary>
	/// Description of IntPtrsParser.
	/// </summary>
	public static class IntPtrsParser
	{
		public static List<string> IntPtrs;
		public static void Parse(XmlDocument xdoc)
		{
			IntPtrs = new List<string>();
			XmlNodeList xml_intptrs = xdoc.SelectNodes("/registry/types/type[@category='define']");
			for (int i=0;i<xml_intptrs.Count;i++)
			{
				if (xml_intptrs[i].InnerText.Contains("struct"))
				{
					if (xml_intptrs[i].SelectSingleNode("name") != null)
					{
						string s_name = xml_intptrs[i].SelectSingleNode("name").InnerText;
						IntPtrs.Add(s_name);
					}
				}
				if (xml_intptrs[i].InnerText.Contains("typedef void"))
				{
					if (xml_intptrs[i].SelectSingleNode("name") != null)
					{
						string s_name = xml_intptrs[i].SelectSingleNode("name").InnerText;
						IntPtrs.Add(s_name);
					}
				}
			}
			xml_intptrs = xdoc.SelectNodes("/registry/types/type[@requires='wayland-client.h']");
			for (int i=0;i<xml_intptrs.Count;i++)
			{
				
				if (xml_intptrs[i].Attributes["name"] != null)
				{
					string s_name = xml_intptrs[i].Attributes["name"].Value;
					IntPtrs.Add(s_name);
				}
			}
			
			xml_intptrs = xdoc.SelectNodes("/registry/types/type[@requires='X11/Xlib.h']");
			for (int i=0;i<xml_intptrs.Count;i++)
			{
				if (xml_intptrs[i].Attributes["name"] != null)
				{
					string s_name = xml_intptrs[i].Attributes["name"].Value;
					IntPtrs.Add(s_name);
				}
			}
			
			xml_intptrs = xdoc.SelectNodes("/registry/types/type[@requires='xcb/xcb.h']");
			for (int i=0;i<xml_intptrs.Count;i++)
			{
				if (xml_intptrs[i].Attributes["name"] != null)
				{
					string s_name = xml_intptrs[i].Attributes["name"].Value;
					IntPtrs.Add(s_name);
				}
			}
			
			xml_intptrs = xdoc.SelectNodes("/registry/types/type[@requires='windows.h']");
			for (int i=0;i<xml_intptrs.Count;i++)
			{
				if (xml_intptrs[i].Attributes["name"] != null)
				{
					string s_name = xml_intptrs[i].Attributes["name"].Value;
					IntPtrs.Add(s_name);
				}
			}
			
			xml_intptrs = xdoc.SelectNodes("/registry/types/type[@requires='zircon/types.h']");
			for (int i=0;i<xml_intptrs.Count;i++)
			{
				if (xml_intptrs[i].Attributes["name"] != null)
				{
					string s_name = xml_intptrs[i].Attributes["name"].Value;
					IntPtrs.Add(s_name);
				}
			}
			
			xml_intptrs = xdoc.SelectNodes("/registry/types/type[@requires='ggp_c/vulkan_types.h']");
			for (int i=0;i<xml_intptrs.Count;i++)
			{
				if (xml_intptrs[i].Attributes["name"] != null)
				{
					string s_name = xml_intptrs[i].Attributes["name"].Value;
					IntPtrs.Add(s_name);
				}
			}
		}
	}
}
