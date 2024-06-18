namespace comp609task4.Pages;

public partial class DataPage : ContentPage
{

    private MainViewModel _vm;
    public DataPage(MainViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.RefreshData(); // Automatically refresh data when page appears
    }
    private void RefreshData_Clicked(object sender, EventArgs e)
    {
        _vm.RefreshData(); // Call a method in MainViewModel to refresh data
    }
}