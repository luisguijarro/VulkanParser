using System;
using System.Xml;
using System.Collections.Generic;
using System.Numerics;
namespace VulkanParser;

public static partial class Parser
{
    #region Constants
    public static Dictionary<string, VkConstant> VkConstants = new Dictionary<string, VkConstant>();
    public static void ParseConstants(XmlDocument xdoc, bool verbose, bool showErrors)
    {
        VkConstants.Clear();
        if (verbose)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Geting Constants");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"----------------------------------------------------------");
            Console.ResetColor();
        }

        XmlNodeList? xml_Constants = xdoc.SelectNodes("/registry/enums[@name='API Constants']");
        if (xml_Constants != null)
        {
            for (int i = 0; i < xml_Constants.Count; i++)
            {
                if (xml_Constants[i] != null)
                {
                    XmlNodeList? xml_enumValues = xml_Constants[i]?.SelectNodes("enum");
                    if (xml_enumValues != null && xml_enumValues.Count > 0)
                    {
                        for (int e = 0; e < xml_enumValues.Count; e++)
                        {
                            string? valueName = xml_enumValues[e]?.Attributes?["name"]?.Value;
                            string? valueVal = xml_enumValues[e]?.Attributes?["value"]?.Value;
                            string? valueType = TypeConversor.GetCSharpType(xml_enumValues[e]?.Attributes?["type"]?.Value);
                            string? valueAlias = xml_enumValues[e]?.Attributes?["alias"]?.Value;
                            string? valueComment = xml_enumValues[e]?.Attributes?["comment"]?.Value;

                            string? convertedVal = valueVal;
                            if (valueVal != null)
                            {
                                convertedVal = (valueVal.Contains("ULL") || valueVal.Contains("U")) ? ValuesConversor.FromCValue(valueVal) : valueVal;
                            }

                            VkConstant nconst = new VkConstant(valueName, valueType, convertedVal, valueAlias, valueComment);
                            if (valueName != null)
                            {
                                VkConstants.Add(valueName, nconst);
                            }

                            if (verbose)
                            {
                                Console.Write($"\tGetted Constant ");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine($"{nconst.Name}");
                                Console.ResetColor();
                                Console.Write($"\t\t");
                                if (valueVal != null)
                                {
                                    //string convertedVal = (valueVal.Contains("ULL") || valueVal.Contains("U")) ? ValuesConversor.FromCValue(valueVal) : valueVal;
                                    Console.Write($"Value: ");
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write($"{valueVal} ");
                                    if (valueVal.Contains("ULL") || valueVal.Contains("U")) { Console.Write($"-> {nconst.Value} "); }
                                    Console.ResetColor();
                                }
                                if (valueType != null)
                                {
                                    Console.Write($"Type: ");
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write($"{nconst.SharpType} ");
                                    Console.ResetColor();
                                }
                                if (valueAlias != null)
                                {
                                    Console.Write($"Alias: ");
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.Write($"{nconst.Alias} ");
                                    Console.ResetColor();
                                }
                                Console.WriteLine();
                                if (valueComment != null)
                                {
                                    Console.Write($"\t\tComment: ");
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine($"{nconst.Comment}");
                                    Console.ResetColor();
                                }
                            }
                        }
                    }
                }
                //Enumerator enumToAdd = new Enumerator();
            }
        }

