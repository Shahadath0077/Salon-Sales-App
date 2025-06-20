using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using SalonAccountSystem.Models;
using SalonAccountSystem.Services;
using SalonAccountSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.ViewModels
{
    [QueryProperty(nameof(SalesDetail), "SalesDetail")]
    [QueryProperty(nameof(MonthlySalesDetail), "MonthlySalesDetail")]
    public partial class AddUpdateSalesPageViewModel : ObservableObject
    {
        private readonly IDailySalesService _dailySalesService;
        private readonly IAddServiceTypeService _addServiceTypeService;
        public ObservableCollection<DailySalesModel> DailySalesList { get; set; } = new ObservableCollection<DailySalesModel>();
        public ObservableCollection<DailySalesGroupModel> MonthlyGroupSalesList { get; set; } = new ObservableCollection<DailySalesGroupModel>();
      
        public ObservableCollection<DailySalesModel> MonthlySalesList { get; set; } = new ObservableCollection<DailySalesModel>();

        public ObservableCollection<AddServiceTypeModel> ServiceTypeList { get; set; } = new ObservableCollection<AddServiceTypeModel>();

        public ObservableCollection<DailySalesDetailGroupModel> DailySalesGroupList { get; set; } = new ObservableCollection<DailySalesDetailGroupModel>();

        [ObservableProperty]
        private DailySalesModel _monthlySalesDetail = new DailySalesModel();

        [ObservableProperty]
        private DailySalesModel _salesDetail = new DailySalesModel();

        [ObservableProperty]
        public AddServiceTypeModel selelctedServiceType;

        [ObservableProperty]
        private SalesReportModel _salesReportDetail = new SalesReportModel();
 
        private DailySalesModel _dailySalesModel { get; set; }
       
        private SettingsPageViewModel _settingsPageViewModel;
 
        public AddUpdateSalesPageViewModel(IDailySalesService dailySalesService, IAddServiceTypeService addServiceTypeService, SettingsPageViewModel settingsPageViewModel)
        {
            _dailySalesService = dailySalesService;
            _addServiceTypeService = addServiceTypeService;
            _dailySalesModel = new DailySalesModel();
            _settingsPageViewModel = settingsPageViewModel;            
        }

        [RelayCommand]
        public async Task AddUpdateSales()
        {
            try
            {
                int response = -1;
                string message = "";
                // Update the sales
                if (SalesDetail.SalesId > 0)
                { 
                    response = await _dailySalesService.UpdateSales(SalesDetail);
                    if (response > 0)
                    {
                        SelelctedServiceType = new AddServiceTypeModel();
                        SalesDetail = new DailySalesModel();
                        message = "Sales updated successfully!";
                        await Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Short).Show(); 
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Update Failed!", "Something went wrong...please try again", "OK");
                    }
                }
                else
                {
                    // add new sales                                       
                    if (SalesDetail.SalesType!="Select a service" && !string.IsNullOrWhiteSpace(SalesDetail.SalesDate.ToString()) && SalesDetail.Amount!= null)
                    {
                    response = await _dailySalesService.AddSales(new DailySalesModel
                    {
                        SalesDate = SalesDetail.SalesDate,
                        Amount = SalesDetail.Amount,                           
                        SalesType = SalesDetail.SalesType,

                    });

                        if (response > 0)
                        {
                            await _settingsPageViewModel.ShowSpinner();
                            SelelctedServiceType = new AddServiceTypeModel();
                            SalesDetail = new DailySalesModel();
                            message = "Sales saved successfully!";

                           
                        await Toast.Make(message,    CommunityToolkit.Maui.Core.ToastDuration.Short).Show();



                        }
                    }
                    else
                    {
                        if (SalesDetail.SalesType == "Select a service")
                        {
                             message = "Please select a service!";
                        }
                        else if (SalesDetail.Amount == null)
                        {
                             message = "Please enter amount!";
                        }
                        await Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
                    }
                }
            }
            catch (Exception ex)
            {  
                string message = "Please select a service!";
                await Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
                Console.WriteLine(ex);
            }
        }

        [RelayCommand]
        public async Task GetServiceTypeList()
        {
            try
            {
                ServiceTypeList.Clear();
                var servieTypeList = await _addServiceTypeService.GetServiceTypeList();
                if (servieTypeList?.Count > 0)
                {
                    foreach (var serviceType in servieTypeList)
                    {
                        ServiceTypeList.Add(serviceType);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        public void ServiceList()
        {
            ServiceListPopup popup = new ServiceListPopup(this);
            Application.Current?.MainPage?.ShowPopup(popup);
        }


       
        

    }
}
