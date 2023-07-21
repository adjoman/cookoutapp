using The_Cookout.ViewModels;

namespace The_Cookout.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();

		BindingContext = new HomePageViewModel();
	}
}
