using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.Models
{
    public class SalesReportModel : ObservableObject
    {
        public string? SalesMonth { get; set; }
        public double? Amount { get; set; }        
    }

    public class SalesReportGroupModel : List<SalesReportModel>
    {
        public string? SalesMonth { get; set; }
        public double? Amount { get; set; }
        public SalesReportGroupModel(string salesMonth, List<SalesReportModel> salesReportList) : base(salesReportList)
        {
            SalesMonth = salesMonth;
            Amount = salesReportList.Where(x => x.Amount.HasValue).Sum(x => x.Amount.Value);
        }
    }
}
