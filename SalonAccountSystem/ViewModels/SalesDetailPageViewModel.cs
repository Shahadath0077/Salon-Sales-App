using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
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

    [QueryProperty(nameof(SalesReportDetail), "SalesReportDetail")]

    [QueryProperty(nameof(MonthlySalesDetail), "MonthlySalesDetail")]
    public partial class SalesDetailPageViewModel:ObservableObject
    {  
        [ObservableProperty]
        private DailySalesModel _monthlySalesDetail = new DailySalesModel();

        public ObservableCollection<DailySalesModel> DailySalesList { get; set; } = new ObservableCollection<DailySalesModel>();
        public ObservableCollection<DailySalesDetailGroupModel> DailySalesGroupList { get; set; } = new ObservableCollection<DailySalesDetailGroupModel>();

        [ObservableProperty]
        private DailySalesModel _salesDetail = new DailySalesModel();

        [ObservableProperty]
        private SalesReportModel _salesReportDetail = new SalesReportModel();
       
        private readonly IDailySalesService _dailySalesService;
        
        AddUpdateSalesPageViewModel _addUpdateSalesPageViewModel; 

        //public LoginPageViewModel _loginPageViewModel;
        public SalesDetailPageViewModel(IDailySalesService dailySalesService, AddUpdateSalesPageViewModel addUpdateSalesPageViewModel)
        {
            _dailySalesService = dailySalesService;         
            _addUpdateSalesPageViewModel = addUpdateSalesPageViewModel;
            string saaleDate= SalesDetail.SalesDate.ToString("dd/MM/yyyy");
            GetSalesList();


           

        }

        [RelayCommand]
        public async Task GetSalesList()
        {
            try
            {                
                DailySalesList.Clear();
                DailySalesGroupList.Clear();
                double totalAmount = 0;
                var salesList = await _dailySalesService.GetDailySalesList();
                if (salesList?.Count > 0)
                {                   
                    foreach (var sales in salesList)
                    {
                        //Filter by month
                        //if (sales.SalesDate.ToString("MMMM") == SalesReportDetail.SalesMonth)
                        //if (sales.SalesDate.ToString("MMMM") == "May")
                       
                        var sDate = sales.SalesDate.ToString("dd/MM/yyyy");
                        var tDate = SalesReportDetail.SalesDate.ToString("dd/MM/yyyy");

                        //if (sales.SalesDate == SalesReportDetail.SalesDate)
                        if (sDate == tDate && sales.SalesType== SalesReportDetail.SalesType)
                        {
                        totalAmount += Convert.ToDouble(sales.Amount);
                        DailySalesList.Add(sales);
                        }
                    }
                    // group the list by date
                    var dic = DailySalesList.GroupBy(x => x.SalesDate.Date).ToDictionary(d => d.Key, d => d.ToList());

                    foreach (KeyValuePair<DateTime, List<DailySalesModel>> item in dic)
                    {
                        DailySalesGroupList.Add(new DailySalesDetailGroupModel(item.Key, new List<DailySalesModel>(item.Value)));
                    }

                }
                else
                {
                    //AddMonthDetail.ShowTotalAmount = totalAmount;
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]     
        public async Task DisplayAction(DailySalesModel dailySalesModel)
        {
            var response = await AppShell.Current.DisplayActionSheet("Select Option", "Cancel", null, "Edit", "Delete");
            if (response == "Edit")
            { 
                AddUpdateSalesPopup popup = new AddUpdateSalesPopup(_addUpdateSalesPageViewModel, dailySalesModel);
                Application.Current?.MainPage?.ShowPopup(popup);
            }
            else if (response == "Delete")
            {
                bool answer = await Shell.Current.DisplayAlert("Confirm Delete", $"Are you sure you want to delete {dailySalesModel.SalesType}?", "Yes", "No");
                if (!answer) return;

                var delResponse = await _dailySalesService.DeleteSales(dailySalesModel);
                if (delResponse > 0)
                {  
                    string mesage = "Sales deleted successfully!";
                    await Toast.Make(mesage, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();

                    await GetSalesList();
                }
            }
        }
    }
}
