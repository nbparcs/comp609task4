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

    //public string GetGeneralStats()
    //{
    //    return $"Total animals: {Animals.Count}";
    //}
    public string QueryByAnimalType(string type, string color)
    {
        List<Animals> selectedLivestock = Animals.Where(x => x.GetType().Name.Equals(type) && (color == "All" || (x.Colour?.Equals(color) ?? false))).ToList();

        double percentage = Animals.Count > 0 ? (double)selectedLivestock.Count / Animals.Count * 100 : 0;
        double profitOrLossPerDay = CalculateProfitOrLossPerDay(selectedLivestock, type);
        double averageWeight = selectedLivestock.Any(x => x.Weight != 0) ? selectedLivestock.Where(x => x.Weight != 0).Average(x => x.Weight) : 0;
        double governmentTaxPerDay = CalculateGovernmentTaxPerDay(selectedLivestock, type);
        double produceAmountPerDay = CalculateProduceAmountPerDay(selectedLivestock, type);

        string results = $"No. of Livestock ({type}s in {color} colour): {selectedLivestock.Count} \n";
        results += $"{"Percentage: "}{percentage:F2}%\n";
        results += $"{"Profit/Loss per day: $"}{profitOrLossPerDay:F2}\n";
        results += $"{"Average weight: "}{averageWeight:F2}kg\n";
        results += $"{"Government tax paid per day: $"}{governmentTaxPerDay:F2}\n";
        results += $"{"Produce amount per day: $"}{produceAmountPerDay:F2}\n";

        return results;
    }

    //public string CalculateProfitAnimal(string type)
    //{
    //    List<Animals> selectedLivestock = Animals.Where(x => x.GetType().Name.Equals(type)).ToList();

    //    double percentage = Animals.Count > 0 ? (double)selectedLivestock.Count / Animals.Count * 100 : 0;
    //    double profitOrLossPerDay = CalculateProfitOrLossPerDay(selectedLivestock, type);
    //    double averageWeight = selectedLivestock.Any(x => x.Weight != 0) ? selectedLivestock.Where(x => x.Weight != 0).Average(x => x.Weight) : 0;
    //    double governmentTaxPerDay = CalculateGovernmentTaxPerDay(selectedLivestock, type);
    //    double produceAmountPerDay = CalculateProduceAmountPerDay(selectedLivestock, type);

    //    string results = $"Number of Livestocks ({type}s in colour): {selectedLivestock.Count} \n";
    //    results += $"{"Percentage: "}{percentage:F2}%\n";
    //    results += $"{"Profit/Loss per day: $"}{profitOrLossPerDay:F2}\n";
    //    results += $"{"Average weight: "}{averageWeight:F2}kg\n";
    //    results += $"{"Government tax paid per day: $"}{governmentTaxPerDay:F2}\n";
    //    results += $"{"Produce amount per day: $"}{produceAmountPerDay:F2}\n";

    //    //string results = $"Number of Livestocks ({type}s in colour): {selectedLivestock.Count} \n";
    //    //results += $"{"Percentage: "}{percentage:F2}%\n";
    //    //results += $"{"Profit/Loss per day: $"}{profitOrLossPerDay:F2}\n";
    //    //results += $"{"Average weight: "}{averageWeight:F2}kg\n";
    //    //results += $"{"Government tax paid per day: $"}{governmentTaxPerDay:F2}\n";
    //    //results += $"{"Produce amount per day: $"}{produceAmountPerDay:F2}\n";

    //    return results;
    //}

    private double CalculateProfitOrLossPerDay(List<Animals> Animals, string type)
    {
        double incomePerDay = 0;
        double costPerDay = Animals.Sum(x => x.Cost);
        double governmentTaxPerDay = CalculateGovernmentTaxPerDay(Animals, type);

        if (type == "Cow")
        {
            incomePerDay += Animals.Sum(x => ((Cow)x).Milk * 9.4);
        }
        else if (type == "Sheep")
        {
            incomePerDay += Animals.Sum(x => ((Sheep)x).Wool * 6.2);
        }

        return incomePerDay - governmentTaxPerDay - costPerDay;
    }

    public string CalculateProfitEstimate(string type, string number)
    {
        if (type == "Cow")
        {
            double.TryParse(CowSingleAvgProfit().Replace("$", ""), out double singleCowProfit);
            double.TryParse(number.Replace("$", ""), out double numberOfInvestment);
            double InvestmentEst = singleCowProfit * numberOfInvestment;
            return $"${InvestmentEst:F2}";
        }
        else if (type == "Sheep")
        {
            double.TryParse(CowSingleAvgProfit().Replace("$", ""), out double singleSheepProfit);
            double.TryParse(number.Replace("$", ""), out double numberOfInvestment);
            double InvestmentEst = singleSheepProfit * numberOfInvestment;
            return $"${InvestmentEst:F2}";
        }
        else
        {
            return "Incorrect Values Entered";
        }


    }

    public string GetTotalTax()
    {
        var cows = _database.ReadCows();
        var sheeps = _database.ReadSheeps();
        double cowTax = cows.Sum(cow => cow.Weight * 0.2); //Calculate total tax for cows (sum of milk * 0.2 * 30)
        double sheepTax = sheeps.Sum(sheep => sheep.Weight * 0.2); // Calculate total tax for sheep (sum of wool * 0.2 * 30)
        double totalTax = cowTax + sheepTax; // Sum the taxes from both cows and sheep
        return $"${totalTax * 30:F2}";  // Return the result as a formatted string
    }

    public string TotalProfitPerDay()
    {
        var cows = _database.ReadCows(); //Get data from the database
        var sheeps = _database.ReadSheeps();
        double cowTax = cows.Sum(cow => cow.Weight * 0.2);
        double sheepTax = sheeps.Sum(sheep => sheep.Weight * 0.2); // Calculates the total tax for sheep (sum of wool * 0.2 * 30)
        double totalTax = cowTax + sheepTax; // Get the sum the taxes from both cows and sheep
        double CowIncome = cows.Sum(cow => cow.Milk * 9.4);
        double SheepIncome = sheeps.Sum(sheep => sheep.Wool * 6.2);
        double Cost = cows.Sum(cow => cow.Cost) + sheeps.Sum(sheep => sheep.Cost);
        double profit = CowIncome + SheepIncome - Cost - totalTax;
        return $"${profit:F2}";
    }

    public string CowSingleAvgProfit()
    {
        var cows = _database.ReadCows(); //Get data from the database
        double CowIncome = cows.Sum(cow => cow.Milk * 9.4);
        double Cost = cows.Sum(cow => cow.Cost);
        double cowTax = cows.Sum(cow => cow.Milk * 0.2);
        double profit = CowIncome - Cost - cowTax;
        int numberOfCows = cows.Count;
        double profitPerCow = numberOfCows > 0 ? profit / numberOfCows : 0;
        return $"${profitPerCow:F2}";

    }
    public string SheepSingleAvgProfit()
    {
        var sheeps = _database.ReadSheeps(); //Get data from the database
        double SheepIncome = sheeps.Sum(sheep => sheep.Wool * 6.2);
        double Cost = sheeps.Sum(sheep => sheep.Cost);
        double sheepTax = sheeps.Sum(sheep => sheep.Wool * 0.2);
        double profit = SheepIncome - Cost - sheepTax;
        int numberOfSheeps = sheeps.Count;
        double profitPerSheep = numberOfSheeps > 0 ? profit / numberOfSheeps : 0;
        return $"${profitPerSheep:F2}";
    }

    public string SheepAvgProfit()
    {
        var sheeps = _database.ReadSheeps(); //Get data from the database
        double SheepIncome = sheeps.Sum(sheep => sheep.Wool * 6.2);
        double Cost = sheeps.Sum(sheep => sheep.Cost);
        double sheepTax = sheeps.Sum(sheep => sheep.Weight * 0.2);
        double profit = SheepIncome - Cost - sheepTax;
        return $"${profit:F2}";
    }

    public string CowAvgProfit()
    {
        var cows = _database.ReadCows(); //Get data from the database
        double CowIncome = cows.Sum(cow => cow.Milk * 9.4);
        double Cost = cows.Sum(cow => cow.Cost);
        double cowTax = cows.Sum(cow => cow.Weight * 0.2);
        double profit = CowIncome - Cost - cowTax;
        return $"${profit:F2}";
    }

    public string AvgWeightLiveStock()
    {
        var livestockWithWeight = Animals.Where(x => x.Weight != 0).ToList();

        if (livestockWithWeight.Count > 0)
        {
            double averageWeight = livestockWithWeight.Average(x => x.Weight); //Getting the average weight
            return $"{averageWeight:F2}kg";
        }
        else
        {
            return "No livestock with non-zero weight found.";
        }
    }

    public static void DisplayCow(List<Cow> animaltype)
    {
        foreach (var anml in animaltype)
        {
            Console.WriteLine($"{"Cow",-12} {anml.Id,-12}{anml.Cost,-20}{anml.Weight,-12}{anml.Colour,-12}{anml.Type,-12})");
        }
    }
    public static void DisplaySheep(List<Sheep> animaltype)
    {
        foreach (var anml in animaltype)
        {
            Console.WriteLine($"{"Sheep",-13}{anml.Id,-12}{anml.Cost,-20}{anml.Weight,-12}{anml.Colour,-12}");
        }
    }
    private double CalculateGovernmentTaxPerDay(List<Animals> livestock, string type)
    {
        double taxPerDay = 0;
        if (type == "Cow")
        {
            taxPerDay = livestock.Sum(x => ((Cow)x).Weight * 0.2);
        }
        else if (type == "Sheep")
        {
            taxPerDay = livestock.Sum(x => ((Sheep)x).Weight * 0.2);
        }

        return taxPerDay;
    }

    private double CalculateProduceAmountPerDay(List<Animals> livestock, string type)
    {
        double produceAmountPerDay = 0;
        if (type == "Cow")
        {
            produceAmountPerDay = livestock.Sum(x => ((Cow)x).Milk);
        }
        else if (type == "Sheep")
        {
            produceAmountPerDay = livestock.Sum(x => ((Sheep)x).Wool);
        }
        return produceAmountPerDay;
    }
    public bool DeleteById(int id, out string deletedRecordInfo)
    {
        deletedRecordInfo = string.Empty;

        // Find the animal in the collection
        Animals del_animal = Animals.FirstOrDefault(s => s.Id == id);
        if (del_animal != null)
        {
            // Attempt to delete the item from the database
            if (_database.DeleteItem(del_animal) > 0)
            {
                // Remove the item from the collection
                if (Animals.Remove(del_animal))
                {
                    // Construct the string with all the record details
                    deletedRecordInfo = $"Deleted Record:\n" +
                                        $"Type: {del_animal.Type}\n" +
                                        $"ID: {del_animal.Id}\n" +
                                        $"Cost: {del_animal.Cost}\n" +
                                        $"Weight: {del_animal.Weight}\n" +
                                        $"Colour: {del_animal.Colour}\n";

                    // Append additional fields based on animal type
                    if (del_animal is Cow cow)
                    {
                        deletedRecordInfo += $"Milk: {cow.Milk}\n";
                    }
                    else if (del_animal is Sheep sheep)
                    {
                        deletedRecordInfo += $"Wool: {sheep.Wool}\n";
                    }

                    // Return true indicating successful deletion
                    return true;
                }
            }
        }

        // Return false if deletion fails or if animal with given ID not found
        return false;
    }

    public string CalculateProfitEstimate(int numberOfNewLivestock, string type)
    {
        double currentAvgProfitPerAnimal;

        if (type == "Cow") //Current average profit per animal based on the type
        {
            var currentAvgProfit = CowSingleAvgProfit();
            currentAvgProfitPerAnimal = double.Parse(currentAvgProfit.TrimStart('$'));
        }
        else if (type == "Sheep")
        {
            var currentAvgProfit = SheepSingleAvgProfit();
            currentAvgProfitPerAnimal = double.Parse(currentAvgProfit.TrimStart('$'));
        }
        else
        {
            return "Invalid livestock type. Please specify 'Cow' or 'Sheep'.";
        }

        double estimatedAdditionalProfit = currentAvgProfitPerAnimal * numberOfNewLivestock; //Calculate the estimated additional daily profit
        return $"${estimatedAdditionalProfit:F2}";
    }


    public bool InsertAnimal(string animalType, string color, string costText, string weightText, string milkText, string woolText, out string result)
    {
        result = string.Empty;
        double cost, weight, milk = 0, wool = 0;

        // Validate common fields
        if (string.IsNullOrWhiteSpace(animalType) || string.IsNullOrWhiteSpace(color) ||
            !double.TryParse(costText, out cost) || !double.TryParse(weightText, out weight))
        {
            result = "Invalid input: Please ensure all required fields are filled correctly.";
            return false;
        }

        // Additional validation based on animal type
        if (animalType == "Cow" && !double.TryParse(milkText, out milk))
        {
            result = "Invalid input: Please enter a valid number for milk produced.";
            return false;
        }
        else if (animalType == "Sheep" && !double.TryParse(woolText, out wool))
        {
            result = "Invalid input: Please enter a valid number for wool produced.";
            return false;
        }

        // Create the new animal object based on the selected type
        Animals newAnimal;
        if (animalType == "Cow")
        {
            newAnimal = new Cow
            {
                Type = animalType,
                Colour = color,
                Cost = cost,
                Weight = weight,
                Milk = milk
            };
        }
        else if (animalType == "Sheep")
        {
            newAnimal = new Sheep
            {
                Type = animalType,
                Colour = color,
                Cost = cost,
                Weight = weight,
                Wool = wool
            };
        }
        else
        {
            result = "Unknown animal type.";
            return false;
        }

        // Insert the new animal into the database
        var inserted = _database.InsertItem(newAnimal);
        if (inserted > 0)
        {
            Animals.Add(newAnimal); // Add the new animal to the collection
            result = $"Record added: {newAnimal}";
            return true;
        }
        else
        {
            result = "Failed to insert record into the database.";
            return false;
        }
    }

    public bool UpdateAnimal(string animalId, string animalType, string color, string costText, string weightText, string milkText, string woolText, out string result)
    {
        result = string.Empty;
        double cost, weight, milk = 0, wool = 0;

        // Validate common fields including animalId
        if (string.IsNullOrWhiteSpace(animalId) || string.IsNullOrWhiteSpace(animalType) || string.IsNullOrWhiteSpace(color) ||
            !double.TryParse(costText, out cost) || !double.TryParse(weightText, out weight))
        {
            result = "Invalid input: Please ensure all required fields are filled correctly.";
            return false;
        }

        // Additional validation based on animal type
        if (animalType == "Cow" && !double.TryParse(milkText, out milk))
        {
            result = "Invalid input: Please enter a valid number for milk produced.";
            return false;
        }
        else if (animalType == "Sheep" && !double.TryParse(woolText, out wool))
        {
            result = "Invalid input: Please enter a valid number for wool produced.";
            return false;
        }

        // Find the animal in the collection
        var existingAnimal = Animals.FirstOrDefault(a => a.Id.ToString() == animalId);
        if (existingAnimal == null)
        {
            result = $"Animal with ID {animalId} not found.";
            return false;
        }


        // Update the animal properties
        existingAnimal.Type = animalType;
        existingAnimal.Colour = color;
        existingAnimal.Cost = cost;
        existingAnimal.Weight = weight;

        if (animalType == "Cow")
        {
            if (existingAnimal is Cow cow)
            {
                cow.Milk = milk;
            }
            else
            {
                result = "Animal type mismatch.";
                return false;
            }
        }
        else if (animalType == "Sheep")
        {
            if (existingAnimal is Sheep sheep)
            {
                sheep.Wool = wool;
            }
            else
            {
                result = "Animal type mismatch.";
                return false;
            }
        }
        else
        {
            result = "Unknown animal type.";
            return false;
        }

        // Update the animal in the database
        var updated = _database.UpdateItem(existingAnimal);
        if (updated > 0)
        {
            result = $"Record updated: {existingAnimal}";
            return true;
        }
        else
        {
            result = "Failed to update record in the database.";
            return false;
        }
    }

    public void RefreshData()
    {
        Animals = new();
        _database.ReadItems().ForEach(x => Animals.Add(x));
    }



}
