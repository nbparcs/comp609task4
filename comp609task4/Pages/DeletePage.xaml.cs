namespace comp609task4.Pages
{
    public partial class DeletePage : ContentPage
    {
        MainViewModel vm;

        public DeletePage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            this.vm = vm;

            // Populate IdPicker with available IDs
            foreach (var animal in vm.Animals)
            {

                IdPicker.Items.Add(animal.Id.ToString());
            }
        }

        private void LiveStockDelete_Btn(object sender, EventArgs e)
        {
            if (IdPicker.SelectedIndex == -1)
            {
                DisplayAlert("Error", "Please select an ID", "OK");
                return;
            }

            // Get selected ID
            int id = Convert.ToInt32(IdPicker.SelectedItem);

            string deletedRecordInfo;
            if (vm.DeleteById(id, out deletedRecordInfo))
            {
                DisplayAlert("Success", deletedRecordInfo, "OK");
            }
            else
            {
                DisplayAlert("Error", $"Failed to delete record with ID: {id}", "OK");
            }

            // Clear selected ID after deletion
            IdPicker.SelectedIndex = -1;

            IdPicker.Items.Clear();


            foreach (var animal in vm.Animals)
            {
                IdPicker.Items.Add(animal.Id.ToString());

            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.RefreshData(); // Automatically refresh data when page appears
        }

    }
}
