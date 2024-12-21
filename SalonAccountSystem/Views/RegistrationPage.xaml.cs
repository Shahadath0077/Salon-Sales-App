using SalonAccountSystem.ViewModels;

namespace SalonAccountSystem.Views;

public partial class RegistrationPage : ContentPage
{
    private RegistrationPageViewModel _registrationPageViewModel;
    public RegistrationPage(RegistrationPageViewModel registrationPageViewModel)
	{
		InitializeComponent();
        _registrationPageViewModel = registrationPageViewModel;
        this.BindingContext = _registrationPageViewModel;
    }
}