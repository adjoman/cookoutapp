using The_Cookout.ViewModels;

namespace The_Cookout.Views
{
	public partial class RegisterView : ContentPage
	{
		public RegisterView()
		{
			InitializeComponent();
			BindingContext = new RegisterViewModel();
		}
	}
}
