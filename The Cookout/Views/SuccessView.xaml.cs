using NiftyNeighbor.ViewModels;

namespace The_Cookout.Views
{
	public partial class SuccessView : ContentPage
	{
		public SuccessView()
		{
			InitializeComponent();

			BindingContext = new SuccessPageViewModel(Navigation);
		}
	}
}
