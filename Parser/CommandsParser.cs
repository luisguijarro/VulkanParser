using System;
using System.Xml;
using System.Collections.Generic;
using System.Numerics;
namespace VulkanParser;

public static partial class Parser
{
    public static Dictionary<string, VkCommand> VkCommands = new Dictionary<string, VkCommand>();
    public static void ParseCommands(XmlDocument xdoc, bool verbose, bool showErrors)
    {
        VkCommands.Clear();
        if (verbose)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Geting Commands");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"----------------------------------------------------------");
            Console.ResetColor();
        }

        XmlNodeList? xml_CommandGroup = xdoc.SelectNodes("/registry/commands");
        if (xml_CommandGroup != null)
        {
            for (int i = 0; i < xml_CommandGroup?.Count; i++)
            {
                if (xml_CommandGroup[i] != null)
                {
                    XmlNodeList? xml_Commands = xml_CommandGroup[i]?.SelectNodes("command");
                    if (xml_Commands != null)
                    {
                        for (int c = 0; c < xml_Commands.Count; c++)
                        {
                            if (xml_Commands[c] != null)
                            {
                                XmlNode? proto = xml_Commands[c]?.SelectSingleNode("proto");
                                string? returnType = proto != null ? xml_Commands[c]?.SelectSingleNode("proto")?.SelectSingleNode("type")?.InnerText : null;
                                string? commandName = proto != null ? xml_Commands[c]?.SelectSingleNode("proto")?.SelectSingleNode("name")?.InnerText : xml_Commands[c]?.Attributes?["name"]?.Value;
                                string? commandAlias = proto == null ? xml_Commands[c]?.Attributes?["alias"]?.Value : null;
                                //string? commandAlias = alias

                                if ((returnType == null && commandAlias == null) || commandName == null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(xml_Commands[c]?.OuterXml);
                                    Console.ResetColor();
                                    throw new Exception($"Commando sin nombre o valor de Retorno.");
                                }

                                VkCommand command = new VkCommand(commandName, returnType);

                                if (verbose)
                                {
                                    if (returnType != null)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.Write($"{returnType} ");
                                    }
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"{commandName}");
                                    if (commandAlias != null)
                                    {
                                        Console.ResetColor();
                                        Console.Write($"\tAlias: ");
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        Console.WriteLine($"{commandAlias}");
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        Console.ResetColor();
                                        Console.WriteLine($"(");
                                    }
                                }
                                // TODO: Parse Params
                                XmlNodeList? xml_Params = xml_Commands[c]?.SelectNodes("param");
                                if (xml_Params != null)
                                {
                                    for (int p = 0; p < xml_Params.Count; p++)
                                    {
                                        string? paramName = xml_Params[p]?.SelectSingleNode("name")?.InnerText;
                                        string? paramType = xml_Params[p]?.SelectSingleNode("type")?.InnerText;
                                        bool isOptional = bool.TryParse(xml_Params[p]?.Attributes?["optional"]?.Value, out bool gettedBoolean) ? gettedBoolean : false;
                                        bool isPointer = xml_Params[p]?.InnerText.Contains("const") == true && xml_Params[p]?.InnerText.Contains('*') == true;
                                        if (verbose)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                                            Console.Write($"\t·{paramType}");
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.WriteLine($"\t{paramName}");
                                            Console.ResetColor();
                                            Console.WriteLine($"\t\t- isPointer: {isPointer}");
                                            Console.WriteLine($"\t\t- isOptional: {isOptional}");
                                        }
                                    }
                                }

                                if (verbose)
                                {
                                    if (commandAlias == null) { Console.WriteLine(")"); }
                                    Console.WriteLine();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
