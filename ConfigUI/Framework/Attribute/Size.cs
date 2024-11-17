namespace ConfigUI.Framework.Attribute;

public class Size : System.Attribute
{
    private readonly string _min;
    private readonly string _max;

    /// <summary>
    /// Limit the value of the property.
    /// </summary>
    /// <param name="min">Min value or Min length of the property</param>
    /// <param name="max">Max value of Max length the property</param>
    public Size(string min, string max)
    {
        _min = min;
        _max = max;
    }

    public string GetMin()
    {
        return _min;
    }

    public string GetMax()
    {
        return _max;
    }
}