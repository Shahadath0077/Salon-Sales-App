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
    public partial class SalesDetailPageViewModel:ObservableObject
    {
        public ObservableCollection<DailySalesModel> DailySalesList { get; set; } = new ObservableCollection<DailySalesModel>();
        public ObservableCollection<DailySalesDetailGroupModel> DailySalesGroupList { get; set; } = new ObservableCollection<DailySalesDetailGroupModel>();

        [ObservableProperty]
        private DailySalesModel _salesDetail = new DailySalesModel();

        [ObservableProperty]
        private SalesReportModel _salesReportDetail = new SalesReportModel();

        private readonly IDailySalesService _dailySalesService;
        //public DailySalesModel dailySalesModel { get; set; }

        public LoginPageViewModel _loginPageViewModel;
        public SalesDetailPageViewModel(IDailySalesService dailySalesService, LoginPageViewModel loginPageViewModel)
        {
            _dailySalesService = dailySalesService;
            //dailySalesModel=new DailySalesModel();
            _loginPageViewModel= loginPageViewModel;
           
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
                        if (sales.SalesDate.ToString("MMMM") == SalesReportDetail.SalesMonth)
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
            var response = await AppShell.Current.DisplayActionSheet("Select Option", "OK", null, "Edit", "Delete");
            if (response == "Edit")
            {
                var navParam = new Dictionary<string, object>();
                navParam.Add("SalesDetail", dailySalesModel);
                await Shell.Current.GoToAsync("//salesdetailpage/addupdatesales", navParam); 
            }
            else if (response == "Delete")
            {
                var delResponse = await _dailySalesService.DeleteSales(dailySalesModel);
                if (delResponse > 0)
                {
                    await Shell.Current.DisplayAlert("Message", "Sales deleted successfully!", "OK");
                    await GetSalesList();
                }
            }
        }

    }
}
