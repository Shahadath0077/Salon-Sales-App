using SalonAccountSystem.Models;
using SalonAccountSystem.SQLiteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.Services
{
    public class AddServiceTypeService:IAddServiceTypeService
    {
        public async Task<int> AddServiceType(AddServiceTypeModel addServiceTypeModel)
        {
            await SQLiteDbConnect.ConnectDb();
            return await SQLiteDbConnect._dbConnection.InsertAsync(addServiceTypeModel);
        }

        public async Task<List<AddServiceTypeModel>> GetServiceTypeList()
        {
            await SQLiteDbConnect.ConnectDb();
            var expensesList = await SQLiteDbConnect._dbConnection.Table<AddServiceTypeModel>().OrderByDescending(x => x.ServiceType).ToListAsync();
            return expensesList;
        }

        public async Task<int> DeleteServiceType(AddServiceTypeModel addServiceTypeModel)
        {
            await SQLiteDbConnect.ConnectDb();
            return await SQLiteDbConnect._dbConnection.DeleteAsync(addServiceTypeModel);
        }

    }
}
