using CommunityToolkit.Maui.Views;
using SalonAccountSystem.Models;
using SalonAccountSystem.ViewModels;
namespace SalonAccountSystem.Views;

public partial class ServiceListPopup : Popup
{
    private AddUpdateSalesPageViewModel _addUpdateSalesPageViewModel;
    
    public ServiceListPopup(AddUpdateSalesPageViewModel addUpdateSalesPageViewModel)
    {
        InitializeComponent();
        _addUpdateSalesPageViewModel = addUpdateSalesPageViewModel;

        this.BindingContext = _addUpdateSalesPageViewModel;
        _addUpdateSalesPageViewModel.GetServiceTypeListCommand.Execute(null);  
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Button btnSender = (Button)sender;
        var name = btnSender.Text;

        _addUpdateSalesPageViewModel.SalesDetail.SalesType = name;
 
        this.Close();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        this.Close();
    }
}