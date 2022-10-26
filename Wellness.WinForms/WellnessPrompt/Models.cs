namespace Wellness.WinForms.WellnessPrompt;

internal class Category
{
    public List<string> Items { get; set; }
    public Color UiColor => System.Drawing.Color.FromArgb(Color[0], Color[1], Color[2]);
    public List<byte> Color { get; set; }
    public string Name { get; set; }
}

internal class Checkin
{
    public Checkin(string doing, string miscellaneous, List<FeelingType> feelings)
    {
        Doing = doing;
        Miscellaneous = miscellaneous;
        Feelings = feelings;
    }

    public string Doing { get; set; }
    public string Miscellaneous { get; set; }
    public List<FeelingType> Feelings { get; set; }
}

internal class FeelingType
{
    public FeelingType(string name, List<string> feelings)
    {
        Name = name;
        Feelings = feelings;
    }

    public string Name { get; set; }
    public List<string> Feelings { get; set; }
}
