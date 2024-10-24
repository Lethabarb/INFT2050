using CDLibrary.Entities;
using CDLibrary.ViewModels;

namespace CDLibrary.Pages;

public partial class EditTrack : ContentPage
{
    private TrackEditViewModel _viewModel;
	public EditTrack(Track track)
	{
        InitializeComponent();
        _viewModel = new TrackEditViewModel(track);
        this.BindingContext = _viewModel;
        if (track.CD == null)
        {
            NewCdFrame.IsVisible = true;
        }
    }
    public EditTrack()
    {
        InitializeComponent();
        _viewModel = new TrackEditViewModel();
        this.BindingContext = _viewModel;
        NewCdFrame.IsVisible = true;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    private void SaveTrack(object sender, EventArgs e)
    {
        if (!_viewModel.Valid())
        {
            if (string.IsNullOrEmpty(_viewModel.Title))
            {
                TitleEditor.BackgroundColor = Color.FromArgb("#ffb8ba");
            }
            if (string.IsNullOrEmpty(_viewModel.Index))
            {
                 IndexEditor.BackgroundColor = Color.FromArgb("#ffb8ba");
            }
            return;
        }
        CD cd;
        if (_viewModel.PickerCd.ToLower() == "new cd" && _viewModel.SelectedCdIndex == App.Cds.GetAll().Count)
        {
            App.Cds.Create(new CD()
            {
                Color = _viewModel.NewCdColor.Value,
                Name = _viewModel.EditorCd,
                Index = int.Parse(_viewModel.NewCdIndex),
            });
            cd = App.Cds.GetBy(cd => cd.Name == _viewModel.EditorCd).First();
        } else
        {
            cd = App.Cds.GetBy(cd => cd.Name == _viewModel.PickerCd).First();
        }
        //Track.CD = _viewModel.;
        Track t = new Track()
        {
            Id = _viewModel.TrackId,
            Title = _viewModel.Title,
            CD = cd,
            CdId =cd.Id,
            Note1 = _viewModel.Note1,
            Note2 = _viewModel.Note2,
            Note3 = _viewModel.Note3,
            Index = int.Parse(_viewModel.Index)
        };
        if (t.Id == -1)
        {
            App.Tracks.Create(t);
        } else
        {
            App.Tracks.SaveItem(t);
        }
        Navigation.PopAsync();
    }
    private void DeleteTrack(object sender, EventArgs e)
    {
        //App.Tracks.DeleteItem(_viewModel.Track);
        Navigation.PopAsync();
    }

    private void CdPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_viewModel.PickerCd == "new CD")
        {
            NewCdFrame.IsVisible = true;
        } else
        {
            NewCdFrame.IsVisible = false;
        }
    }
}