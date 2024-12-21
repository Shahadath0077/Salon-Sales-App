
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
    //[QueryProperty(nameof(SalesReportDetail), "SalesReportDetail")]
    public partial class SalesPageViewModel : ObservableObject
    {
        public ObservableCollection<SalesReportModel> MonthlySalesReportList { get; set; } = new ObservableCollection<SalesReportModel>();

        private readonly IDailySalesService _dailySalesService;    
        public SalesReportModel salesReportModel { get; set; }

        [ObservableProperty]
        private DailySalesModel _salesDetail = new DailySalesModel();


       // [ObservableProperty]
       // private SalesReportModel _salesReportDetail = new SalesReportModel();

        public DailySalesModel dailySalesModel { get; set; }
        public LoginPageViewModel _loginPageViewModel;

        public SalesPageViewModel(IDailySalesService dailySalesService, LoginPageViewModel loginPageViewModel)
        {
            _dailySalesService = dailySalesService;          
            salesReportModel = new SalesReportModel();
            //dailySalesModel = new DailySalesModel();
            _loginPageViewModel= loginPageViewModel;
        }

        [RelayCommand]
        public async Task GetMonthlySalesList()
        {
            try
            {
                MonthlySalesReportList.Clear();                                                       
                var salesList = await _dailySalesService.GetDailySalesList();
                if (salesList?.Count > 0)
                {                   
                    #region
                    //foreach (var sales in salesList)
                    //{
                    //    totalAmount += Convert.ToDouble(sales.Amount);
                    //    DailySalesList.Add(sales);
                    //}

                    //var result =
                    //    from s in DailySalesList
                    //    group s by new { date = new DateTime(s.SalesDate.Year, s.SalesDate.Month, 1) } into g
                    //    select (new
                    //    {
                    //        salesDate = g.Key.date,
                    //        TotalAmount = g.Sum(x => x.Amount),
                    //    });
                    #endregion

                    List<SalesReportModel> ReportList = salesList.GroupBy(_ => new { date = new DateTime(_.SalesDate.Year, _.SalesDate.Month, 1 )})
                        .Select(g => new SalesReportModel
                        {
                            SalesMonth = g.Key.date.ToString("MMMM"),
                            Amount = g.Sum(x => x.Amount)
                        }).ToList();

                    foreach (var item in ReportList)
                    {                       
                        MonthlySalesReportList.Add(item);
                    }                 
                }
                else
                {
                    //MonthlySalesDetail.ShowTotalAmount = totalAmount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        public async Task NavigateToSalesDetailPage(SalesReportModel salesReportModel)
        {
            // Show spinner for 1 second
            await _loginPageViewModel.ShowSpinner();

            var navParam = new Dictionary<string, object>();           
            navParam.Add("SalesReportDetail", salesReportModel);
            await Shell.Current.GoToAsync("salesdetailpage", navParam);
        }
    }
}
