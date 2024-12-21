using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.Models
{
    public partial class DailySalesModel : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int SalesId { get; set; }
        public string? SalesType { get; set; }
        public double? Amount { get; set; }
        public DateTime SalesDate { get; set; } = DateTime.Now;
        public double? GroupTotalAmount { get; set; }
        [ObservableProperty]
        public double showTotalAmount = 0;
        [ObservableProperty]
        public double totalRecords = 0;
       // [ObservableProperty]
       // public string salesMonth;

    }
    public class DailySalesGroupModel : List<DailySalesModel>
    {
        public string? SalesType { get; set; }
        public double? Amount { get; set; }     
        public DailySalesGroupModel(string salesType, List<DailySalesModel> salesList) : base(salesList)
        {
            SalesType = salesType;
            Amount = salesList.Where(x => x.Amount.HasValue).Sum(x => x.Amount.Value);
        }
    }

    public class DailySalesDetailGroupModel : List<DailySalesModel>
    {
        public DateTime? SalesDate { get; set; }
        public double? Amount { get; set; }
        public DailySalesDetailGroupModel(DateTime salesDate, List<DailySalesModel> salesLst) : base(salesLst)
        {
            SalesDate = salesDate;
            Amount = salesLst.Where(x => x.Amount.HasValue).Sum(x => x.Amount.Value);
        }
    }
}
