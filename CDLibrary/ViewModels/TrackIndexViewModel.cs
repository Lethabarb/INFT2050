using Android.Views;
using CDLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CDLibrary.ViewModels
{
    internal class TrackIndexViewModel : BindableObject
    {
        public List<Track> Tracks { get; set; }
        private string _query;
        public string Query
        {
            get
            {
                return _query;
            }
            set
            {
                _query = value;
                UpdateTracks(_query);
                OnPropertyChanged(nameof(Query));
            }
        }

        public TrackIndexViewModel()
        {

            UpdateTracks("");
        }
        public void UpdateTracks(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                Tracks = App.Tracks.GetAll();
            } else
            {
                var items = App.Tracks.GetAll();
                Tracks = new List<Track>();
                Tracks = items.Where(t => t.RelatedText(query)).ToList();
            }
            OnPropertyChanged(nameof(Tracks));
        }
    }
}
