using Plugin.Maui.Audio;
using System.Windows.Input;

namespace CDLibrary.ViewModels
{
    public class BpmViewModel : BindableObject
    {
        public bool MicActive { get; set; } = false;
        public string ButtonColour
        {
            get
            {
                return MicActive ? "#FF7777" : "#FFFFFF";
            }
        }
        public string ButtonIcon
        {
            get
            {
                return MicActive ? "pause.png" : "play.png";
            }
        }
        public string CounterText
        {
            get
            {
                return Bars ? "Bars per Min" : "Beats per Min";
            }
        }
        private bool bars = false;
        public bool Bars { get
            {
                return bars;
            }
            set
            {
                bars = value;
                OnPropertyChanged(nameof(CounterText));
            }
        }
        public int BarSelection { get; set; }

        public int Bpm { get; set; } = 28;
        public bool Editable
        {
            get
            {
                return !MicActive;
            }
        }
        private IAudioManager audioManager;
        private Task tickAction;
        public ICommand OnBarChange { get; set; }

        //public int FrequencyMs
        //{
        //    get
        //    {
        //        //1000 / (60 * 3 / 60)
        //        if (Bars)
        //        {
        //            return 1000 / (Bpm * BarSelection / 60);
        //        }
        //        else
        //        {
        //            return 1000 / (Bpm / 60);
        //        }
        //    }
        //}
        public BpmViewModel(IAudioManager audioManager)
        {
            this.audioManager = audioManager;
            OnBarChange = new Command(() =>
            {

            });
            BarSelection = 3;
        }
        public async void MicActivation()
        {
            MicActive = !MicActive;
            OnPropertyChanged(nameof(ButtonColour));
            OnPropertyChanged(nameof(ButtonIcon));
            OnPropertyChanged(nameof(Editable));
            if (Bars)
            {
                Task.Run(barTicks);
            } else
            {
                Task.Run(Ticks);
            }
        }
        //public async Task Play()
        //{
        //var tick2 = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("tick.mp3"));
        //int c = 0;
        //while (MicActive)
        //{
        //    tickAction.Start();
        //    if (Bars)
        //    {
        //        if (c == 0) tick1.Play();
        //        else tick2.Play();
        //        c++;
        //        if (c == BarSelection) c = 0;
        //    }
        //    else
        //    {
        //        tick2.Play();
        //    }
        //    await Task.Delay(FrequencyMs);
        //    }
        //}
        public async Task barTicks()
        {
            var stream = await FileSystem.OpenAppPackageFileAsync("tick.wav");
            var stream2 = await FileSystem.OpenAppPackageFileAsync("tickstart.wav");
            var tick1 = audioManager.CreatePlayer(stream);
            var tick2 = audioManager.CreatePlayer(stream2);
            double Frequency = 1000.00 / ((double) Bpm / 60.00)/ (double)BarSelection;
            int FrequencyMs = (int)Frequency - 20;
            while (MicActive)
            {
                tick2.Play();
                await Task.Delay(FrequencyMs);
                for (int i = 0; i < BarSelection - 1; i++)
                {
                    tick1.Play();
                    await Task.Delay(FrequencyMs);
                }
            }
            tick1.Dispose();
            tick2.Dispose();
        }
        public async Task Ticks()
        {
            var stream = await FileSystem.OpenAppPackageFileAsync("tick.wav");
            var tick2 = audioManager.CreatePlayer(stream);
            double Frequency = 1000.00 / ((double)Bpm / 60.00);
            int FrequencyMs = (int)Frequency - 20;
            while (MicActive)
            {
                tick2.Play();
                await Task.Delay(FrequencyMs);
            }
            tick2.Dispose();
        }

    }
}
