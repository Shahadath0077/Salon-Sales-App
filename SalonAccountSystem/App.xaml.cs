using SalonAccountSystem.Models;

namespace SalonAccountSystem
{
    public partial class App : Application
    {
        public static LoginModel? loginModel;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
