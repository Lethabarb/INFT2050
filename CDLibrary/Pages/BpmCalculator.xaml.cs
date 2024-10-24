using CDLibrary.ViewModels;
using Plugin.Maui.Audio;

namespace CDLibrary.Pages;

public partial class BpmCalculator : ContentPage
{
	private BpmViewModel _viewModel;
	public BpmCalculator(IAudioManager audioManager)
	{
		InitializeComponent();
        _viewModel = new BpmViewModel(audioManager);
        this.BindingContext = _viewModel;
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
		_viewModel.MicActivation();
    }
}