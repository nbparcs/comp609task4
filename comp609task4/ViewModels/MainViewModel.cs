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

    public string GetTotalTax()
    {
        // Retrieve data from the database
        var cows = _database.ReadCows();
        var sheeps = _database.ReadSheeps();

        // Calculate total tax for cows (sum of milk * 0.2 * 30)
        double cowTax = cows.Sum(cow => cow.Milk * 0.2);

        // Calculate total tax for sheep (sum of wool * 0.2 * 30)
        double sheepTax = sheeps.Sum(sheep => sheep.Wool * 0.2);

        // Sum the taxes from both cows and sheep
        double totalTax = cowTax + sheepTax;

        // Return the result as a formatted string
        return $"Total tax paid to the government: ${totalTax * 30:F2}";
    }

    public string TotalProfitPerDay()
    {
        // Retrieve data from the database
        var cows = _database.ReadCows();
        var sheeps = _database.ReadSheeps();

        double cowTax = cows.Sum(cow => cow.Milk * 0.2);

        // Calculate total tax for sheep (sum of wool * 0.2 * 30)
        double sheepTax = sheeps.Sum(sheep => sheep.Wool * 0.2);

        // Sum the taxes from both cows and sheep
        double totalTax = cowTax + sheepTax;

        double CowIncome = cows.Sum(cow => cow.Milk * 9.4);
        double SheepIncome = sheeps.Sum(sheep => sheep.Wool * 6.2);

        double Cost = cows.Sum(cow => cow.Cost) + sheeps.Sum(sheep => sheep.Cost);

        double profit = CowIncome + SheepIncome - Cost - totalTax;
        return $"Farm daily profit: ${profit:F2}";
    }

    public string CowSingleAvgProfit()
    {
        // Retrieve data from the database
        var cows = _database.ReadCows();

        double CowIncome = cows.Sum(cow => cow.Milk * 9.4);
        double Cost = cows.Sum(cow => cow.Cost);
        double cowTax = cows.Sum(cow => cow.Milk * 0.2);

        double profit = CowIncome - Cost - cowTax;

        int numberOfCows = cows.Count;
        double profitPerCow = numberOfCows > 0 ? profit / numberOfCows : 0;

        return $"On Average, a single cow makes daily profit: ${profitPerCow:F2}";

    }

    public string SheepSingleAvgProfit()
    {
        // Retrieve data from the database
        var sheeps = _database.ReadSheeps();

        double SheepIncome = sheeps.Sum(sheep => sheep.Wool * 6.2);
        double Cost = sheeps.Sum(sheep => sheep.Cost);
        double sheepTax = sheeps.Sum(sheep => sheep.Wool * 0.2);

        double profit = SheepIncome - Cost - sheepTax;

        int numberOfSheeps = sheeps.Count;
        double profitPerSheep = numberOfSheeps > 0 ? profit / numberOfSheeps : 0;

        return $"On Average, a single sheep makes daily profit: ${profitPerSheep:F2}";

    }

    public string SheepAvgProfit()
    {
        // Retrieve data from the database
        var sheeps = _database.ReadSheeps();

        double SheepIncome = sheeps.Sum(sheep => sheep.Wool * 6.2);
        double Cost = sheeps.Sum(sheep => sheep.Cost);
        double sheepTax = sheeps.Sum(sheep => sheep.Wool * 0.2);

        double profit = SheepIncome - Cost - sheepTax;


        return $"Current daily profit of all sheep is ${profit:F2}";

    }

    public string CowAvgProfit()
    {
        // Retrieve data from the database
        var cows = _database.ReadCows();

        double CowIncome = cows.Sum(cow => cow.Milk * 9.4);
        double Cost = cows.Sum(cow => cow.Cost);
        double cowTax = cows.Sum(cow => cow.Milk * 0.2);

        double profit = CowIncome - Cost - cowTax;

        return $"Current daily profit of all cow is ${profit:F2}";

    }

    public string AvgWeightLiveStock()
    {
        return $"Average weight of all livestocks: {Animals.Average(x => x.Weight):F2}";
    }

    //public string querybystoretype(string type)
    //{
    //    list<store> sts = stores.where(x => x.gettype().name.equals(type)).tolist();
    //    string results = $"{$"number of {type}:",-30}{sts.count}\n";
    //    results += $"{"average number of staff:",-30}{sts.average(x => x.numstaff)}";
    //    return results;
    //}

    public bool DeleteById(int id)
    {
        // find the store with the given id
        Animals? del_animal = Animals.FirstOrDefault(s => s.Id == id);
        if (del_animal != null)
        {
            // if the store is found and deleted from the database, remove it from the collection
            if (_database.DeleteItem(del_animal) > 0)
                if (Animals.Remove(del_animal))
                    return true;
        }
        return false;
    }


}
