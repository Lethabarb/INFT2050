using CDLibrary.Pages;
using CDLibrary.Repositories;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using Index = CDLibrary.Pages.Index;

namespace CDLibrary
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                handlers.AddHandler(typeof(Shell), typeof(CDLibrary.Platforms.Android.AndroidShellRenderer));
#elif IOS
                    handlers.AddHandler(typeof(Shell), typeof(CDLibrary.Platforms.iOS.IosShellRenderer));
#endif
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.AddAudio();
            builder.Services.AddSingleton<BpmCalculator>();
            builder.Services.AddSingleton<Index>();
            builder.Services.AddSingleton<EditTrack>();
            builder.Services.AddSingleton<IAudioManager, AudioManager>();
            builder.Services.AddSingleton<DatabaseContext>();
            builder.Services.AddTransient<CdRepository>();
            builder.Services.AddTransient<TrackRepository>();

            return builder.Build();
        }
    }
}
