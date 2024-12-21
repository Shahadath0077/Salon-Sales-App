using SalonAccountSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.Services
{
    public interface IAddServiceTypeService
    {
        Task<int> AddServiceType(AddServiceTypeModel addServiceTypeModel);
        Task<List<AddServiceTypeModel>> GetServiceTypeList();
        Task<int> DeleteServiceType(AddServiceTypeModel expensesDetailModel);
    }
}

