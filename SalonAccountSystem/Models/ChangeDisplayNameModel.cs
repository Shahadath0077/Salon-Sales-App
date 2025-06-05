using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.Models
{
    public partial class ChangeDisplayNameModel : ObservableObject
    {
        [ObservableProperty]
        public string? fullName;
    }
}
