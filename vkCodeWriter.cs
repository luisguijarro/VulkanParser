using System;
using System.IO;

namespace VulkanParser;

public static partial class CodeWriter
{
    public static string VkNameSpace = "";
    public static void WriteCode(string vkNameSpace, string output, bool verbose)
    {
        VkNameSpace = vkNameSpace;
        if (!Directory.Exists(output))
        {
            Directory.CreateDirectory(output);
        }
        writeTypes(output, verbose);
    }
}
