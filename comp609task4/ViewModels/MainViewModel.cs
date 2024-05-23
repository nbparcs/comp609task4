namespace comp609task4.ViewModels;

public class MainViewModel
{
    public ObservableCollection<Animals> Animals { get; set; }
    public readonly Database _database;
    public MainViewModel()
    {
        _database = new();
        Animals = new();
        _database.ReadItems().ForEach(x => Animals.Add(x));
    }
}
