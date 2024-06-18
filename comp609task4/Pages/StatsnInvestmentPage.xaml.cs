namespace comp609task4.Pages;

public partial class StatsnInvestmentPage : ContentPage
{
    MainViewModel vm;
    public StatsnInvestmentPage(MainViewModel vm)
    {
        InitializeComponent();
        this.vm = vm;
        GovtTaxLabel.Text = vm.GetTotalTax();
        DailyProfitsLabel.Text = vm.TotalProfitPerDay();
        AvgWeightLabel.Text = vm.AvgWeightLiveStock();
        //SingleCowAvgProfitLabel.Text = vm.CowSingleAvgProfit();
        //SingleSheepAvgProfitLabel.Text = vm.SheepSingleAvgProfit();
        AllSheepsProfitLabel.Text = vm.SheepAvgProfit();
        AllCowsProfitLabel.Text = vm.CowAvgProfit();
    }



    private void CalculateProfitEstimate_Clicked(object sender, EventArgs e)
    {
        if (LivestockTypePicker.SelectedItem != null)
        {
            string type = LivestockTypePicker.SelectedItem.ToString();

            ProfitEstimate.Text = vm.CalculateProfitEstimate(type, AnimalInvestment.Text);
        }
        else
        {
            DisplayAlert("Error", "Please select an animal type or color.", "OK");
        }
    }
}