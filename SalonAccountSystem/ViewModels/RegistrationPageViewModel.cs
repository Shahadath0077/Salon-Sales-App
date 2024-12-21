using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SalonAccountSystem.Models;
using SalonAccountSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.ViewModels
{
    [QueryProperty(nameof(RegistrationDetail), "RegistrationDetail")]

    public partial class RegistrationPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private RegistrationModel _registrationDetail = new RegistrationModel();

        private readonly IRegistrationService _registrationService;

        public LoginPageViewModel _loginPageViewModel; 

        public RegistrationPageViewModel(IRegistrationService registrationService, LoginPageViewModel loginPageViewModel)
        {
            _registrationService = registrationService;
            _loginPageViewModel = loginPageViewModel;
        }

        [RelayCommand]
        public async Task RegistrationSubmit()
        {
            int response = -1;

            if (RegistrationDetail.FullName != null && RegistrationDetail.UserName != null && RegistrationDetail.UserPassword != null)
            {
                await _loginPageViewModel.ShowSpinner(); 

                response = await _registrationService.AddRegistration(new RegistrationModel
                {
                    Id = RegistrationDetail.Id,
                    FullName = RegistrationDetail.FullName,
                    UserName = RegistrationDetail.UserName,
                    UserPassword = RegistrationDetail.UserPassword,
                });
                if (response > 0)
                {
                    await Shell.Current.DisplayAlert("Message", "User registration successful!", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while doing registration", "OK");
                }
                await Shell.Current.Navigation.PopToRootAsync();
                //await AppShell.Current.GoToAsync(nameof(MainPage));
            }
            else
            {
                await AppShell.Current.DisplayAlert("Message", "Enter user details!", "OK");
            }
            RegistrationDetail = new RegistrationModel();
        }

        [RelayCommand]
        public async Task NavigateToLoginPage()
        {           
            await Shell.Current.GoToAsync("///login");         
            //await Shell.Current.Navigation.PopToRootAsync();
        }

    }
}
