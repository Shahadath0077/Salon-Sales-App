
using CommunityToolkit.Maui.Views;
using Microcharts;
using Microcharts.Maui;
using SalonAccountSystem.Models;
using SalonAccountSystem.ViewModels;
using SkiaSharp;


namespace SalonAccountSystem.Views;

public partial class MonthlyGroupSalesPopup : Popup
{
    private SalesPageViewModel _salesPageViewModel;
    private SalesReportModel _salesReportModel;
    public MonthlyGroupSalesPopup(SalesPageViewModel salesPageViewModel, SalesReportModel salesReportModel)
	{
		InitializeComponent();
        _salesPageViewModel=salesPageViewModel;
        _salesReportModel=salesReportModel;
        _salesPageViewModel.SalesReportDetail.SalesMonth= _salesReportModel.SalesMonth;

        this.BindingContext = _salesPageViewModel;
        _salesPageViewModel.GetMonthlyGroupSalesCommand.Execute(null);


        //ChartEntry[] entries = new ChartEntry[]
        //{
        //    new ChartEntry(100)
        //    {
        //    Label="Shaving",
        //    ValueLabel="30",
        //    Color=SKColor.Parse("#ff9900")

        //    },
        //    new ChartEntry(200)
        //    {
        //    Label="Hair Cut",
        //    ValueLabel="50",
        //    Color=SKColor.Parse("#dd0800"),
        //    },
        //    new ChartEntry(80)
        //    {
        //    Label="Hair Color",
        //    ValueLabel="80",
        //    Color=SKColor.Parse("#0000FF"),
        //    },
        //    new ChartEntry(120)
        //    {
        //    Label="Hair Wash",
        //    ValueLabel="120",
        //    Color=SKColor.Parse("#008000"),
        //    },
        //    new ChartEntry(220)
        //    {
        //    Label="Nail Polish",
        //    ValueLabel="220",
        //    Color=SKColor.Parse("#00FFFF"),
        //    }
        //};


        //chartView.Chart = new DonutChart
        //{
        //    Entries = entries,
        //    BackgroundColor = SKColors.Transparent
        //};




    }
  

    private void Button_Clicked(object sender, EventArgs e)
    {
		this.Close();
    }
}