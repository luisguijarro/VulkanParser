namespace VulkanParser
{
    public static class TypeConversor
    {
        public static string? GetCSharpType(string? cType)
        {
            switch (cType)
            {
                case "uint8_t":
                    return "byte";
                case "uint16_t":
                    return "ushort";
                case "uint32_t":
                    return "uint";
                case "uint64_t":
                    return "ulong";

                case "int8_t":
                    return "sbyte";
                case "int16_t":
                    return "short";
                case "int32_t":
                    return "int";
                case "int64_t":
                    return "long";

                case "size_t":
                    return "IntPtr";

                default:
                    return cType;
            }
        }
    }
}