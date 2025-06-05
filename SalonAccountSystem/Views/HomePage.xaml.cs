
using SalonAccountSystem.Models;
using SalonAccountSystem.ViewModels;


namespace SalonAccountSystem.Views;

public partial class HomePage : ContentPage
{
    private HomePageViewModel _homePageViewModel;
    public HomePage(HomePageViewModel homePageViewModel)
    {
        InitializeComponent();

        _homePageViewModel = homePageViewModel;
        this.BindingContext = _homePageViewModel;

        //if (App.loginModel != null)
        //{
        //    lblUserFullName.Text = App.loginModel.FullName;
        //}
        DataRefreshTimer();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _homePageViewModel.GetSalesListCommand.Execute(null);
        _homePageViewModel.GetNameCommand.Execute(null);
        
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


    private  void DataRefreshTimer()
    {     
        Dispatcher.StartTimer(TimeSpan.FromSeconds(15), () =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                _homePageViewModel.GetSalesListCommand.Execute(null);
                 
            });
            return true;
        });
    }

    

}