using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SalonAccountSystem.Models;
using SalonAccountSystem.Services;
using SalonAccountSystem.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonAccountSystem.ViewModels
{
    [QueryProperty(nameof(ChangePasswordDetail), "ChangePasswordDetail")]
    [QueryProperty(nameof(AddServiceTypeDetail), "AddServiceTypeDetail")]
    public partial class SettingsPageViewModel : ObservableObject
    {        
        public ObservableCollection<AddServiceTypeModel> ServiceTypeList { get; set; } = new ObservableCollection<AddServiceTypeModel>();

        [ObservableProperty]
        private ChangePasswordModel _changePasswordDetail = new ChangePasswordModel();

        [ObservableProperty]
        private AddServiceTypeModel _addServiceTypeDetail = new AddServiceTypeModel();

        private readonly IChangePasswordService _changePasswordService;
        private readonly IAddServiceTypeService _addServiceTypeService;
        private LoginPageViewModel _loginPageViewModel;
        public SettingsPageViewModel(IChangePasswordService changePasswordService, IAddServiceTypeService addServiceTypeService, LoginPageViewModel loginPageViewModel)
        {
            _changePasswordService=changePasswordService;
            _addServiceTypeService = addServiceTypeService;
            _loginPageViewModel=loginPageViewModel;
        }

        [RelayCommand]
        public async Task ChangePassword()
        {
            if (ChangePasswordDetail.NewPassword ==null || ChangePasswordDetail.ConfirmPassword == null)
            {
                await Shell.Current.DisplayAlert("Message", "Please enter password", "OK");
            }
            else
            {
                if (ChangePasswordDetail.NewPassword != ChangePasswordDetail.ConfirmPassword)
                {
                    await Shell.Current.DisplayAlert("Message", "Password Mismatched, Try Again!", "OK");
                }
                else
                {
                    await _loginPageViewModel.ShowSpinner();
                    var response = await _changePasswordService.ChangePassword(new ChangePasswordModel
                    {
                        NewPassword = ChangePasswordDetail.NewPassword,
                    });
                    if (response > 0)
                    {
                        await Shell.Current.DisplayAlert("Message", "Password changed  successfully!", "OK");  
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Message", "Password changed failed!", "OK");
                    }
                }                   
            }
            ChangePasswordDetail = new ChangePasswordModel();
        }

        [RelayCommand]
        public async Task AddServiceType()
        {
            if (AddServiceTypeDetail.ServiceType == null)
            {
                await Shell.Current.DisplayAlert("Message", "Please enter service type", "OK");
            }
            else
            {
                await _loginPageViewModel.ShowSpinner();
                var response = await _addServiceTypeService.AddServiceType(new AddServiceTypeModel
                {
                    ServiceType = AddServiceTypeDetail.ServiceType
                });
                if (response > 0)
                {
                    await GetServiceTypeList();
                    await Shell.Current.DisplayAlert("Message", "Service type added  successfully!", "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Message", "Service type added failed!", "OK");
                }
                AddServiceTypeDetail = new AddServiceTypeModel();
            }
        }

        [RelayCommand]
        public async Task GetServiceTypeList()
        {
            try
            {
                ServiceTypeList.Clear();                    
                var servieTypeList = await _addServiceTypeService.GetServiceTypeList();
                if (servieTypeList?.Count > 0)
                { 
                    foreach (var serviceType in servieTypeList)
                    {
                        ServiceTypeList.Add(serviceType);                  
                    }                 
                }
                else
                {
                    
                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [RelayCommand]
        public void ShowChangePasswordPopup()
        {
            ChangePasswordPopup popup = new ChangePasswordPopup(this);
            Application.Current?.MainPage?.ShowPopup(popup);
        }

        [RelayCommand]
        public void ShowServiceTypePopup()
        {
            AddServiceTypePopup popup = new AddServiceTypePopup(this);
            Application.Current?.MainPage?.ShowPopup(popup);
        }

        [RelayCommand]
        public async Task DisplayAction(AddServiceTypeModel addServiceTypeModel)
        {
            var response = await AppShell.Current.DisplayActionSheet("Delete Service Type?", "OK", null, "Delete");

            if (response== "Delete")
            {
                var delResponse = await _addServiceTypeService.DeleteServiceType(addServiceTypeModel);
                if (delResponse > 0)
                {
                    await Shell.Current.DisplayAlert("Message", "Service Type deleted successfully!", "OK");
                    await GetServiceTypeList();
                }
            }
  
        }
    }
}
