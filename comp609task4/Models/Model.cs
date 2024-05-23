public class Animals
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string? Colour { get; set; }
    
    public double Cost {  get; set; }
    public double Weight { get; set; }
}

[Table("Cow")]
public class Cow : Animals
{
    public string? Animal { get; set; } = "Cow";
    public double Milk { get; set; }

    public override string ToString()
    {
        return $"{Id,-20}{Cost,-12}{Weight,-12}{Colour,-12}{Milk, -12}";
    }
}


[Table("Sheep")]
public class Sheep : Animals
{
    public string? Animal { get; set; } = "Sheep";
    public string? Wool { get; set; }
    public override string ToString()
    {
        return $"{Id,-20}{Cost,-12}{Weight,-12}{Colour,-12}{Wool,-12}";
    }
}