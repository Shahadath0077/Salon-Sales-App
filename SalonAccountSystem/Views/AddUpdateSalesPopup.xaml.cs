using CommunityToolkit.Maui.Views;
using SalonAccountSystem.Models;
using SalonAccountSystem.ViewModels;

namespace SalonAccountSystem.Views;

public partial class AddUpdateSalesPopup : Popup
{
    private AddUpdateSalesPageViewModel _addUpdateSalesPageViewModel;
    private DailySalesModel? _dailySalesModel;
   
    public AddUpdateSalesPopup(AddUpdateSalesPageViewModel addUpdateSalesPageViewModel, DailySalesModel dailySalesModel)
	{
		InitializeComponent();

        _addUpdateSalesPageViewModel = addUpdateSalesPageViewModel;
        _dailySalesModel = dailySalesModel;

        if (_dailySalesModel!=null)
        {
            _addUpdateSalesPageViewModel.SalesDetail.SalesId = _dailySalesModel.SalesId;
            _addUpdateSalesPageViewModel.SalesDetail.Amount = _dailySalesModel.Amount;
            _addUpdateSalesPageViewModel.SalesDetail.SalesDate = _dailySalesModel.SalesDate;
            _addUpdateSalesPageViewModel.SalesDetail.SalesType = _dailySalesModel.SalesType;
        }
        this.BindingContext = _addUpdateSalesPageViewModel;
        _addUpdateSalesPageViewModel.GetServiceTypeListCommand.Execute(null);

    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        Button btnSender = (Button)sender;
        var name = btnSender.Text;

        var tt = btnServiceType.Text;

       
        if (amountEntry.Text != null && tt != "Select a service")
        {
            //_addUpdateSalesPageViewModel.SalesDetail.SalesType = "Select a service";
            this.Close();
        }      
        else if (name == "CANCEL")
        {
            //_addUpdateSalesPageViewModel.SalesDetail.SalesType = "Select a service";
            _addUpdateSalesPageViewModel.SalesDetail.Amount = null;
            this.Close();
        }
    }
}