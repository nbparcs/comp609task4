namespace comp609task4.Pages;

public partial class DataPage : ContentPage
{
    public DataPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}