using SalonAccountSystem.Models;
using SalonAccountSystem.SQLiteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.Services
{
    public class DailySalesService : IDailySalesService
    {
        public async Task<int> AddSales(DailySalesModel dailySalesModel)
        {
            await SQLiteDbConnect.ConnectDb();
            return await SQLiteDbConnect._dbConnection.InsertAsync(dailySalesModel);
        }

        public async Task<int> DeleteSales(DailySalesModel dailySalesModel)
        {
            await SQLiteDbConnect.ConnectDb();
            return await SQLiteDbConnect._dbConnection.DeleteAsync(dailySalesModel);
        }

        public async Task<List<DailySalesModel>> GetDailySalesList()
        {
            await SQLiteDbConnect.ConnectDb();
            var salesList = await SQLiteDbConnect._dbConnection.Table<DailySalesModel>().OrderByDescending(x => x.SalesDate).ToListAsync(); 
            return salesList;
        }

       
        public async Task<int> UpdateSales(DailySalesModel dailySalesModel)
        {
            await SQLiteDbConnect.ConnectDb();
            if (dailySalesModel.SalesType== "Select a service")
            {
                return -1;
            }
            else
            {
                return await SQLiteDbConnect._dbConnection.UpdateAsync(dailySalesModel);
            }

           
            
        }
    }
}
