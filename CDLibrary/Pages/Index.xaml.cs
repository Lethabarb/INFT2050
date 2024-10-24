using CDLibrary.Entities;
using CDLibrary.Repositories;
using CDLibrary.ViewModels;

namespace CDLibrary.Pages
{
    public partial class Index : ContentPage
    {
        private TrackIndexViewModel _viewModel;
        public Index()
        {
            InitializeComponent();
            _viewModel = new TrackIndexViewModel();
            this.BindingContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            _viewModel.UpdateTracks("");
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            NavigateToTrack((int) e.Parameter);

        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            NavigateToTrack();
        }

        private async Task NavigateToTrack(int id)
        {
            Track t = App.Tracks.GetItem(id);
            EditTrack editTrack = new EditTrack(t);
            await Navigation.PushAsync(editTrack);
        }
        private async Task NavigateToTrack()
        {
            EditTrack editTrack = new EditTrack();
            await Navigation.PushAsync(editTrack);
        }

    }

}
