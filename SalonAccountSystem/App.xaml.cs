using SalonAccountSystem.Models;
using SalonAccountSystem.ViewModels;

namespace SalonAccountSystem
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
