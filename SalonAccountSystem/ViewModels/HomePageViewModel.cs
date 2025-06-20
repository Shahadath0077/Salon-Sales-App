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
    [QueryProperty(nameof(MonthlySalesDetail), "MonthlySalesDetail")]
    [QueryProperty(nameof(SalesDetail), "SalesDetail")]
    public partial class HomePageViewModel : ObservableObject
    {
        private readonly IDailySalesService _dailySalesService;
        private readonly IChangeDisplayNameService _changeDisplayNameService;

        public ObservableCollection<DailySalesModel> DailySalesList { get; set; } = new ObservableCollection<DailySalesModel>();
        public ObservableCollection<DailySalesGroupModel> MonthlyGroupSalesList { get; set; } = new ObservableCollection<DailySalesGroupModel>();
      
        [ObservableProperty]
        private DailySalesModel _monthlySalesDetail = new DailySalesModel();
        [ObservableProperty]
        private DailySalesModel _salesDetail = new DailySalesModel();         
        [ObservableProperty]
        private ChangeDisplayNameModel _displayName = new ChangeDisplayNameModel();

        AddUpdateSalesPageViewModel _addUpdateSalesPageViewModel;
        SalesDetailPageViewModel _salesDetailPageViewModel;        
      
        public HomePageViewModel(IDailySalesService dailySalesService, IChangeDisplayNameService changeDisplayNameService, AddUpdateSalesPageViewModel addUpdateSalesPageViewModel, SalesDetailPageViewModel salesDetailPageViewModel)
        {
            _dailySalesService = dailySalesService;       
            _changeDisplayNameService = changeDisplayNameService;
            _addUpdateSalesPageViewModel= addUpdateSalesPageViewModel;          
            _salesDetailPageViewModel = salesDetailPageViewModel;
        }

       
        [RelayCommand]
        public async Task GetSalesList() 
        {
            try
            { 
                bool flag = false;

                DailySalesList.Clear();
                MonthlyGroupSalesList.Clear();
                double totalAmount = 0;
                var salesList = await _dailySalesService.GetDailySalesList();               
                if (salesList?.Count > 0)
                {
                    foreach (var sales in salesList)
                    { 
                        if (sales.SalesDate.Date.ToString("dd/MM/yyyy") == SalesDetail.SalesDate.ToString("dd/MM/yyyy"))
                        {
                            totalAmount += Convert.ToDouble(sales.Amount);
                            DailySalesList.Add(sales);
                            flag = true;
                        }                      
                    }

                    // enable/disable layout for daily sales
                    if (flag)
                    {
                        MonthlySalesDetail.IsLayoutVisible = false;
                        MonthlySalesDetail.IsTotalAmountVisible = true;
                    }
                    else
                    {
                        MonthlySalesDetail.IsLayoutVisible = true;
                        MonthlySalesDetail.IsTotalAmountVisible = false;
                    }


                    // group the list by SalesType
                    var dic = DailySalesList.GroupBy(x => x.SalesType).ToDictionary(d => d.Key, d => d.ToList());

                    foreach (KeyValuePair<string, List<DailySalesModel>> item in dic)
                    {
                        MonthlyGroupSalesList.Add(new DailySalesGroupModel(item.Key, new List<DailySalesModel>(item.Value),totalAmount));
                    }
                    MonthlySalesDetail.ShowTotalAmount = totalAmount;
                    MonthlySalesDetail.TotalRecords = DailySalesList.Count;
                }
                else
                {
                    MonthlySalesDetail.ShowTotalAmount = totalAmount;
                    MonthlySalesDetail.IsLayoutVisible = true;
                    MonthlySalesDetail.IsTotalAmountVisible = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        public void AddSales()
        {
            AddUpdateSalesPopup popup = new AddUpdateSalesPopup(_addUpdateSalesPageViewModel,null);
            Application.Current?.MainPage?.ShowPopup(popup);

        }

        [RelayCommand]
        public async Task GetName()
        {
            var name = await _changeDisplayNameService.GetDisplayName();
            if (name != string.Empty) 
            {                
                DisplayName.FullName = name;
            }
            else
            {
                DisplayName.FullName = "User";
            }


        }

        [RelayCommand]
        public async Task GetSelectedDate()
        {
            var selectedDate = SalesDetail.SalesDate;
            if(selectedDate!=null)
            {
                await GetSalesList();
            }
        }

        [RelayCommand]
        public async Task NavigateToSalesDetailPage(DailySalesGroupModel dailySalesGroupModel)
        {
            foreach (var tt in dailySalesGroupModel)
            {
                _salesDetailPageViewModel.SalesReportDetail.SalesDate = tt.SalesDate;
                _salesDetailPageViewModel.SalesReportDetail.SalesType = tt.SalesType;
            }
             await Application.Current.MainPage.ShowPopupAsync(new SalesDetailPopup(_salesDetailPageViewModel));
        }
    }
}
