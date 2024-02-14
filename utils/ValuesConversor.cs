namespace VulkanParser;
public static class ValuesConversor
{
    public static string FromCValue(string cValue)
    {
        return cValue.Replace("(", "").Replace(")", "").Replace("ULL", "ul").Replace("U", "u");
    }
}
