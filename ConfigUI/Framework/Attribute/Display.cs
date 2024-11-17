namespace ConfigUI.Framework.Attribute;

public class Display : System.Attribute
{
    private readonly string _name;
    private readonly string? _description;

    /// <summary>
    /// Display the name of the property of the config.
    /// </summary>
    /// <param name="name">Name of the property, can be translated.</param>
    /// <param name="description">Description of the property, can be translated</param>
    public Display(string name, string? description)
    {
        this._name = name;
        this._description = description;
    }

    public string GetName()
    {
        return this._name;
    }

    public string? GetDescription()
    {
        return this._description;
    }
}