namespace VulkanParser;

public class VkConstant
{
    public string? SharpType;
    public string? Name;
    public string? Value;
    public string? Alias;
    public string? Comment;
    public VkConstant(string? name, string? sharpType, string? value, string? alias, string? comment)
    {
        this.Name = name;
        this.SharpType = sharpType;
        this.Value = value;
        this.Alias = alias;
        this.Comment = comment;
    }
}
