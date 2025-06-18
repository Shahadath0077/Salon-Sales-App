using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SalonAccountSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        [RelayCommand]
        public async Task About()
        {
            AboutPopup aboutPopup = new AboutPopup(this);
            Application.Current?.MainPage?.ShowPopup(aboutPopup);
            Shell.Current.FlyoutIsPresented = false;
        }  
    }
}
