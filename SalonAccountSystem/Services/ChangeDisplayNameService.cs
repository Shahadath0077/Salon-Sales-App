using SalonAccountSystem.Models;
using SalonAccountSystem.SQLiteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SQLite.SQLite3;

namespace SalonAccountSystem.Services
{
    public class ChangeDisplayNameService : IChangeDisplayNameService
    {
        public async Task<int> ChangeDisplayName(ChangeDisplayNameModel changeDisplayNameModel)
        {
            // using registration table to save display name:05May2025

            await SQLiteDbConnect.ConnectDb();
            RegistrationModel registrationModel = new RegistrationModel();
            int result = 0;

            var data = await SQLiteDbConnect._dbConnection.Table<RegistrationModel>().Where(x => x.Id == 1).ToListAsync();

            if (data.Count > 0)
            {
                foreach (var dd in data)
                {
                    registrationModel.Id = Convert.ToInt32(dd.Id);
                    registrationModel.FullName = changeDisplayNameModel.FullName;                   
                    result= await SQLiteDbConnect._dbConnection.UpdateAsync(registrationModel);
                    
                }
            }
            else
            {
                registrationModel.Id = 1;
                registrationModel.FullName = changeDisplayNameModel.FullName;
                registrationModel.UserName = "Test";
                registrationModel.UserPassword = "Password";
                result= await SQLiteDbConnect._dbConnection.InsertAsync(registrationModel);
               
            }

            return result;
        }

        public async Task<string> GetDisplayName()
        {
            string name = "";
            await SQLiteDbConnect.ConnectDb();
            var exist = await SQLiteDbConnect._dbConnection.Table<RegistrationModel>().Where(x => x.Id == 1).ToListAsync();

            if (exist.Count > 0)
            {
                foreach (var data in exist)
                {
                    name = data.FullName;
                }
            }
            return name;

        }
    }
}
