namespace VulkanParser;

public static class Utils
{
    public static string deduceBestValueType(string[] values)
    {
        return "";
    }
    private static Type GetOptimalType(string Value)
    {
        sbyte sbyteVal = 0;
        byte byteVal = 0;
        uint uintVal = 0;
        int intVal = 0;
        ulong ulongVal = 0;
        long longVal = 0;
        float floatVal = 0;
        double doubleVal = 0;


        if (sbyte.TryParse(Value, out sbyteVal))
        {
            return typeof(sbyte);
        }

        if (byte.TryParse(Value, out byteVal))
        {
            return typeof(byte);
        }

        if (uint.TryParse(Value, out uintVal))
        {
            return typeof(uint);
        }

        if (int.TryParse(Value, out intVal))
        {
            return typeof(int);
        }

        if (ulong.TryParse(Value, out ulongVal))
        {
            return typeof(ulong);
        }

        if (long.TryParse(Value, out longVal))
        {
            return typeof(long);
        }

        if (float.TryParse(Value, out floatVal))
        {
            return typeof(float);
        }

        if (double.TryParse(Value, out doubleVal))
        {
            return typeof(double);
        }

        return typeof(string);
    }
}
