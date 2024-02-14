using System;
using System.Xml;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
namespace VulkanParser;

public static partial class Parser
{
    public static void ParseFeatures(XmlDocument xdoc, bool verbose, bool showErrors)
    {
        //VkCommands.Clear();
        if (verbose)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Geting Features");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"----------------------------------------------------------");
            Console.ResetColor();
        }

        XmlNodeList? xml_Features = xdoc.SelectNodes("/registry/feature[contains(@api, 'vulkan')]");
        if (xml_Features != null)
        {
            for (int i = 0; i < xml_Features?.Count; i++)
            {
                if (xml_Features[i] != null)
                {
                    bool isVulkan = xml_Features[i]?.Attributes?["api"]?.Value.Split(',').Any((value) => value == "vulkan") ?? false;
                    if (!isVulkan)
                    {
                        continue;
                    }

                    // TODO: Hacer cosas.
                    if (verbose)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"{xml_Features[i]?.Attributes?["name"]?.Value}");
                        Console.ResetColor();
                    }
                    XmlNodeList? xml_Requires = xml_Features[i]?.SelectNodes("require");
                    if (xml_Requires != null && xml_Requires.Count > 0)
                    {
                        for (int r = 0; r < xml_Requires?.Count; r++)
                        {
                            XmlNodeList? xml_Commands = xml_Requires[r]?.SelectNodes("command");
                            if (xml_Commands != null && xml_Commands.Count > 0)
                            {
                                if (verbose)
                                {
                                    string? comment = xml_Requires[r]?.Attributes?["comment"]?.Value;
                                    Console.WriteLine($"\t{comment}");
                                }
                                for (int c = 0; c < xml_Commands?.Count; c++)
                                {
                                    string? commandName = xml_Commands[c]?.Attributes?["name"]?.Value;
                                    if (commandName == null)
                                    {
                                        throw new Exception("[ParseFeatures] No name attribute on Command.");
                                    }
                                    if (verbose) { Console.WriteLine($"\t\tCommand: {commandName}"); }
                                }
                                if (verbose) { Console.WriteLine(); }
                            }
                        }
                    }
                }
            }
        }
    }
}
