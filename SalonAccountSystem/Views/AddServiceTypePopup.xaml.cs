using CommunityToolkit.Maui.Views;
using SalonAccountSystem.ViewModels;
using System.Linq.Expressions;

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

    private void CloseButton_Clicked(object sender, EventArgs e)
    {
        Button btnSender = (Button)sender;
        var name = btnSender.Text;
        var service = serviceName.Text;

        if (service != null)
        {
            this.Close();
        }
        else if (name == null)
        {
            this.Close();
        }
    }
}
