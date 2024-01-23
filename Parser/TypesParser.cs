using System;
using System.Xml;
using System.Collections.Generic;
using System.Drawing;

namespace VulkanParser;

public static partial class Parser
{
    public static Dictionary<string, string> BaseTypesList = new Dictionary<string, string>(); // name, type

    public static void ParseBaseTypes(XmlDocument xdoc, bool verbose)
    {

        if (verbose)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Geting base Types");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"----------------------------------------------------------");
            Console.ResetColor();
        }
        BaseTypesList.Clear();
        XmlNodeList? xml_baseTypes = xdoc.SelectNodes("/registry/types/type[@category='basetype' and (@deprecated='false' or not(@deprecated))]");
        if (xml_baseTypes != null)
        {
            for (int i = 0; i < xml_baseTypes.Count; i++)
            {
                if (xml_baseTypes[i] != null)
                {
                    XmlNode? nameNode = xml_baseTypes[i]?.SelectSingleNode("name");
                    XmlNode? cTypeNode = xml_baseTypes[i]?.SelectSingleNode("type");

                    if (nameNode != null && cTypeNode?.InnerText != null)
                    {
                        string typeName = nameNode.InnerText;
                        string typeType = TypeConversor.GetCSharpType(cTypeNode.InnerText) ?? "";

                        BaseTypesList.Add(typeName, typeType);

                        if (verbose) { Console.WriteLine($"\tGetted Type {typeName}, {typeType}"); }
                    }

                }
            }
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Getted {BaseTypesList.Count} Base Types.");
        Console.ResetColor();
    }

    public static Dictionary<string, string> BitmaskTypesList = new Dictionary<string, string>(); // name, type

    public static void ParseBitmaskTypes(XmlDocument xdoc, bool verbose)
    {
        if (verbose)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Geting bitmask Types");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"----------------------------------------------------------");
            Console.ResetColor();
        }
        BitmaskTypesList.Clear();
        XmlNodeList? xml_BitmaskTypes = xdoc.SelectNodes("/registry/types/type[@category='bitmask' and (@deprecated='false' or not(@deprecated))]");
        if (xml_BitmaskTypes != null)
        {
            for (int i = 0; i < xml_BitmaskTypes.Count; i++)
            {
                if (xml_BitmaskTypes[i] != null)
                {
                    XmlNode? nameNode = xml_BitmaskTypes[i]?.SelectSingleNode("name");
                    XmlNode? cTypeNode = xml_BitmaskTypes[i]?.SelectSingleNode("type");

                    if (nameNode != null && cTypeNode != null)
                    {
                        string typeName = nameNode.InnerText;
                        string typeType = TypeConversor.GetCSharpType(cTypeNode.InnerText) ?? "";

                        if (!BitmaskTypesList.ContainsKey(typeName))
                        {
                            BitmaskTypesList.Add(typeName, typeType);

                            if (verbose) { Console.WriteLine($"\tGetted Type {typeName}, {typeType}"); }
                        }
                    }

                }
            }
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Getted {BitmaskTypesList.Count} Bitmask Types.");
        Console.ResetColor();
    }

    public static Dictionary<string, string> DefineTypesList = new Dictionary<string, string>(); // name, type

    public static void ParseDefineTypes(XmlDocument xdoc, bool verbose)
    {
        if (verbose)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Geting define Types");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"----------------------------------------------------------");
            Console.ResetColor();
        }
        DefineTypesList.Clear();
        XmlNodeList? xml_DefineTypes = xdoc.SelectNodes("/registry/types/type[(@api='vulkan' or not(@api)) and (@deprecated='false' or not(@deprecated)) and @category='define']");
        if (xml_DefineTypes != null)
        {
            for (int i = 0; i < xml_DefineTypes.Count; i++)
            {
                if (xml_DefineTypes[i] != null)
                {
                    XmlNode? nameNode = xml_DefineTypes[i]?.SelectSingleNode("name");
                    XmlNode? cTypeNode = xml_DefineTypes[i]?.SelectSingleNode("type");

                    if (nameNode != null && cTypeNode != null)
                    {
                        string typeName = nameNode.InnerText;
                        string typeType = TypeConversor.GetCSharpType(cTypeNode.InnerText) ?? "";

                        if (!DefineTypesList.ContainsKey(typeName) && !typeName.Contains("VKSC_"))
                        {
                            DefineTypesList.Add(typeName, typeType);

                            if (verbose) { Console.WriteLine($"\tGetted Type {typeName}, {typeType}"); }
                        }
                    }
                    else if (nameNode != null)
                    {
                        if (verbose)
                        {
                            string typeName = nameNode.InnerText;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"\t\tType {typeName}");
                            Console.ResetColor();
                        }
                    }

                }
            }
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Getted {DefineTypesList.Count} Define Types.");
        Console.ResetColor();
    }
}
