using System;
using System.Xml;

namespace VulkanParser;

public static partial class Parser
{
    public static void Parse(string xmlFilePath, bool verbose, bool showErrors)
    {
        XmlDocument xdoc = new XmlDocument();
        xdoc.Load(xmlFilePath);
        ParseBaseTypes(xdoc, verbose, showErrors);
        ParseBitmaskTypes(xdoc, verbose, showErrors);
        ParseDefineTypes(xdoc, verbose, showErrors);
        ParseConstants(xdoc, verbose, showErrors);
        ParseEnums(xdoc, verbose, showErrors);
        ParseBitmasks(xdoc, verbose, showErrors);
        ParseCommands(xdoc, verbose, showErrors);
        ParseFeatures(xdoc, verbose, showErrors);

    }
}
