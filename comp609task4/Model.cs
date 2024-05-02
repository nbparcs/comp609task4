public class Animals
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Colour { get; set; }
    public double Milk { get; set; }
    public double Cost {  get; set; }
    public double Weight { get; set; }
}

[Table("Cow")]
public class Cow : Animals
{
    //public int SalesVolume { get; set; }
    public override string ToString()
    {
        return $"{Milk,-12}{Id,-20}{Cost,-12}{Weight,-12}{Colour,-12}";
    }
}

public class Sheep
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Colour { get; set; }
    public double Wool { get; set; }
    public double Cost { get; set; }
    public double Weight { get; set; }
}