namespace VulkanParser;

public class VkDefineType
{
    public TypeOfDefType typeOfDefType;
    public string DefineTypeName = "";
    public string DefineTypeValue = "";
    public string ArgumentsMethod = "";
    public VkDefineType(TypeOfDefType type, string defineTypeName, string? argumentsMethod, string defineTypeValue)
    {
        this.typeOfDefType = type;
        this.DefineTypeName = defineTypeName;
        // En la asignación limpiamos.
        this.DefineTypeValue = defineTypeValue.Replace(" \\", "").Replace("\n", "").Replace("    ", "");
        if (argumentsMethod != null)
        {
            this.ArgumentsMethod = argumentsMethod;
        }
    }
}

public enum TypeOfDefType
{
    Method = 0,
    Constant = 1
}