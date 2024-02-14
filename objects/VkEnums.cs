namespace VulkanParser;

public class Enumerator
{
    public string name;
    public string tipo;
    public Dictionary<string, enumValue> enumValues = new Dictionary<string, enumValue>();
    public Enumerator(string sName, string sTipo)
    {
        this.name = sName;
        this.tipo = sTipo;
    }
}

public class enumValue
{
    public string? Name;
    public string? Alias;
    public string? Value;
    public string? Comment;

    public enumValue(string? name, string? value, string? alias, string? comment)
    {
        this.Name = name;
        this.Value = value;
        this.Alias = alias;
        this.Comment = comment;
    }
}
