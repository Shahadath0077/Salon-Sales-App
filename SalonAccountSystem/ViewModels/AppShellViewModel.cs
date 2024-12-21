using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        public async Task Logout()
        {
            if (Preferences.ContainsKey(nameof(App.loginModel)))
            {
                Preferences.Remove(nameof(App.loginModel));
            }
            //await Shell.Current.Navigation.PopToRootAsync();
            await Shell.Current.GoToAsync("///login");
            //if (await DisplayAlert("Are you sure?", "You will be logged out.", "Yes", "No"))
            //{


            //}

            //await Shell.Current.GoToAsync("/main");
            // await Shell.Current.GoToAsync("/main");
        }

        [RelayCommand]
        public async Task About()
        {
            AboutPopup aboutPopup = new AboutPopup(this);
            Application.Current?.MainPage?.ShowPopup(aboutPopup);
        }  
    }
}
