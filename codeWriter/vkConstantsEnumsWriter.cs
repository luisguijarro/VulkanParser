using System.IO;

namespace VulkanParser;

public static partial class CodeWriter
{
    public static void WriteConstantsEnums(string output, bool verbose)
    {
        WriteConstants(output, verbose);
        WriteEnums(output, verbose);
    }

    public static void WriteConstants(string output, bool verbose)
    {
        FileStream fs = new FileStream(output + "/vkConstants.cs", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);

        sw.WriteLine("using System;");
        sw.WriteLine();
        sw.WriteLine($"namespace {VkNameSpace};");
        sw.WriteLine();
        sw.WriteLine("public static partial class VK");
        sw.WriteLine("{");

        //TODO: Escribir el fichero.
        foreach (string key in Parser.VkConstants.Keys)
        {
            VkConstant vkConstant = Parser.VkConstants[key];
            string? ConstantType = vkConstant.SharpType;
            if (vkConstant.Alias != null && vkConstant.Alias.Length > 0)
            {
                ConstantType = Parser.VkConstants[vkConstant.Alias].SharpType;
            }
            string line = $"\tpublic const {ConstantType} {vkConstant.Name}";
            line += $" = {((vkConstant.Value != null && vkConstant.Value.Length > 0) ? vkConstant.Value : vkConstant.Alias)};";
            line += $" {((vkConstant.Comment != null && vkConstant.Comment.Length > 0) ? "//" + vkConstant.Comment : "")}";
            sw.WriteLine(line);
        }
        sw.WriteLine("}");
        sw.WriteLine();

        sw.Close();
        fs.Close();
    }

    public static void WriteEnums(string output, bool verbose)
    {
        FileStream fs = new FileStream(output + "/vkEnums.cs", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);

        sw.WriteLine("using System;");
        sw.WriteLine();
        sw.WriteLine($"namespace {VkNameSpace};");
        sw.WriteLine();

        //TODO: Escribir el fichero.
        foreach (string key in Parser.Enums.Keys)
        {
            Enumerator enumerator = Parser.Enums[key];
            sw.WriteLine($"public enum {enumerator.name}");
            sw.WriteLine("{");

            foreach (enumValue ev in enumerator.enumValues.Values)
            {
                string line = $"\t{ev.Name} = {((ev.Value != null && ev.Value.Length > 0) ? ev.Value : ev.Alias)},";
                if (ev.Comment != null && ev.Comment.Length > 0)
                {
                    line += $" // {ev.Comment}";
                }
                sw.WriteLine(line);
            }

            sw.WriteLine("}");
            sw.WriteLine();
        }

        sw.Close();
        fs.Close();
    }
}