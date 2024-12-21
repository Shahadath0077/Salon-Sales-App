using SalonAccountSystem.ViewModels;

namespace SalonAccountSystem.Views;

public partial class SalesPage : ContentPage
{
    private SalesPageViewModel _salesPageViewModel;
    public SalesPage(SalesPageViewModel salesPageViewModel)
	{
		InitializeComponent();
        _salesPageViewModel = salesPageViewModel;
        this.BindingContext = _salesPageViewModel;   
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _salesPageViewModel.GetMonthlySalesListCommand.Execute(null);          
    }
}