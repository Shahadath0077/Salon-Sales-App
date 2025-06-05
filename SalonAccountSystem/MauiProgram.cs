using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SalonAccountSystem.Models;
using SalonAccountSystem.Services;
using SalonAccountSystem.ViewModels;
using SalonAccountSystem.Views;
using Microcharts.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;

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
                .UseMicrocharts()
                .UseSkiaSharp()
                
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
            {


#if ANDROID
            handler.PlatformView.BackgroundTintList =
Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif


            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Views Registration                   
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<SalesPage>();
            builder.Services.AddSingleton<SettingsPage>();
            
            // ViewModels Registration
            builder.Services.AddSingleton<AppShellViewModel>();           
            builder.Services.AddTransient<AddUpdateSalesPageViewModel>();
            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddSingleton<SettingsPageViewModel>();
            builder.Services.AddSingleton<SalesPageViewModel>();
            builder.Services.AddSingleton<SalesDetailPageViewModel>();
            builder.Services.AddSingleton<ChangeDisplayNameModel>();
            builder.Services.AddSingleton<SalesReportModel>();

            // Services Registration           
            builder.Services.AddSingleton<IDailySalesService, DailySalesService>();           
            builder.Services.AddSingleton<IAddServiceTypeService, AddServiceTypeService>();
            builder.Services.AddSingleton<IChangeDisplayNameService, ChangeDisplayNameService>();

            return builder.Build();
        }
    }
}
