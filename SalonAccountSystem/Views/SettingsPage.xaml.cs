using SalonAccountSystem.ViewModels;

namespace SalonAccountSystem.Views;

public partial class SettingsPage : ContentPage
{
    private SettingsPageViewModel _settingsPageViewModel;	
    public SettingsPage(SettingsPageViewModel settingsPageViewModel)
	{
		InitializeComponent();
        _settingsPageViewModel= settingsPageViewModel;
        this.BindingContext = _settingsPageViewModel;      
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _settingsPageViewModel.GetServiceTypeListCommand.Execute(null);
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