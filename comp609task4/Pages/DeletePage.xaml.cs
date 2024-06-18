namespace comp609task4.Pages;

public partial class DeletePage : ContentPage
{
    MainViewModel vm;
    public DeletePage(MainViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;

    }

    private void LiveStockDelete_Btn(object sender, EventArgs e)
    {
        string input = LiveStockID.Text;
        if (string.IsNullOrWhiteSpace(input))
        {
            DisplayAlert("Error", "Invalid item name", "OK");
            return;
        }

        if (!int.TryParse(LiveStockID.Text, out int id))
        {
            DisplayAlert("Error", "Invalid quantity", "OK");
            return;
        }

        if (vm.DeleteById(id))
            DisplayAlert("Success", $"Record deleted: {id}", "OK");

        LiveStockID.Text = string.Empty;
    }
}