using SalonAccountSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.Services
{
    public interface IDailySalesService
    {
        Task<List<DailySalesModel>> GetDailySalesList();
        Task<int> AddSales(DailySalesModel dailySalesModel);
        Task<int> UpdateSales(DailySalesModel dailySalesModel);
        Task<int> DeleteSales(DailySalesModel dailySalesModel);

        Task<List<SalesReportModel>> GetDailySalesReportList();

    }
}
