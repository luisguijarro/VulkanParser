namespace VulkanParser;

public class VkCommand
{
    public List<string> successcodes = new List<string>();
    public List<string> errorcodes = new List<string>();
    public string returnType;
    public string CommandName;
    public List<VkCommandParam> VkParams = new List<VkCommandParam>();

    public VkCommand(string CommandName, string returnType)
    {
        this.CommandName = CommandName;
        this.returnType = returnType;
    }
}

public class VkCommandParam
{
    public string paramName;
    public string paramType;
    public bool isPointer;
    public bool isOptional;

    public VkCommandParam(string paramName, string paramType)
    {
        this.paramName = paramName;
        this.paramType = paramType;
    }
}