        if (verbose)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Getted {VkConstants.Count} API Constants.");
            Console.ResetColor();
        }
    }
    #endregion

    #region Enumerators
    public static Dictionary<string, Enumerator> Enums = new Dictionary<string, Enumerator>();
    public static void ParseEnums(XmlDocument xdoc, bool verbose, bool showErrors)
    {
        if (verbose)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Geting Enums of type enum");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"----------------------------------------------------------");
            Console.ResetColor();
        }

        XmlNodeList? xml_Enums = xdoc.SelectNodes("/registry/enums[@name!='API Constants' and @type='enum']");
        if (xml_Enums != null)
        {
            for (int i = 0; i < xml_Enums.Count; i++)
            {
                if (xml_Enums[i] != null)
                {
                    string? enumName = xml_Enums[i]?.Attributes?["name"]?.Value;
                    string? enumComment = xml_Enums[i]?.Attributes?["comment"]?.Value;
                    // TODO: Determine Type
                    if (enumName == null)
                    {
                        throw new Exception("[ParseBitmasks] enumName cannot be null.");
                    }
                    Enumerator vkenum = new Enumerator(enumName, "");
                    if (verbose)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"\tEnum {enumName}");
                        Console.ResetColor();
                    }
                    XmlNodeList? xml_enumValues = xml_Enums[i]?.SelectNodes("enum");
                    if (xml_enumValues != null && xml_enumValues.Count > 0)
                    {
                        for (int e = 0; e < xml_enumValues.Count; e++)
                        {
                            string? valueName = xml_enumValues[e]?.Attributes?["name"]?.Value;
                            string? valueVal = xml_enumValues[e]?.Attributes?["value"]?.Value;
                            //string? valueType = xml_enumValues[e]?.Attributes?["type"]?.Value;
                            string? valueAlias = xml_enumValues[e]?.Attributes?["alias"]?.Value;
                            string? valueComment = xml_enumValues[e]?.Attributes?["comment"]?.Value;

                            if (valueName == null)
                            {
                                throw new Exception("Enum Name cannot be null");
                            }

                            enumValue ev = new enumValue(valueName, valueVal, valueAlias, valueComment);
                            vkenum.enumValues.Add(valueName, ev);

                            if (verbose)
                            {
                                Console.Write($"\t\t- Name:");
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine($" {valueName}");
                                Console.ResetColor();
                                if (valueVal != null)
                                {
                                    Console.WriteLine($"\t\t\t- Value: {valueVal}");
                                }
                                if (valueAlias != null)
                                {
                                    Console.Write($"\t\t\t- Alias:");
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine($" {valueAlias}");
                                    Console.ResetColor();
                                }
                                if (valueComment != null)
                                {
                                    Console.WriteLine($"\t\t\t- Comment: {valueComment}");
                                }
                            }
                        }
                    }
                    Enums.Add(vkenum.name, vkenum);
                }
                //Enumerator enumToAdd = new Enumerator();
            }
        }
        /*
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Getted {Enums.Count} Enums Type enum.");
        Console.ResetColor();
        */
    }

    public static void ParseBitmasks(XmlDocument xdoc, bool verbose, bool showErrors)
    {
        if (verbose)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Geting Enums of type bitmask");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"----------------------------------------------------------");
            Console.ResetColor();
        }

        // Value = 2 ^ bitpos;
        //Math.Pow(2, bitpos);

        XmlNodeList? xml_Enums = xdoc.SelectNodes("/registry/enums[@name!='API Constants' and @type='bitmask']");
        if (xml_Enums != null)
        {
            for (int i = 0; i < xml_Enums.Count; i++)
            {
                if (xml_Enums[i] != null)
                {
                    string? enumName = xml_Enums[i]?.Attributes?["name"]?.Value;
                    string? enumComment = xml_Enums[i]?.Attributes?["comment"]?.Value;
                    // TODO: Determine Type
                    if (enumName == null)
                    {
                        throw new Exception("[ParseBitmasks] enumName cannot be null.");
                    }
                    Enumerator vkenum = new Enumerator(enumName, "");
                    if (verbose)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"\tEnum {enumName}");
                        Console.ResetColor();
                    }
                    XmlNodeList? xml_enumValues = xml_Enums[i]?.SelectNodes("enum");
                    if (xml_enumValues != null && xml_enumValues.Count > 0)
                    {
                        for (int e = 0; e < xml_enumValues.Count; e++)
                        {
                            string? valueName = xml_enumValues[e]?.Attributes?["name"]?.Value;
                            string? valueVal = xml_enumValues[e]?.Attributes?["value"]?.Value;
                            string? valueBitPos = xml_enumValues[e]?.Attributes?["bitpos"]?.Value;
                            string? valueAlias = xml_enumValues[e]?.Attributes?["alias"]?.Value;
                            string? valueComment = xml_enumValues[e]?.Attributes?["comment"]?.Value;
                            if (valueVal == null)
                            {
                                if (valueBitPos != null)
                                {
                                    valueVal = Math.Pow(2, (double)uint.Parse(valueBitPos)).ToString();
                                }
                            }


                            if (valueName == null)
                            {
                                throw new Exception("Enum Name cannot be null");
                            }

                            enumValue ev = new enumValue(valueName, valueVal, valueAlias, valueComment);
                            vkenum.enumValues.Add(valueName, ev);

                            if (verbose)
                            {
                                Console.Write($"\t\t- Name:");
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine($" {valueName}");
                                Console.ResetColor();
                                if (valueVal != null)
                                {
                                    Console.WriteLine($"\t\t\t- Value: {valueVal}");
                                }
                                if (valueAlias != null)
                                {
                                    Console.Write($"\t\t\t- Alias:");
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine($" {valueAlias}");
                                    Console.ResetColor();
                                }
                                if (valueComment != null)
                                {
                                    Console.WriteLine($"\t\t\t- Comment: {valueComment}");
                                }
                            }
                        }
                    }

                    Enums.Add(vkenum.name, vkenum);
                }
            }
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Getted {Enums.Count} Enums.");
        Console.ResetColor();
    }

    #endregion

    private static string BestType(string newType)
    {
        // TODO: Determinar mejor tipo.
        return "";
    }
}
