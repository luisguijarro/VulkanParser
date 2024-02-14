using System;
using System;
using System.Xml;
using System.Collections.Generic;
using System.Drawing;

namespace VulkanParser;

public static partial class Parser
{
    public static Dictionary<string, string> BaseTypesList = new Dictionary<string, string>(); // name, type

    public static void ParseBaseTypes(XmlDocument xdoc, bool verbose, bool showErrors)
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
                        if (typeType == "void")
                        {
                            if (xml_baseTypes[i].InnerXml.Contains("*"))
                            {
                                typeType = "IntPtr";
                            }
                        }

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

    public static void ParseBitmaskTypes(XmlDocument xdoc, bool verbose, bool showErrors)
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

    public static Dictionary<string, VkDefineType> DefineTypesList = new Dictionary<string, VkDefineType>(); // name, type

    public static void ParseDefineTypes(XmlDocument xdoc, bool verbose, bool showErrors)
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
        XmlNodeList? xml_DefineTypes = xdoc.SelectNodes("/registry/types/type[((contains(@api, 'vulkan') and @api!='vulkansc') or not(@api)) and (@deprecated='false' or not(@deprecated)) and @category='define']");
        if (xml_DefineTypes != null)
        {
            for (int i = 0; i < xml_DefineTypes.Count; i++)
            {
                if (xml_DefineTypes[i] != null)
                {
                    if (!xml_DefineTypes[i].InnerText.Contains("VKSC_"))
                    {
                        XmlNode? nameNode = xml_DefineTypes[i]?.SelectSingleNode("name");
                        XmlNode? cTypeNode = xml_DefineTypes[i]?.SelectSingleNode("type");

                        if (nameNode != null)
                        {
                            Console.WriteLine();
                            string typeName = nameNode.InnerText;

                            string innerText = xml_DefineTypes[i].InnerText;
                            Console.WriteLine(innerText);


                            Console.WriteLine($"DefineType: {typeName}");
                            // Removing #Define:
                            innerText = innerText.Replace("#define ", "");

                            if (cTypeNode == null) // Be C Macros -> C# Methods or uint value.
                            {
                                uint uintValue = 0;

                                if (uint.TryParse(nameNode.NextSibling.InnerText, out uintValue))
                                {
                                    DefineTypesList.Add(typeName, new VkDefineType(TypeOfDefType.Constant, typeName, null, uintValue.ToString()));
                                }
                                else
                                {
                                    // Separate and Asign type to params
                                    string vkParams = innerText.Split('(')[1].Split(')')[0].Replace(innerText.Split('(')[1].Split(')')[0], "uint " + string.Join(", uint", innerText.Split('(')[1].Split(')')[0].Split(',')));
                                    Console.WriteLine($"\tParams: {vkParams}");

                                    // Separating Values that will be returned by Method / Macro
                                    string vKValue = innerText.Replace(typeName, "").Replace("(" + innerText.Split('(')[1].Split(')')[0] + ")", "");
                                    Console.WriteLine($"\tvKValue: {vKValue}");

                                    // Adding VKType to Collection:
                                    DefineTypesList.Add(typeName, new VkDefineType(TypeOfDefType.Method, typeName, vkParams, vKValue));
                                }
                            }
                            else // Can be constants. Some of these can call another Methods / Macros
                            {
                                string typeType = TypeConversor.GetCSharpType(cTypeNode.InnerXml) ?? "";

                                string vKValue = cTypeNode.NextSibling.InnerText;

                                // Adding VKType to Collection:
                                DefineTypesList.Add(typeName, new VkDefineType(TypeOfDefType.Constant, typeName, typeType, vKValue));

                                if (verbose || showErrors) { Console.WriteLine($"\tGetted Type {typeName}, {typeType} {vKValue}"); }
                            }
                        }
                        /*
                        else if (nameNode != null)
                        {
                            if (verbose || showErrors)
                            {
                                string typeName = nameNode.InnerText;
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"\t\tType {typeName}");
                                Console.ResetColor();
                            }
                        }
                        */
                    }
                }
            }
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Getted {DefineTypesList.Count} Define Types.");
        Console.ResetColor();
    }
}
