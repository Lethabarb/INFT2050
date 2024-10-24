using Android.Content.Res;
using CDLibrary.Repositories;

namespace CDLibrary
{
    public partial class App : Application
    {
        public static TrackRepository Tracks { get; private set; }
        public static CdRepository Cds { get; private set; }
        public App(TrackRepository tracks, CdRepository cds)
        {
            InitializeComponent();

            MainPage = new AppShell();

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) => {
#if ANDROID
                handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });
            Tracks = tracks;
            Cds = cds;
        }
    }
}
