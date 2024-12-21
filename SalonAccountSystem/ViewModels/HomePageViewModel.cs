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
    public partial class HomePageViewModel : ObservableObject
    {
        public ObservableCollection<DailySalesModel> DailySalesList { get; set; } = new ObservableCollection<DailySalesModel>();
        public ObservableCollection<DailySalesGroupModel> MonthlyGroupSalesList { get; set; } = new ObservableCollection<DailySalesGroupModel>();

        [ObservableProperty]
        private DailySalesModel _monthlySalesDetail = new DailySalesModel();   
        public DailySalesModel salesDetailModel { get; set; }

        private readonly IDailySalesService _dailySalesService;
        public HomePageViewModel(IDailySalesService dailySalesService)
        {
            _dailySalesService=dailySalesService;
            salesDetailModel=new DailySalesModel();            
        }

        [RelayCommand]
        public async Task GetSalesList() 
        {
            try
            {               
                DailySalesList.Clear();
                MonthlyGroupSalesList.Clear();
                double totalAmount = 0;
                var salesList = await _dailySalesService.GetDailySalesList();               
                if (salesList?.Count > 0)
                {
                    foreach (var sales in salesList)
                    {
                        //Filter by month
                        var today = DateTime.Today;
                        if (sales.SalesDate.Date == today)
                        {
                            totalAmount += Convert.ToDouble(sales.Amount);
                            DailySalesList.Add(sales);
                        }

                        // get the month name
                        //string month_name = sales.SalesDate.Date.ToString("MMMM");

                        //if (month_name=="December")
                        //{
                        //    // get all records for this month
                        //    MonthlySalesList.Add(sales);

                        //    // get total amount for this month
                        //}

                    }

                    // group the list by SalesType
                    var dic = DailySalesList.GroupBy(x => x.SalesType).ToDictionary(d => d.Key, d => d.ToList());

                    foreach (KeyValuePair<string, List<DailySalesModel>> item in dic)
                    {
                        MonthlyGroupSalesList.Add(new DailySalesGroupModel(item.Key, new List<DailySalesModel>(item.Value)));
                    }
                    MonthlySalesDetail.ShowTotalAmount = totalAmount;
                    MonthlySalesDetail.TotalRecords = DailySalesList.Count;
                }
                else
                {
                    MonthlySalesDetail.ShowTotalAmount = totalAmount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        public async Task AddSales()
        {
            await Shell.Current.GoToAsync("//salesdetailpage/addupdatesales");     
        }
    }
}
