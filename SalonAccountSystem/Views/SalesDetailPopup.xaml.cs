using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using SalonAccountSystem.Models;
using SalonAccountSystem.Services;
using SalonAccountSystem.ViewModels;
using System.Collections.ObjectModel;
namespace SalonAccountSystem.Views;

public partial class SalesDetailPopup : Popup
{
    private SalesDetailPageViewModel _salesDetailPageViewModel;
    public SalesDetailPopup(SalesDetailPageViewModel salesDetailPageViewModel)
    {
        InitializeComponent();
        _salesDetailPageViewModel = salesDetailPageViewModel;
        BindingContext = _salesDetailPageViewModel;
        _salesDetailPageViewModel.GetSalesListCommand.Execute(null);      
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        this.Close();
    } 
}