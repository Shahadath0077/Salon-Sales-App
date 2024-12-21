using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class AddUpdateSalesPageViewModel : ObservableObject
    {
        public ObservableCollection<DailySalesModel> MonthlySalesList { get; set; } = new ObservableCollection<DailySalesModel>();

         public ObservableCollection<AddServiceTypeModel> ServiceTypeList { get; set; } = new ObservableCollection<AddServiceTypeModel>();


        [ObservableProperty]
        private DailySalesModel _salesDetail = new DailySalesModel();

        [ObservableProperty]
        public AddServiceTypeModel selelctedServiceType;

        private readonly IDailySalesService _dailySalesService;
        private DailySalesModel _dailySalesModel { get; set; }

        private readonly IAddServiceTypeService _addServiceTypeService;
        public AddUpdateSalesPageViewModel(IDailySalesService dailySalesService, IAddServiceTypeService addServiceTypeService)
        {
            _dailySalesService = dailySalesService;
            _addServiceTypeService= addServiceTypeService;
            _dailySalesModel = new DailySalesModel();
            
        }

        [RelayCommand]
        public async Task AddUpdateSales()
        { 
            try
            {              
                int response = -1;
                if (SalesDetail.SalesId > 0)
                {                    
                    SalesDetail.SalesType = SelelctedServiceType.ServiceType;
                    response = await _dailySalesService.UpdateSales(SalesDetail);
                    if (response > 0)
                    {
                        SelelctedServiceType = new AddServiceTypeModel();
                        SalesDetail = new DailySalesModel();
                        await Shell.Current.DisplayAlert("Message", "Sales updated successfully!", "OK");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while updating sales", "OK");
                    }                                        
                }
                else
                {                    
                    if (!string.IsNullOrWhiteSpace(SelelctedServiceType.ServiceType) && !string.IsNullOrWhiteSpace(SalesDetail.SalesDate.ToString()) && (SalesDetail.Amount != null))
                        {
                        response = await _dailySalesService.AddSales(new Models.DailySalesModel
                        {
                            SalesDate = SalesDetail.SalesDate,
                            Amount = SalesDetail.Amount,
                            SalesType = SelelctedServiceType.ServiceType,
                        });

                        if (response > 0)
                        {
                            SelelctedServiceType = new AddServiceTypeModel();
                            SalesDetail = new DailySalesModel();
                            await Shell.Current.DisplayAlert("Message", "Sales saved successfully!", "OK");
                        }
                        else
                        {
                            await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while adding expenses", "OK");
                        }
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Message", "Enter all the required fields!", "OK");
                    }
                }               
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Message", "Please select a service", "OK");                
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
                else
                {

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
