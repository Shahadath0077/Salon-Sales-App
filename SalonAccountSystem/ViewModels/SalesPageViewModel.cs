
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts.Maui;
using Microcharts;
using SalonAccountSystem.Models;
using SalonAccountSystem.Services;
using SalonAccountSystem.Views;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;



namespace SalonAccountSystem.ViewModels
{
    [QueryProperty(nameof(SalesDetail), "SalesDetail")]
    [QueryProperty(nameof(MonthlySalesDetail), "MonthlySalesDetail")]
    [QueryProperty(nameof(SalesReportDetail), "SalesReportDetail")]
    public partial class SalesPageViewModel : ObservableObject
    {
        public ObservableCollection<SalesReportModel> MonthlySalesReportList { get; set; } = new ObservableCollection<SalesReportModel>();

        public ObservableCollection<DailySalesModel> DailySalesList { get; set; } = new ObservableCollection<DailySalesModel>();
        public ObservableCollection<DailySalesGroupModel> MonthlyGroupSalesList { get; set; } = new ObservableCollection<DailySalesGroupModel>();

        [ObservableProperty]
        private DailySalesModel _monthlySalesDetail = new DailySalesModel();


        private readonly IDailySalesService _dailySalesService;
        public SalesReportModel _salesReportModel { get; set; }

        [ObservableProperty]
        private DailySalesModel _salesDetail = new DailySalesModel();


        [ObservableProperty]
        private SalesReportModel _salesReportDetail = new SalesReportModel();

        public DailySalesModel dailySalesModel { get; set; }
       // public LoginPageViewModel _loginPageViewModel;

        private SettingsPageViewModel _settingsPageViewModel;

        public Chart Chart { get; private set; }
        public SalesPageViewModel(IDailySalesService dailySalesService, SettingsPageViewModel settingsPageViewModel, SalesReportModel salesReportModel)
        {
            _dailySalesService = dailySalesService;
            _salesReportModel = salesReportModel;
            _settingsPageViewModel= settingsPageViewModel;

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
                    List<SalesReportModel> ReportList = salesList.GroupBy(_ => new { date = new DateTime(_.SalesDate.Year, _.SalesDate.Month, 1) })
                        .Select(g => new SalesReportModel
                        {
                            SalesMonth = g.Key.date.ToString("MMMM"),
                            SalesYear=g.Key.date.ToString("yyyy"),
                            Amount = g.Sum(x => x.Amount)
                        }).ToList();

                    foreach (var item in ReportList)
                    {
                        MonthlySalesReportList.Add(item);
                    }
                    MonthlySalesDetail.IsLayoutVisible = false;
                }
                else
                {
                    MonthlySalesDetail.IsLayoutVisible = true;
                    //MonthlySalesDetail.ShowTotalAmount = totalAmount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        public async Task GetMonthlyGroupSales()
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
                        if (sales.SalesDate.ToString("MMMM") == SalesReportDetail.SalesMonth)
                        {
                            totalAmount += Convert.ToDouble(sales.Amount);
                            DailySalesList.Add(sales);
                        }
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        public async Task NavigateToSalesDetailPage(SalesReportModel salesReportModel)
        { 
            await _settingsPageViewModel.ShowSpinner();
            await PullValues(salesReportModel.SalesMonth);

            MonthlyGroupSalesPopup popup = new MonthlyGroupSalesPopup(this, salesReportModel);
            Application.Current?.MainPage?.ShowPopup(popup);

        }
        public async Task PullValues(string? salesMonth)
        {
            double totalAmount = 0;
            DailySalesList.Clear();
            List<ChartModel> categoryGroupList = new List<ChartModel>();

            var dailySalesList = await _dailySalesService.GetDailySalesList();
            if (dailySalesList?.Count > 0)
            {
                foreach (var sales in dailySalesList)
                {
                    if (sales.SalesDate.ToString("MMMM") == salesMonth)
                    {
                        totalAmount += Convert.ToDouble(sales.Amount);
                        DailySalesList.Add(sales);
                    }
                }
            }

            var groupSalesList = DailySalesList.GroupBy(x => x.SalesType, x => x.Amount).ToList();

            var random = new Random();

            foreach (var group in groupSalesList)
            {
                var hexColors = String.Format("#{0:X6}", random.Next(0x1000000));
                var model = new ChartModel();
                {
                    model.SalesType = group.Key;
                    model.Amount = group.Sum(x => Convert.ToInt32(x));
                    model.HexColor = hexColors;
                };
                categoryGroupList.Add(model);
            }

            ChartEntry[] entries = new ChartEntry[groupSalesList.Count];

            for (int i = 0; i <= groupSalesList.Count-1; i++)
            {
                var category = categoryGroupList[i];
                entries[i] = new ChartEntry((float?)category.Amount)
                {
                    Label = category.SalesType,
                    ValueLabel = category.Amount.ToString(),
                    Color = SKColor.Parse(category.HexColor)
                };   
            }

            Chart = new DonutChart()
            {
                Entries = entries,
                BackgroundColor = SKColors.Transparent
            };
        }



        //private void SyncArray()
        //{
        //    if (WeightList.Count != entries.Length)
        //    {
        //        entries = new ChartEntry[WeightList.Count];
        //    }

        //    for (int i = 0; i <= WeightList.Count - 1; i++)
        //    {
        //        if (i == WeightList.Count - 1 || i == 0)
        //        {
        //            entries[i] = new ChartEntry(WeightList[i]) { Label = "" + i, ValueLabel = "" + WeightList[i] };
        //        }
        //        else
        //        {
        //            entries[i] = new ChartEntry(WeightList[i]) { Label = "" + i };
        //        }
        //    }
        //    Chart = new LineChart() { Entries = entries, BackgroundColor = SKColors.Transparent };
        //}
    }
}
