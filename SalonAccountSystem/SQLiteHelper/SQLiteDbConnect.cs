using SalonAccountSystem.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.SQLiteHelper
{
    public class SQLiteDbConnect
    {      
        public static SQLiteAsyncConnection? _dbConnection;
        public static async Task ConnectDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SalonAccount.db");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<RegistrationModel>();
                await _dbConnection.CreateTableAsync<DailySalesModel>();
                await _dbConnection.CreateTableAsync<AddServiceTypeModel>();

            }
        } 
    }
}
