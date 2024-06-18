public class Animals
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string? Colour { get; set; }

    public double Cost { get; set; }
    public double Weight { get; set; }

    public int Numanimals { get; set; }

    public double GenStatsLabel { get; set; }

    public string? Type {get; set; }


}

[Table("Cow")]
public class Cow : Animals
{
    public string? Type { get; set; } = "Cow";
    public double Milk { get; set; }

    public override string ToString()
    {
        return $"{Id,-20}{Cost,-12}{Weight,-12}{Colour,-12}{Milk,-12},{Type,-12},{Numanimals, -12}";
    }
}


[Table("Sheep")]
public class Sheep : Animals
{
    public string? Type { get; set; } = "Sheep";
    public double Wool { get; set; }
    public override string ToString()
    {
        return $"{Id,-20}{Cost,-12}{Weight,-12}{Colour,-12}{Wool,-12},{Type,-12},{Numanimals,-12}";
    }
}