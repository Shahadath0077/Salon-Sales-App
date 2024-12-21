using CommunityToolkit.Maui.Views;
using SalonAccountSystem.ViewModels;

namespace SalonAccountSystem.Views;

public partial class AddServiceTypePopup : Popup
{
    private SettingsPageViewModel _settingsPageViewModel;
    public AddServiceTypePopup(SettingsPageViewModel settingsPageViewModel)
	{
		InitializeComponent();
        _settingsPageViewModel=settingsPageViewModel;
        this.BindingContext = _settingsPageViewModel;

    }
    private void Button_Clicked(object sender, EventArgs e)
    {
		this.Close();
    }
}