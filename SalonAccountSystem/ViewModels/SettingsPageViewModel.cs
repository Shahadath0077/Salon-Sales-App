using CommunityToolkit.Maui.Alerts;
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
    [QueryProperty(nameof(AddServiceTypeDetail), "AddServiceTypeDetail")]
    [QueryProperty(nameof(ChangeDisplayNameDetail), "ChangeDisplayNameDetail")]
    public partial class SettingsPageViewModel : ObservableObject
    {
        public ObservableCollection<AddServiceTypeModel> ServiceTypeList { get; set; } = new ObservableCollection<AddServiceTypeModel>();

        [ObservableProperty]
        private AddServiceTypeModel _addServiceTypeDetail = new AddServiceTypeModel();

        [ObservableProperty]
        private ChangeDisplayNameModel _changeDisplayNameDetail = new ChangeDisplayNameModel();

       
        private readonly IAddServiceTypeService _addServiceTypeService;
        private readonly IChangeDisplayNameService _changeDisplayNameService;
        //private LoginPageViewModel _loginPageViewModel;
        public SettingsPageViewModel(IAddServiceTypeService addServiceTypeService,  IChangeDisplayNameService changeDisplayNameService)
        {           
            _addServiceTypeService = addServiceTypeService;
            //_loginPageViewModel = loginPageViewModel;
            _changeDisplayNameService = changeDisplayNameService;
        }

        [RelayCommand]
        public async Task ChangeDisplayName()
        {
            if (ChangeDisplayNameDetail.FullName ==null)
            {
                string message = "Please enter name!";
                await Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            }
            else
            {
                await ShowSpinner();

                var response = await _changeDisplayNameService.ChangeDisplayName(new ChangeDisplayNameModel
                {
                    FullName = ChangeDisplayNameDetail.FullName,
                });


                if (response > 0)
                {
                    string message = "Name changed  successfully!";
                    await Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Message", "Name changed failed!", "OK");
                }
                
            }
            ChangeDisplayNameDetail = new ChangeDisplayNameModel();
        }

        [RelayCommand]
        public async Task AddServiceType()
        {
            if (AddServiceTypeDetail.ServiceType == null)
            {
                string mesage = "Please enter service type!";
                await Toast.Make(mesage, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();

            }
            else
            {   
                await ShowSpinner();
                var response = await _addServiceTypeService.AddServiceType(new AddServiceTypeModel
                {
                    ServiceType = AddServiceTypeDetail.ServiceType
                });
                if (response > 0)
                {
                    await GetServiceTypeList();                   
                    string mesage= "Service type added  successfully!";
                    await Toast.Make(mesage,CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
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
        public void ShowChangeDisplayNamePopup()
        {
            ChangeDisplayNamePopup popup = new ChangeDisplayNamePopup(this);
            Application.Current?.MainPage?.ShowPopup(popup);
        }

        [RelayCommand]
        public void ShowServiceTypePopup()
        {
            AddServiceTypePopup popup = new AddServiceTypePopup(this);
            Application.Current?.MainPage?.ShowPopupAsync(popup);
        }

        [RelayCommand]
        public async Task DisplayAction(AddServiceTypeModel addServiceTypeModel)
        {
            var response = await AppShell.Current.DisplayActionSheet("Delete Service Type?", "Cancel", null, "Delete");

            if (response== "Delete")
            {
                bool answer = await Shell.Current.DisplayAlert("Confirm Operation", $"Are you sure you want to delete {addServiceTypeModel.ServiceType}?", "Yes", "No");
                if (!answer) return;

                var delResponse = await _addServiceTypeService.DeleteServiceType(addServiceTypeModel);
                if (delResponse > 0)
                {
                    string mesage = "Service type deleted successfully!";
                    await Toast.Make(mesage, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
                    await GetServiceTypeList();
                }
            }
  
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
