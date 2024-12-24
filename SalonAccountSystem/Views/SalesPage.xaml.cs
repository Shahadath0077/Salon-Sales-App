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
    protected override bool OnBackButtonPressed()
    {
        Dispatcher.Dispatch(async () =>
        {
            var answer = await DisplayAlert("Confirm Exit", "Are you sure you want to exit the app?", "Yes", "No");
            if (answer)
            {
                Application.Current.Quit();
            }
        });
        return true;
    }
}