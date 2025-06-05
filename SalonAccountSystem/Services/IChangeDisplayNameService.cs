using SalonAccountSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.Services
{
    public interface IChangeDisplayNameService
    {
        Task<int> ChangeDisplayName(ChangeDisplayNameModel changeDisplayNameModel);
        Task<string> GetDisplayName();
       
    }
}
