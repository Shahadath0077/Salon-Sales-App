using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using SalonAccountSystem.Models;
using SalonAccountSystem.Services;
using SalonAccountSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.ViewModels
{
    [QueryProperty(nameof(LoginDetail), "LoginDetail")]
    public partial class LoginPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private LoginModel _loginDetail = new LoginModel();

        [ObservableProperty]
        private RegistrationModel registrationModel = new RegistrationModel();

        private readonly ILoginService _loginService;

       // private HomePageViewModel _tabbedPageModel { get; set; }

        public LoginPageViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            //_tabbedPageModel = new HomePageViewModel();
        }

        [RelayCommand]
        public async Task UserLogin()
        {
            

            if (LoginDetail.UserName != null && LoginDetail.UserPassword != null)
            {
                LoginModel loginModel = await _loginService.VerifyUserLogin(LoginDetail);

                if (loginModel.FullName != null)
                {
                    await ShowSpinner();

                    if (Preferences.ContainsKey(nameof(App.loginModel)))
                    {
                        Preferences.Remove(nameof(App.loginModel));
                    }
                    string userDetails = JsonConvert.SerializeObject(loginModel);

                    Preferences.Set(nameof(App.loginModel), userDetails);
                    App.loginModel = loginModel;

                    await Shell.Current.GoToAsync("///home");
                    //await AppShell.Current.GoToAsync(nameof(HomePage));

                    //App.Current.MainPage = new MainTabbedPage(_tabbedPageModel);
                    // App.Current.MainPage = new AppShell();
                    //await AppShell.Current.GoToAsync(nameof(MainTabbedPage));

                }
                else
                {
                    await AppShell.Current.DisplayAlert("Message", "User not found!", "OK");
                }
            }
            else
            {
                await AppShell.Current.DisplayAlert("Message", "Enter user name and password!", "OK");
            }
            LoginDetail = new LoginModel();
        }

        [RelayCommand]
        public async Task UserRegistration()
        {
            //await AppShell.Current.GoToAsync(nameof(RegistrationPage));
            await AppShell.Current.GoToAsync("/registration");
        }

        public async Task ShowSpinner()
        {
            var popup = new SpinnerPopup();
            Application.Current?.MainPage?.ShowPopup(popup);
            await new TaskFactory().StartNew(() => { Thread.Sleep(1000); });
            popup.Close();            
        }
    }
}
