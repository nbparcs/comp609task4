namespace comp609task4.Models;

public class Database
{
    private readonly SQLiteConnection _connection;
    public Database() //establish connection to database first
    {
        string dbName = "FarmDataOriginal.db";

        string dbPath = Path.Combine(Current.AppDataDirectory, dbName); //GlobalUsings FileSystems
        if (!File.Exists(dbPath))
        {
            using Stream stream = Current.OpenAppPackageFileAsync(dbName).Result; //Open  the db file from the asset folder

            using MemoryStream memoryStream = new();
            stream.CopyTo(memoryStream);
            File.WriteAllBytes(dbPath, memoryStream.ToArray()); //write db data to app directory a
        }
        _connection = new SQLiteConnection(dbPath); //initialise the connection
        _connection.CreateTables<Cow, Sheep>();//create if its not existing yet
    }
    public List<Animals> ReadItems()
    {
        var animals = new List<Animals>();
        var lst1 = _connection.Table<Cow>().ToList(); //read cow
        animals.AddRange(lst1);
        var lst2 = _connection.Table<Sheep>().ToList(); //read sheep
        animals.AddRange(lst2);
        return animals;
    }
    public List<Cow> ReadCows()
    {
        return _connection.Table<Cow>().ToList();
    }

    public List<Sheep> ReadSheeps()
    {
        return _connection.Table<Sheep>().ToList();
    }

    public int DeleteItem(Animals item)
    {
        // Delete the item from the database and return the number of rows affected
        return _connection.Delete(item);
    }
}
