using SalonAccountSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.Services
{
    public interface ILoginService
    {
        Task<LoginModel> VerifyUserLogin(LoginModel loginModel);
    }
}
