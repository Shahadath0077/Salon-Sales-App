using CommunityToolkit.Maui.Views;
using SalonAccountSystem.ViewModels;
namespace SalonAccountSystem.Views;

public partial class ChangeDisplayNamePopup : Popup
{
    private SettingsPageViewModel _settingsPageViewModel;
    public ChangeDisplayNamePopup(SettingsPageViewModel settingsPageViewModel)
	{
		InitializeComponent();
        _settingsPageViewModel = settingsPageViewModel;
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        Button btnSender = (Button)sender;
        var name= btnSender.Text;
        //var display = displayName.Text;
        if (displayName.Text != null)
        {           
            this.Close();
        }
        else if (name==null)
        {
            this.Close();
        } 
    }

}