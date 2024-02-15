using System;
using System.IO;

namespace VulkanParser;
public static partial class CodeWriter
{
    public static void writeTypes(string output, bool verbose)
    {
        FileStream fs = new FileStream(output + "/vkTypes.cs", FileMode.Truncate);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine("using System;");
        sw.WriteLine();
        sw.WriteLine($"namespace {VkNameSpace};");
        sw.WriteLine();

        WriteBaseTypes(sw, verbose);
        sw.WriteLine();
        WriteBitmaskTypes(sw, verbose);
        sw.WriteLine();
        WriteDefineTypes(sw, verbose);

        sw.Close();
        fs.Close();
    }

    public static void WriteBaseTypes(StreamWriter sw, bool verbose)
    {
        sw.WriteLine("#region BaseTypes");
        foreach (string key in VulkanParser.Parser.BaseTypesList.Keys)
        {
            string typeAlias = $"using {key} = {VulkanParser.Parser.BaseTypesList[key]};";
            sw.WriteLine(typeAlias);
        }
        sw.WriteLine("#endregion");
    }


    public static void WriteBitmaskTypes(StreamWriter sw, bool verbose)
    {
        sw.WriteLine("#region BitmaskTypes");
        foreach (string key in VulkanParser.Parser.BitmaskTypesList.Keys)
        {
            string typeAlias = $"using {key} = {VulkanParser.Parser.BitmaskTypesList[key]};";
            sw.WriteLine(typeAlias);
        }
        sw.WriteLine("#endregion");
    }

    public static void WriteDefineTypes(StreamWriter sw, bool verbose)
    {
        sw.WriteLine("public static partial class VK");
        sw.WriteLine("{");
        sw.WriteLine("\t#region DefineTypes Macros:");
        foreach (string key in VulkanParser.Parser.DefineTypesList.Keys)
        {
            VkDefineType df = VulkanParser.Parser.DefineTypesList[key];
            if (df.typeOfDefType == TypeOfDefType.Method)
            {
                string line = $"\tpublic static uint {df.DefineTypeName} ({df.ArgumentsMethod})";
                sw.WriteLine(line);
                sw.WriteLine("\t{");
                sw.WriteLine($"\t\treturn {df.DefineTypeValue.Replace("uint32_t", "uint")};");
                sw.WriteLine("\t}");
            }
        }
        sw.WriteLine("\t#endregion");
        sw.WriteLine();
        sw.WriteLine("\t#region DefineTypes Constants:");
        foreach (string key in VulkanParser.Parser.DefineTypesList.Keys)
        {
            VkDefineType df = VulkanParser.Parser.DefineTypesList[key];
            if (df.typeOfDefType == TypeOfDefType.Constant)
            {
                string line = $"\tpublic const uint {df.DefineTypeName} = {(df.ArgumentsMethod.Length > 0 ? df.ArgumentsMethod + " " : "")}{df.DefineTypeValue.Split("//")[0].Trim() + ";"}";
                sw.WriteLine(line);
            }
        }
        sw.WriteLine("\t#endregion");
        sw.WriteLine("}");
    }
}
