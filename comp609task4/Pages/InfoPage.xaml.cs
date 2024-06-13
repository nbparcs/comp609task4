using System.ComponentModel;

namespace comp609task4.Pages;

public partial class InfoPage : ContentPage
{
    MainViewModel vm;
    public InfoPage(MainViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        //GenStatsLabel.Text = vm.GetGeneralStats();
        Allanimals = vm.Animals.ToList();
    }

    public List<Animals> Allanimals { get; set; }

    private void Searchbtn_Clicked(object sender, EventArgs e)
    {
        if (LivestockTypePicker.SelectedItem != null)
        {
            string type = LivestockTypePicker.SelectedItem.ToString();
            string color = LiveStockColourPicker.SelectedItem?.ToString() ?? "All";

            ResultLabel.Text = vm.QueryByAnimalType(type, color);
        }
        else
        {
            DisplayAlert("Error", "Please select an animal type or color.", "OK");
        } 
    }

    private void ResetButton_Clicked(object sender, EventArgs e)
    {
        LivestockTypePicker.SelectedItem = string.Empty;
        LiveStockColourPicker.SelectedItem = string.Empty;

        ResultLabel.Text = string.Empty;
    }
}