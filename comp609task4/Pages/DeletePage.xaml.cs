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

        string deletedRecordInfo;
        if (vm.DeleteById(id, out deletedRecordInfo))
        {
            DisplayAlert("Success", deletedRecordInfo, "OK");
        }
        else
        {
            DisplayAlert("Error", $"Failed to delete record with ID: {id}", "OK");
        }

        LiveStockID.Text = string.Empty;
    }

}