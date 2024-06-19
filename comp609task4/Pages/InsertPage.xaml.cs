namespace comp609task4.Pages;

public partial class InsertPage : ContentPage
{
    MainViewModel vm;
    public ObservableCollection<string> InsertRecord { get; set; }
    public InsertPage(MainViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        AnimalPicker.ItemsSource = new string[] { "Cow", "Sheep" };
    }

    private void OnAnimalTypeSelectionChange(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        if (selectedIndex == -1) return;

        string type = (string)picker.ItemsSource[selectedIndex];

        if (type == "Cow")
        {
            ColourPicker.ItemsSource = new string[] { "Red", "Black" };
        }
        else if (type == "Sheep")
        {
            ColourPicker.ItemsSource = new string[] { "White", "Black" };
        }

        // Optional: Set the default selected index for ColourPicker
        ColourPicker.SelectedIndex = 0;
    }

    private async void InsertRecord_Btn(object sender, EventArgs e)
    {

        if (AnimalPicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "Please select an ID, animal type, and color.", "OK");
            return;
        }
        string animalType = AnimalPicker.SelectedItem as string;
        string color = ColourPicker.SelectedItem as string;
        string costText = LiveStockCost.Text;
        string weightText = LiveStockWeight.Text;
        string milkText = LiveStockMilk.Text;
        string woolText = LiveStockWool.Text;

        // Validate inputs based on animal type
        if (animalType == "Cow" && !string.IsNullOrWhiteSpace(woolText))
        {
            await DisplayAlert("Error", "Cows cannot have wool. Please leave the wool field empty.", "OK");
            return;
        }

        if (animalType == "Sheep" && !string.IsNullOrWhiteSpace(milkText))
        {
            await DisplayAlert("Error", "Sheep cannot have milk. Please leave the milk field empty.", "OK");
            return;
        }

        bool isSuccess = vm.InsertAnimal(animalType, color, costText, weightText, milkText, woolText, out string result);

        await DisplayAlert(isSuccess ? "Success" : "Error", result, "OK");

        if (isSuccess)
        {
            // Clear input fields
            ColourPicker.SelectedIndex = -1;
            LiveStockCost.Text = string.Empty;
            LiveStockWeight.Text = string.Empty;
            LiveStockMilk.Text = string.Empty;
            LiveStockWool.Text = string.Empty;
            AnimalPicker.SelectedIndex = -1;
        }
    }
}