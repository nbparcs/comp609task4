namespace comp609task4.Pages;

public partial class UpdatePage : ContentPage

{
    MainViewModel vm;
    public ObservableCollection<string> UpdateRecord { get; set; }
    public UpdatePage(MainViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
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
        ColourPicker.SelectedIndexChanged += OnColorSelectionChange;
    }

    private void OnColorSelectionChange(object sender, EventArgs e)
    {
        var animalType = AnimalPicker.SelectedItem as string;
        var color = ColourPicker.SelectedItem as string;

        if (!string.IsNullOrEmpty(animalType) && !string.IsNullOrEmpty(color))
        {
            vm.FilterIds(animalType, color);
            IdPicker.ItemsSource = vm.FilteredIds;
        }
    }

    private async void UpdateRecord_Btn(object sender, EventArgs e)
    {
        if (IdPicker.SelectedItem == null || AnimalPicker.SelectedItem == null || ColourPicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "Please select an ID, animal type, and color.", "OK");
            return;
        }

        if (int.TryParse(IdPicker.SelectedItem.ToString(), out int id))
        {
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

            bool isSuccess = vm.UpdateAnimal(id, animalType, color, costText, weightText, milkText, woolText, out string result);

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
                IdPicker.SelectedIndex = -1;
            }
        }
        else
        {
            await DisplayAlert("Error", "Invalid ID selected.", "OK");
        }
    }


}