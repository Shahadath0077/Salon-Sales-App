namespace SalonAccountSystem.Views;
using CommunityToolkit.Maui.Views;
using SalonAccountSystem.ViewModels;

public partial class ChangePasswordPopup : Popup
{
    private SettingsPageViewModel _settingsPageViewModel;
	public ChangePasswordPopup(SettingsPageViewModel settingsPageViewModel)
	{
		InitializeComponent();
        _settingsPageViewModel = settingsPageViewModel;

    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        this.Close();
    }
}