using SalonAccountSystem.Models;
using SalonAccountSystem.SQLiteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.Services
{
    public class LoginService : ILoginService
    {
        public async Task<LoginModel> VerifyUserLogin(LoginModel loginModel)
        {
            await SQLiteDbConnect.ConnectDb();
            var exist = await SQLiteDbConnect._dbConnection.Table<RegistrationModel>().Where(c => c.UserName == loginModel.UserName &&
                            c.UserPassword == loginModel.UserPassword).ToListAsync();

            if (exist.Count > 0)
            {
                foreach (var data in exist)
                {
                    loginModel.FullName = data.FullName;
                }
            }
            return loginModel;
        }
    }
}
