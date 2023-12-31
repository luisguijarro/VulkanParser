using System;
using System.Xml;
using System.Collections.Generic;

namespace VulkanParser
{
    public static partial class Parser
    {
        public static Dictionary<string, string> BaseTypesList = new Dictionary<string, string>(); // name, type
        public static void ParseBaseTypes(XmlDocument xdoc, bool verbose)
        {
            BaseTypesList.Clear();
            XmlNodeList? xml_baseTypes = xdoc.SelectNodes("/registry/types/type[@category='basetype']");
            if (xml_baseTypes != null)
            {
                for (int i = 0; i < xml_baseTypes.Count; i++)
                {
                    if (xml_baseTypes[i] != null)
                    {
                        XmlNode? nameNode = xml_baseTypes[i]?.SelectSingleNode("name");
                        XmlNode? cTypeNode = xml_baseTypes[i]?.SelectSingleNode("type");

                        if (nameNode != null && cTypeNode != null)
                        {
                            string typeName = nameNode.InnerText;
                            string typeType = TypeConversor.GetCSharpType(cTypeNode.InnerText);

                            BaseTypesList.Add(typeName, typeType);

                            if (verbose) { Console.WriteLine($"Getted Type {typeName}, {typeType}"); }
                        }

                    }
                }
            }
        }
    }
}