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

    }

    private async void InsertRecord_Btn(object sender, EventArgs e)
    {
        string animalType = AnimalPicker.SelectedItem as string;
        string color = LiveStockColour.Text;
        string costText = LiveStockCost.Text;
        string weightText = LiveStockWeight.Text;
        string milkText = LiveStockMilk.Text;
        string woolText = LiveStockWool.Text;

        bool isSuccess = vm.InsertAnimal(animalType, color, costText, weightText, milkText, woolText, out string result);

        await DisplayAlert(isSuccess ? "Success" : "Error", result, "OK");

        if (isSuccess)
        {
            // Clear input fields
            LiveStockColour.Text = string.Empty;
            LiveStockCost.Text = string.Empty;
            LiveStockWeight.Text = string.Empty;
            LiveStockMilk.Text = string.Empty;
            LiveStockWool.Text = string.Empty;
            AnimalPicker.SelectedIndex = -1;
        }
    }
}