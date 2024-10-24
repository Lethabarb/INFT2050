using CDLibrary.Entities;
using CDLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDLibrary.ViewModels
{
    class TrackEditViewModel : BindableObject
    {
        public List<string> CdNames { get; set; }
        public string PickerCd { get; set; }
        public string EditorCd { get; set; }
        public string NewCdIndex { get; set; }
        public Colour NewCdColor { get; set; }
        public int SelectedCdIndex { get; set; }
        public int TrackId { get; set; }
        public string Title { get; set; }

        public string Note1 { get; set; } = "";
        public string Note2 { get; set; } = "";
        public string Note3 { get; set; } = "";
        public string Index { get; set; }

        public TrackEditViewModel()
        {
            CdNames = App.Cds.GetAllNames("new CD");
            TrackId = -1;
            PickerCd = CdNames.First();
            SelectedCdIndex = CdNames.IndexOf(PickerCd);
        }
        public TrackEditViewModel(Track track)
        {
            CdNames = App.Cds.GetAllNames("new CD");
            TrackId = track.Id;
            Note1 = track.Note1;
            Note2 = track.Note2;
            Note3 = track.Note3;
            Title = track.Title;
            if (track.CD != null)
            {
                PickerCd = track.CD.Name;
                SelectedCdIndex = CdNames.IndexOf(PickerCd);
            } else
            {
                PickerCd = "New CD";
                SelectedCdIndex = CdNames.Count;
            }
        }
        public bool Valid()
        {
            return !string.IsNullOrEmpty(Title) && Index != null;
        }


    }
}
