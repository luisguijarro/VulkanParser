
using System.Xml;

namespace VulkanParser;

public static partial class Parser
{
    public static void Parse(string xmlFilePath, bool verbose)
    {
        XmlDocument xdoc = new XmlDocument();
        xdoc.Load(xmlFilePath);
        ParseBaseTypes(xdoc, verbose);
        ParseBitmaskTypes(xdoc, verbose);
        ParseDefineTypes(xdoc, verbose);
        ParseConstants(xdoc, verbose);
        ParseEnums(xdoc, verbose);
        ParseBitmasks(xdoc, verbose);
        ParseCommands(xdoc, verbose);
    }
}
