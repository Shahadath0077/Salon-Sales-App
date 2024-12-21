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
       // _settingsPageViewModel.GetServiceTypeListCommand.Execute(null);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _settingsPageViewModel.GetServiceTypeListCommand.Execute(null);
    }

}