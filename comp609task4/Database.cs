using CoreBluetooth;
using SQLite;

namespace comp609task4
{
    internal class Database
    {
        private readonly SQLiteConnection _connection;
        public Database() //establish connection to database first
        {
            var dataDir = @"C:\Users\User\source\repos\comp609task4";
            var DBPath = Path.Combine(dataDir, "FarmDataOriginal.db");
            _connection = new SQLiteConnection(DBPath); //initialise the connection
            _connection.CreateTables<Cow, Sheep>();//create if its not existing yet
        }
        public List<Sheep> ReadSheep()
        {
            var sheeps = new List<Sheep>();
            var lst = _connection.Table<Sheep>().ToList();
            sheeps.AddRange(lst);
            return sheeps;
        }
        public List<Cow> ReadCow()
        {
            var cows = new List<Cow>();
            var lst = _connection.Table<Cow>().ToList();
            cows.AddRange(lst);
            return cows;
        }
    }
}
