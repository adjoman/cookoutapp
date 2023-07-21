using CommunityToolkit.Maui.Views;
using Microsoft.AppCenter.Crashes;
using Microsoft.Win32;
using The_Cookout.ViewModels;

namespace The_Cookout.Views
{
	public partial class LoginView : ContentPage
	{
		public LoginView()
		{
			InitializeComponent();

			BindingContext = new LoginPageViewModel(Navigation);
		}

        async void OnSignupTapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
        {
            try
            {
                Device.InvokeOnMainThreadAsync(() =>
                {
                    Navigation.PushAsync(new RegisterView());
                });


               // if (App.Current.MainPage is NavigationPage)
                   // await (App.Current.MainPage as NavigationPage).PushAsync(new RegisterView());

                //await Shell.Current.GoToAsync("/RegisterView");
                //await Navigation.PushAsync(new RegisterView());
            }
			catch (Exception ex)
			{
				Crashes.TrackError(ex);
			}
        }

        void ForgotPassword_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
        {
            try
            {
                this.ShowPopup(new ForgotPassword());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw;
            }
        }
    }
}
