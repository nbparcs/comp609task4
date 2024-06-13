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
        SingleCowAvgProfitLabel.Text = vm.CowSingleAvgProfit();
        SingleSheepAvgProfitLabel.Text = vm.SheepSingleAvgProfit();
        AllSheepsProfitLabel.Text = vm.SheepAvgProfit();
        AllCowsProfitLabel.Text = vm.CowAvgProfit();


    }


}