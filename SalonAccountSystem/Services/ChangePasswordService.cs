using SalonAccountSystem.Models;
using SalonAccountSystem.SQLiteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.Services
{
    public class ChangePasswordService:IChangePasswordService
    {
        public async Task<int> ChangePassword(ChangePasswordModel changePasswordModel)
        {            
            await SQLiteDbConnect.ConnectDb();
            RegistrationModel registrationModel = new RegistrationModel();
           
             var data = await SQLiteDbConnect._dbConnection.Table<RegistrationModel>().Where(x => x.FullName == App.loginModel.FullName).ToListAsync();

            if(data.Count>0)
            {
                foreach (var dd in data)
                {
                    registrationModel.Id = Convert.ToInt32(dd.Id);
                    registrationModel.FullName = dd.FullName;
                    registrationModel.UserName = dd.UserName;
                    registrationModel.UserPassword = changePasswordModel.NewPassword;
                }
            }
 
            return await SQLiteDbConnect._dbConnection.UpdateAsync(registrationModel);           
        }
    }
}
