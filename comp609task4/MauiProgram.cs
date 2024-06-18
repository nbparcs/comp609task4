using Microsoft.Extensions.Logging;

namespace comp609task4
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<DataPage>();
            //builder.Services.AddTransient<StatsnInvestmentPage>();
            builder.Services.AddTransient<InfoPage>();
            builder.Services.AddTransient<InsertPage>();
            builder.Services.AddTransient<DeletePage>();
            builder.Services.AddTransient<UpdatePage>();
            return builder.Build();
        }
    }
}
