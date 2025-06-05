using CommunityToolkit.Maui.Views;
using SalonAccountSystem.ViewModels;

namespace SalonAccountSystem.Views;

public partial class AboutPopup : Popup
{
	AppShellViewModel _appShellViewModel;
    public AboutPopup(AppShellViewModel appShellViewModel)
	{
		InitializeComponent();
        _appShellViewModel=appShellViewModel;
        this.BindingContext = _appShellViewModel;

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        //Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
        this.Close();   

    }
}