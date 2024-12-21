using SalonAccountSystem.ViewModels;

namespace SalonAccountSystem.Views;

public partial class AddUpdateSalesPage : ContentPage
{
    private AddUpdateSalesPageViewModel _addUpdateSalesPageViewModel;
    public AddUpdateSalesPage(AddUpdateSalesPageViewModel addUpdateSalesPageViewModel)
    {
        InitializeComponent();
        _addUpdateSalesPageViewModel = addUpdateSalesPageViewModel;
        this.BindingContext = _addUpdateSalesPageViewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _addUpdateSalesPageViewModel.GetServiceTypeListCommand.Execute(null);
    }

}