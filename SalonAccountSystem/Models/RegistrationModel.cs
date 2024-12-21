using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.Models
{
    public class RegistrationModel
    {
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
    }
}
