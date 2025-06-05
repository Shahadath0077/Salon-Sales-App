using SalonAccountSystem.ViewModels;
using SalonAccountSystem.Views;

namespace SalonAccountSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RouteNavigation();
        }

        public void RouteNavigation()
        {
            //this.BindingContext = new AppShellViewModel();
            //Routing.RegisterRoute("login", typeof(LoginPage));
            //Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            //Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            //Routing.RegisterRoute(nameof(MainTabbedPage), typeof(MainTabbedPage));
            //Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            //Routing.RegisterRoute(nameof(SalesPage), typeof(SalesPage));
            //Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            //Routing.RegisterRoute(nameof(AddUpdateSalesPage), typeof(AddUpdateSalesPage));


            this.BindingContext = new AppShellViewModel();
            //Routing.RegisterRoute("login", typeof(LoginPage));
            //Routing.RegisterRoute("registration", typeof(RegistrationPage));   
            Routing.RegisterRoute("home", typeof(HomePage));
            Routing.RegisterRoute("sales", typeof(SalesPage));
            Routing.RegisterRoute("settings", typeof(SettingsPage));
            //Routing.RegisterRoute("//salesdetailpage/addupdatesales", typeof(AddUpdateSalesPage));            
            //Routing.RegisterRoute("salesdetailpage", typeof(SalesDetailPage));

           

        }
    }
}
