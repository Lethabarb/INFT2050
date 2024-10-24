using Android.Media;
using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Storage;

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
                return "microphone.png";
            }
        }
        public bool Bars { get; set; }
        public int BarSelection { get; set; }
        public int Bpm { get; set; }
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

        public int FrequencyMs { get
            {
                //1000 / (60 * 3 / 60)
                if (Bars)
                {
                    return 1000 / (Bpm * BarSelection / 60);
                }
                else
                {
                    return 1000 / (Bpm / 60);
                }
            }
        }
        public BpmViewModel(IAudioManager audioManager)
        {
            this.audioManager = audioManager;
            OnBarChange = new Command(() =>
            {

            });
        }
        public void MicActivation()
        {
            MicActive = !MicActive;
            OnPropertyChanged(nameof(ButtonColour));
            OnPropertyChanged(nameof(ButtonIcon));
            OnPropertyChanged(nameof(Editable));
            if (MicActive)
            {
                Task.Run(() => {
                    Play();
                });
            }
        }
        public async Task Play()
        {
            var tick1 = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("tickstart.mp3"));
            var tick2 = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("tick.mp3"));
            int c = 0;
            while (MicActive)
            {
                tickAction.Start();
                if (Bars)
                {
                    if (c == 0) tick1.Play();
                    else tick2.Play();
                    c++;
                    if (c == BarSelection) c = 0;
                }
                else
                {
                    tick2.Play();
                }
                await Task.Delay(FrequencyMs);
            }
        }
        //public Task barTicks()
        //{
        //    return Task.Run(() =>
        //    {
        //        while (MicActive)
        //        {
        //            tick1.Play();
        //            Task.Delay(FrequencyMs);
        //            for (int i = 0; i < BarSelection - 1; i++)
        //            {
        //                tick2.Play();
        //                Task.Delay(FrequencyMs);
        //            }
        //        }
        //    });
        //}
        //public Task Ticks()
        //{
        //    return Task.Run(() =>
        //    {
        //        while (MicActive)
        //        {
        //            tick2.Play();
        //            Task.Delay(FrequencyMs);
        //        }
        //    });
        //}

    }
}
