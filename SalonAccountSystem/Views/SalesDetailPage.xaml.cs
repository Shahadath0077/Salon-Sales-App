using SalonAccountSystem.ViewModels;


namespace SalonAccountSystem.Views;

public partial class SalesDetailPage : ContentPage
{
    private SalesDetailPageViewModel _salesDetailPageViewModel;
    public SalesDetailPage(SalesDetailPageViewModel salesDetailPageViewModel)
	{
		InitializeComponent();
        _salesDetailPageViewModel=salesDetailPageViewModel;
        this.BindingContext = _salesDetailPageViewModel;
        //_salesDetailPageViewModel.GetSalesListCommand.Execute(null);
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _salesDetailPageViewModel.GetSalesListCommand.Execute(null);
    }
}