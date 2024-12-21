using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SalonAccountSystem.Services;
using SalonAccountSystem.ViewModels;
using SalonAccountSystem.Views;

namespace SalonAccountSystem
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Views Registration
            builder.Services.AddSingleton<RegistrationPage>();
            builder.Services.AddSingleton<LoginPage>();
            //builder.Services.AddSingleton<MainPage>();           
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<SalesPage>();
            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddTransient<AddUpdateSalesPage>();
            builder.Services.AddSingleton<SalesDetailPage>();

            // ViewModels Registration
            builder.Services.AddSingleton<AppShellViewModel>();
            builder.Services.AddSingleton<RegistrationPageViewModel>();
            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddTransient<AddUpdateSalesPageViewModel>();
            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddSingleton<SettingsPageViewModel>();
            builder.Services.AddSingleton<SalesPageViewModel>();
            builder.Services.AddSingleton<SalesDetailPageViewModel>();


            // Services Registration
            builder.Services.AddSingleton<IRegistrationService, RegistrationService>();
            builder.Services.AddSingleton<ILoginService, LoginService>();
            builder.Services.AddSingleton<IDailySalesService, DailySalesService>();
            builder.Services.AddSingleton<IChangePasswordService, ChangePasswordService>();
            builder.Services.AddSingleton<IAddServiceTypeService, AddServiceTypeService>();




            return builder.Build();
        }
    }
}
